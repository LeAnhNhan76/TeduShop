using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    #region Interface

    public interface IClientService
    {
        Client GetByClientIdAndClientSecret(string clientId, string clientSecret);
    }

    #endregion Interface

    #region Implement

    public class ClientService : IClientService
    {
        #region Properties

        private IClientRepository _clientRepository;
        private IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructors

        public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            this._clientRepository = clientRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Methods

        public Client GetByClientIdAndClientSecret(string clientId, string clientSecret)
        {
            return _clientRepository.GetByClientIdAndClientSecret(clientId, clientSecret);
        }
        #endregion Methods
    }

    #endregion Implement
}