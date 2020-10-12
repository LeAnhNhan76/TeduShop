using System.Collections.Generic;
using System.Linq;
using TeduShop.Common.Constants;
using TeduShop.Common.Enums;
using TeduShop.Common.Utilities;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    #region Interface

    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetLatest(int top);

        IEnumerable<Product> GetHotProduct(int top);

        IEnumerable<Product> GetAll(string keyword);

        IEnumerable<Product> GetAllByParentId(int parentId);

        IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, AppValue.SortProduct sort, out int totalRow);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, AppValue.SortProduct sort, out int totalRow);

        IEnumerable<Product> GetRelatedProducts(int id, int top);

        IEnumerable<Product> GetListProductByName(string name);

        Product GetById(int id);

        IEnumerable<Tag> GetListTagByProductId(int id);

        void IncreaseView(int id);

        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow);

        Tag GetTagById(string tagId);

        void Save();
    }

    #endregion Interface

    #region Implement

    public class ProductService : IProductService
    {
        #region Properties

        private IProductRepository _productRepository;
        private IProductTagRepository _productTagRepository;
        private ITagRepository _tagRepository;
        private IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructors

        public ProductService(IProductRepository productRepository, IProductTagRepository productTagRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._productTagRepository = productTagRepository;
            this._tagRepository = tagRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Methods

        public Product Add(Product product)
        {
            var productReturn = _productRepository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                var length = tags.Length;
                var dataTags = _tagRepository.GetAll().ToList();
                for (var i = 0; i < length; i++)
                {
                    var tagId = StringHelperUtility.ToUnsignString(tags[i]);
                    if (!dataTags.Any(x => x.ID == tagId))
                    {
                        Tag tag = new Tag()
                        {
                            ID = tagId,
                            Name = tags[i],
                            Type = Constant.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag()
                    {
                        ProductID = productReturn.ID,
                        TagID = tagId
                    };
                    _productTagRepository.Add(productTag);
                }
                _unitOfWork.Commit();
            }
            return productReturn;
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return _productRepository.GetAll();
            }
            return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
        }

        public IEnumerable<Product> GetAllByParentId(int parentId)
        {
            return _productRepository.GetMulti(x => x.Status == true);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _productRepository.GetMulti(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetLatest(int top)
        {
            return _productRepository.GetMulti(x => x.Status == true && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, AppValue.SortProduct sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);

            switch (sort)
            {
                case AppValue.SortProduct.New:
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case AppValue.SortProduct.Discount:
                    query = query.OrderByDescending(x => x.Promotion.HasValue);
                    break;

                case AppValue.SortProduct.Price:
                    query = query.OrderBy(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetListProductByName(string name)
        {
            return _productRepository.GetMulti(x => x.Name.Contains(name) && x.Status == true);
        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            return _productRepository.GetListProductByTag(tagId, page, pageSize, out totalRow);
        }

        public IEnumerable<Tag> GetListTagByProductId(int id)
        {
            return _productTagRepository.GetMulti(x => x.ProductID == id, new string[] { "Tag"}).Select(y => y.Tag);
        }

        public IEnumerable<Product> GetRelatedProducts(int id, int top)
        {
            var product = _productRepository.GetSingleById(id);
            return _productRepository.GetMulti(x => x.Status == true && x.ID != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public Tag GetTagById(string tagId)
        {
            return _tagRepository.GetSingleByCondition(x => x.ID == tagId);
        }

        public void IncreaseView(int id)
        {
            var product = _productRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
            {
                product.ViewCount += 1;
            }
            else
            {
                product.ViewCount = 1;
            }
            _productRepository.Update(product);
            _unitOfWork.Commit();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, AppValue.SortProduct sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.Name.Contains(keyword));

            switch (sort)
            {
                case AppValue.SortProduct.New:
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case AppValue.SortProduct.Discount:
                    query = query.OrderByDescending(x => x.Promotion.HasValue);
                    break;

                case AppValue.SortProduct.Price:
                    query = query.OrderBy(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                var length = tags.Length;
                var dataTags = _tagRepository.GetAll().ToList();
                for (var i = 0; i < length; i++)
                {
                    var tagId = StringHelperUtility.ToUnsignString(tags[i]);
                    if (!dataTags.Any(x => x.ID == tagId))
                    {
                        Tag tag = new Tag()
                        {
                            ID = tagId,
                            Name = tags[i],
                            Type = Constant.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag()
                    {
                        ProductID = product.ID,
                        TagID = tagId
                    };
                    _productTagRepository.Add(productTag);
                }
            }
            _unitOfWork.Commit();
        }

        #endregion Methods
    }

    #endregion Implement
}