using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    #region Interface

    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);

        void Save();
    }

    #endregion Interface

    #region Implement

    public class FeedbackService : IFeedbackService
    {
        #region Properties

        private IFeedbackRepository _feedbackRepository;
        private IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructors

        public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            this._feedbackRepository = feedbackRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Methods

        public Feedback Create(Feedback feedback)
        {
            return _feedbackRepository.Add(feedback);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        #endregion Methods
    }

    #endregion Implement
}