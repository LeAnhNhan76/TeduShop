using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IClientRepository : IRepository<Client>
    {
        Client GetByClientIdAndClientSecret(string clientId, string clientSecret);
    }

    #endregion Interface

    #region Implement

    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        #region Constructors

        public ClientRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        
        #endregion Constructors

        #region Methods

        public Client GetByClientIdAndClientSecret(string clientId, string clientSecret)
        {
            return this.DbContext.Clients.FirstOrDefault(x => x.ClientId == clientId && x.ClientSecret == clientSecret);
        }

        #endregion Methods
    }

    #endregion Implement
}