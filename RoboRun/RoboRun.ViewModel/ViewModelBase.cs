using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RoboRun.ViewModel
{
    /// <summary>
    /// Type of ViewModel base class.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Instantiate ViewModel base class.
        /// </summary>
        protected ViewModelBase() { }

        #endregion

        #region Event methods

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}