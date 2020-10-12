using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        #region Properties

        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _postCategoryService;
        private List<PostCategory> _listPostCategory;

        #endregion Properties

        #region Constructors

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _postCategoryService = new PostCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listPostCategory = new List<PostCategory>()
            {
                new PostCategory(){ID = 1, Name = "post category 1", Alias = "post-category-1", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Status = true},
                new PostCategory(){ID = 2, Name = "post category 2", Alias = "post-category-2", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Status = true},
                new PostCategory(){ID = 3, Name = "post category 3", Alias = "post-category-3", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Status = true},
                new PostCategory(){ID = 4, Name = "post category 4", Alias = "post-category-4", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Status = true},
            };
        }

        #endregion Constructors

        #region Methods

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //setup method
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listPostCategory);

            //call action
            var result = _postCategoryService.GetAll() as List<PostCategory>;

            // compare
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void PostCategoyr_Service_Create()
        {
            PostCategory postCategory = new PostCategory()
            {
                Name = "post category 1",
                Alias = "post-category-1",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Status = true
            };

            _mockRepository.Setup(m => m.Add(postCategory)).Returns((PostCategory _postCategory) =>
            {
                _postCategory.ID = 1;
                return _postCategory;
            });

            var result = _postCategoryService.Add(postCategory);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        #endregion Methods
    }
}