using System.Windows.Input;

namespace RoboRun.ViewModel
{
    /// <summary>
    /// Type of delegate command.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        #region Private fields

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        #endregion

        #region Events

        public event EventHandler? CanExecuteChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiate delegate command.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        public DelegateCommand(Action<object> execute) : this(null, execute) { }

        /// <summary>
        /// Instantiate delegate command.
        /// </summary>
        /// <param name="canExecute">Condition of executing.</param>
        /// <param name="execute">Action to execute.</param>
        public DelegateCommand(Func<object, bool> canExecute, Action<object> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Check if action can be executed.
        /// </summary>
        /// <param name="parameter">Parameter of the action.</param>
        /// <returns>True if the action is executable.</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Execute action.
        /// </summary>
        /// <param name="parameter">Parameter of the action.</param>
        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("Command execution is disabled.");
            }
            _execute(parameter);
        }

        #endregion

        #region Event methods

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
