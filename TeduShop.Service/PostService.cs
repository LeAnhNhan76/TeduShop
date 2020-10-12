using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    #region Interface

    public interface IPostService
    {
        Post Add(Post post);

        void Update(Post post);

        Post Delete(int id);

        IEnumerable<Post> GetAll();

        IEnumerable<Post> GetPaging(int page, int pageSize, out int totalRow);

        Post GetById(int id);

        IEnumerable<Post> GetByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);

        IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        void SaveChanges();
    }

    #endregion Interface

    #region Implement

    public class PostService : IPostService
    {
        #region Properties

        private IPostRepository _postRepository;
        private IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructors

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Methods

        public Post Add(Post post)
        {
            return _postRepository.Add(post);
        }

        public Post Delete(int id)
        {
            return _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetAllByTag(tag, page, pageSize, out totalRow);
        }

        public IEnumerable<Post> GetByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryId, out totalRow, page, pageSize, new[] { "PostCategory" });
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public IEnumerable<Post> GetPaging(int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }

        #endregion Methods
    }

    #endregion Implement
}