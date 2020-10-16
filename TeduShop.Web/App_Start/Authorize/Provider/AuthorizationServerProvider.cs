using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduShop.Common.Constants;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.Web.App_Start.Authorize
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        #region Properties

        private IClientService _clientService;

        /// <summary>
        /// Public client ID property.
        /// </summary>
        private readonly string _publicClientId;

        private readonly string _publicClientSecret;

        #endregion Properties

        #region Constructors

        public AuthorizationServerProvider()
        {
        }

        public AuthorizationServerProvider(string publicClientId, string publicClientSecret)
        {
            this._publicClientId = publicClientId;
            this._publicClientSecret = publicClientSecret;
        }

        #endregion Constructors

        #region Methods

        // Validate all of the token request from client
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            #region Try Get Credentials
            //string clientId = string.Empty;
            //string clientSecret = string.Empty;

            //// The TryGetBasicCredentials method checks the Authorization header and
            //// Return the ClientId and clientSecret
            //if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            //{
            //    context.SetError(Constant.OAuthorization_Invalid_Client, "Client credentials could not be retrieved through the Authorization header.");
            //    context.Rejected();
            //    return;
            //}
            #endregion

            // Initialize client service
            _clientService = GetClientService(new DbFactory() { });

            //Check the existence of by calling the ValidateClient method
            Client client = _clientService.GetByClientIdAndClientSecret(_publicClientId, _publicClientSecret);

            if (client == null)
            {
                // Client could not be validated.
                context.SetError(Constant.OAuthorization_Invalid_Client, "Client credentials are invalid.");
                context.Rejected();

            }
            else
            {
                if (!client.IsActive)
                {
                    context.SetError(Constant.OAuthorization_Invalid_Client, "Client is inactive.");
                    context.Rejected();
                }
                else
                {
                    // Client has been verified.
                    context.OwinContext.Set<Client>(Constant.OAuthorization_OAuth_Client, client);
                    context.OwinContext.Set<string>(Constant.OAuthorization_OAuth_ClientAllowedOrigin, client.AllowedOrigin);
                    context.OwinContext.Set<string>(Constant.OAuthorization_OAuth_ClientRefreshTokenLifeTime, client.RefreshTokenLifeTime.ToString());
                    context.Validated(_publicClientId);
                }
            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var client = context.OwinContext.Get<Client>(Constant.OAuthorization_OAuth_Client);
            if (client == null) return;

            var allowedOrigin = context.OwinContext.Get<string>(Constant.OAuthorization_OAuth_ClientAllowedOrigin);
            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            UserManager<ApplicationUser> userManager = context.OwinContext.GetUserManager<UserManager<ApplicationUser>>();
            ApplicationUser user;
            try
            {
                user = await userManager.FindAsync(context.UserName, context.Password);
            }
            catch (Exception ex)
            {
                // Could not retrieve the user due to error.
                context.SetError(Constant.OAuthorization_Server_Error + ex);
                context.Rejected();
                return;
            }
            if (user != null)
            {
                //ClaimsIdentity identity = await userManager.CreateIdentityAsync(
                //                            user,
                //                            DefaultAuthenticationTypes.ExternalBearer);
                //context.Validated(identity);

                var claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                claims.Add(new Claim(ClaimTypes.Role, string.Join(",", user.Roles)));
                claims.Add(new Claim("Email", user.Email));
                claimsIdentity.AddClaims(claims);

                var props = new Dictionary<string, string>()
                {
                    [Constant.OAuthorization_Properties_ClientId] = context.ClientId == null ? string.Empty : context.ClientId,
                    [Constant.OAuthorization_Properties_UserName] = context.UserName
                };
                var oauthProperties = new AuthenticationProperties(props);
                var oauthTicket = new AuthenticationTicket(claimsIdentity, oauthProperties);
                context.Validated(oauthTicket);
            }
            else
            {
                context.SetError(Constant.OAuthorization_Invalid_Grant, "Tài khoản hoặc mật khẩu không đúng.'");
                context.Rejected();
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return base.TokenEndpoint(context);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary[Constant.OAuthorization_Properties_ClientId];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError(Constant.OAuthorization_Invalid_Client, "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            //Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        #region private method

        private static IClientService GetClientService(DbFactory dbFactory)
        {
            return new ClientService(GetClientRepository(dbFactory), GetUnitOfWork(dbFactory));
        }

        private static IClientRepository GetClientRepository(DbFactory dbFactory)
        {
            return new ClientRepository(dbFactory) { };
        }

        private static IUnitOfWork GetUnitOfWork(DbFactory dbFactory)
        {
            return new UnitOfWork(dbFactory) { };
        }

        #endregion private method

        #endregion Methods
    }
}