using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods

        void Commit();

        #endregion
    }
}
