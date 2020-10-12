using System;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    #region Interface

    public interface IErrorService
    {
        Error Create(Error error);

        void Save();

        Guid GetGuid();
    }

    #endregion Interface

    #region Implement

    public class ErrorService : IErrorService
    {
        #region Properties

        private IErrorRepository _errorRepository;
        private IUnitOfWork _unitOfWork;
        public Guid guid { get; set; }

        #endregion Properties

        #region Constructors

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
            this.guid = Guid.NewGuid();
        }

        #endregion Constructors

        #region Methods

        public Error Create(Error error)
        {
            return _errorRepository.Add(error);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public Guid GetGuid()
        {
            return this.guid;
        }

        #endregion Methods
    }

    #endregion Implement
}