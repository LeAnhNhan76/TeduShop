using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    #region Interface

    public interface IContactDetailService
    {
        ContactDetail GetDefaultContact();
    }

    #endregion Interface

    #region Implement

    public class ContactDetailService : IContactDetailService
    {
        #region Properties

        private IContactDetailRepository _contactDetailRepository;
        private IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructors

        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            this._contactDetailRepository = contactDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Methods

        public ContactDetail GetDefaultContact()
        {
            return _contactDetailRepository.GetSingleByCondition(x => x.Status);
        }

        #endregion Methods
    }

    #endregion Implement
}