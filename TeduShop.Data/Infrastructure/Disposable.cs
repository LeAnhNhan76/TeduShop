using System;

namespace TeduShop.Data.Infrastructure
{
    public class Disposable : IDisposable
    {
        #region Properties

        private bool isDisposed;

        #endregion Properties

        #region Constructors

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Constructors

        #region Methods

        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }

            isDisposed = true;
        }

        // Ovveride this to dispose custom objects
        protected virtual void DisposeCore()
        {
        }

        #endregion Methods
    }
}