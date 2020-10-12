using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    #region Interface

    public interface IPageService
    {
        Page GetByAlias(string alias);
    }

    #endregion Interface

    #region Implement

    public class PageService : IPageService
    {
        #region Properties

        private IPageRepository _pageRepository;
        private IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructors

        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            this._pageRepository = pageRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Methods
        
        public Page GetByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias);
        }

        #endregion Methods
    }

    #endregion Implement
    
}