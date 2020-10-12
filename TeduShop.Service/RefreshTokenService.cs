using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    #region Interface

    public interface IRefreshTokenService
    {
        bool Add(RefreshToken refreshToken);

        bool Remove(RefreshToken refreshToken);

        bool RemoveById(string id);

        RefreshToken FindById(string id);

        List<RefreshToken> GetAll();
    }

    #endregion Interface

    #region Implement

    public class RefreshTokenService : IRefreshTokenService
    {
        #region Properties

        private IRefreshTokenRepository _refreshTokenRepository;
        private IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructors

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork)
        {
            this._refreshTokenRepository = refreshTokenRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Methods

        public bool Add(RefreshToken refreshToken)
        {
            var existingRefreshToken = _refreshTokenRepository
                .GetSingleByCondition(x => x.ClientId == refreshToken.ClientId 
                            && x.UserName == refreshToken.UserName);
            if(existingRefreshToken != null)
            {
                var resultRemove = _refreshTokenRepository.Delete(existingRefreshToken);
                if (resultRemove == null) return false;
            }
            var entity = _refreshTokenRepository.Add(refreshToken);
            if (entity == null) return false;
            _unitOfWork.Commit();
            return true;
        }

        public RefreshToken FindById(string id)
        {
            return _refreshTokenRepository.GetSingleByCondition(x => x.ID == id);
        }

        public List<RefreshToken> GetAll()
        {
            return _refreshTokenRepository.GetAll().ToList();
        }

        public bool Remove(RefreshToken refreshToken)
        {
            var entityRemove = _refreshTokenRepository.Delete(refreshToken);
            if (entityRemove == null) return false;
            _unitOfWork.Commit();
            return true;
        }

        public bool RemoveById(string id)
        {
            var entityRemove = _refreshTokenRepository.Delete(id);
            if (entityRemove == null) return false;
            _unitOfWork.Commit();
            return true;
        }

        #endregion Methods
    }

    #endregion Implement

}