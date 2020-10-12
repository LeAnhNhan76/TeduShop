using System;

namespace TeduShop.Common.Exceptions
{
    public class NameDuplicatedException : Exception
    {
        #region Constructors

        public NameDuplicatedException()
        {
        }

        public NameDuplicatedException(string message)
        : base(message)
        {
        }

        public NameDuplicatedException(string message, Exception inner)
        : base(message, inner)
        {
        }

        #endregion Constructors

        #region Methods

        #endregion  Methods
    }
}