using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IFeedbackRepository : IRepository<Feedback>
    {
    }

    #endregion Interface

    #region Implement

    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        #region Constructors

        public FeedbackRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #endregion Constructors
    }

    #endregion Implement
}