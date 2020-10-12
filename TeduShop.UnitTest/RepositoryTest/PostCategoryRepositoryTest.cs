using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.UnitTest.RepositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        #region Properties

        private IDbFactory dbFactory;
        private IPostCategoryRepository objRepository;
        private IUnitOfWork unitOfWork;

        #endregion Properties

        #region Constructors

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new PostCategoryRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        #endregion Constructors

        #region Methods

        [TestMethod]
        public void PostCategory_Repository_GetAll()
        {
            var list = objRepository.GetAll().ToList();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            PostCategory obj = new PostCategory()
            {
                Name = "post category test",
                Alias = "post-category-test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Status = true
            };

            var result = objRepository.Add(obj);
            unitOfWork.Commit();

            Assert.IsNotNull(result);
            Assert.AreEqual(9, result.ID);
        }

        #endregion Methods
    }
}