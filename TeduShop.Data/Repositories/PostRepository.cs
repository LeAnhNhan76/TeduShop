using System;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow);
    }

    #endregion Interface

    #region Implement

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        #region Constructors

        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #endregion Constructor

        #region Methods

        public IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            try
            {
                var query = from post in DbContext.Posts
                            join posttag in DbContext.PostTags
                            on post.ID equals posttag.PostID
                            where posttag.TagID == tag && post.Status
                            orderby post.CreatedDate descending
                            select post;
                if (query.Count() > 0)
                {
                    totalRow = query.Count();
                    return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                else
                {
                    totalRow = 0;
                    return null;
                }
            }
            catch (Exception ex)
            {
                totalRow = 0;
                return null;
            }
        }

        #endregion Method
    }

    #endregion Implement
}