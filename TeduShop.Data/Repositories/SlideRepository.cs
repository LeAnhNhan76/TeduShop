using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface ISlideRepository : IRepository<Slide>
    {
    }

    #endregion Interface

    #region Implement

    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        #region Constructors
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion

        #region Methods
        #endregion

    }

    #endregion Implement
}