using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduShop.Common.Constants;
using TeduShop.Common.Utilities;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.Web.App_Start.Authorize
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        #region Properties

        private IRefreshTokenService _refreshTokenService;

        #endregion Properties

        #region Constructors

        public RefreshTokenProvider()
        {
           
        }

        #endregion Constructors

        #region Methods

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            // Initilize refresh token service
            this._refreshTokenService = GetRefreshTokenService(new DbFactory() { });

            //Get the client Id from Ticket properties
            var clientId = context.Ticket.Properties.Dictionary[Constant.OAuthorization_Properties_ClientId];
            if (string.IsNullOrEmpty(clientId)) return;

            var userName = context.Ticket.Identity.FindFirst(ClaimTypes.NameIdentifier);
            if (userName == null) return;

            // Generating a Unique Refresh Token ID
            var refreshTokenId = Guid.NewGuid().ToString("n");
            
            var refreshTokenLifeTime = context.OwinContext.Get<string>(Constant.OAuthorization_OAuth_ClientRefreshTokenLifeTime);
            // Creating the Refresh Token object
            var refreshToken = new RefreshToken()
            {
                // storing the refreshTokenId in hash format
                ID = CipherUtility.GetHash(refreshTokenId),
                ClientId = clientId,
                UserName = userName.Value,
                IssuedTime = DateTime.UtcNow,
                ExpiredTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            // Setting Issued and Expired time of the refresh token
            context.Ticket.Properties.IssuedUtc = refreshToken.IssuedTime;
            context.Ticket.Properties.ExpiresUtc = refreshToken.ExpiredTime;

            refreshToken.ProtectedTicket = context.SerializeTicket();

            var result = _refreshTokenService.Add(refreshToken);
            if(result)
            {
                context.SetToken(refreshTokenId);
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>(Constant.OAuthorization_OAuth_ClientAllowedOrigin);
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var hashedTokenId = CipherUtility.GetHash(context.Token);
            var refreshToken = _refreshTokenService.FindById(hashedTokenId);
            if(refreshToken != null)
            {
                // Get protectedTicket from RefreshToken class 
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                var resultRemove = _refreshTokenService.RemoveById(hashedTokenId);
            }
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
        
        #endregion Methods

        #region private method

        private static IRefreshTokenService GetRefreshTokenService(DbFactory dbFactory)
        {
            return new RefreshTokenService(GetRefreshTokenRepository(dbFactory), GetUnitOfWork(dbFactory));
        }

        private static IRefreshTokenRepository GetRefreshTokenRepository(DbFactory dbFactory)
        {
            return new RefreshTokenRepository(dbFactory) { };
        }

        private static IUnitOfWork GetUnitOfWork(DbFactory dbFactory)
        {
            return new UnitOfWork(dbFactory) { };
        }

        #endregion private method
    }
}