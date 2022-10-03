using RoboRun.Model;

namespace RoboRun.ViewModel
{
    /// <summary>
    /// Type of RoboRun ViewModel.
    /// </summary>
    public class RoboRunViewModel : ViewModelBase
    {
        #region Private fields

        private RoboRunModel _model;    // model

        #endregion

        #region Properties

        public DelegateCommand NewGameCommand { get; private set; }

        public DelegateCommand LoadGameCommand { get; private set; }

        public DelegateCommand SaveGameCommand { get; private set; }

        public DelegateCommand ExitGameCommand { get; private set; }

        public bool IsGameSmall
        {
            get { return _model.GameTableSize == GameTableSize.Small; }
            set
            {
                if (_model.GameTableSize == GameTableSize.Small)
                    return;

                _model.GameTableSize = GameTableSize.Small;
                OnPropertyChanged("IsGameSmall");
                OnPropertyChanged("IsGameMedium");
                OnPropertyChanged("IsGameBig");
            }
        }

        public bool IsGameMedium
        {
            get { return _model.GameTableSize == GameTableSize.Medium; }
            set
            {
                if (_model.GameTableSize == GameTableSize.Medium)
                    return;

                _model.GameTableSize = GameTableSize.Medium;
                OnPropertyChanged("IsGameSmall");
                OnPropertyChanged("IsGameMedium");
                OnPropertyChanged("IsGameBig");
            }
        }

        public bool IsGameBig
        {
            get { return _model.GameTableSize == GameTableSize.Big; }
            set
            {
                if (_model.GameTableSize == GameTableSize.Big)
                    return;

                _model.GameTableSize = GameTableSize.Big;
                OnPropertyChanged("IsGameSmall");
                OnPropertyChanged("IsGameMedium");
                OnPropertyChanged("IsGameBig");
            }
        }

        #endregion

        #region Events

        public event EventHandler? NewGame;
        public event EventHandler? LoadGame;
        public event EventHandler? SaveGame;
        public event EventHandler? ExitGame;

        #endregion

        #region Constructor

        public RoboRunViewModel(RoboRunModel model)
        {
            _model = model;
            _model.GameTimeAdvanced += new EventHandler<RoboRunEventArgs>(Model_GameTimeAdvanced);
            _model.GameTimePaused += new EventHandler<RoboRunEventArgs>(Model_GameTimePaused);
            _model.GameWin += new EventHandler<RoboRunEventArgs>(Model_GameWin);
            _model.RobotMoved += new EventHandler(Model_RobotMoved);
        }

        #endregion

        #region Game event handlers

        private void Model_GameTimeAdvanced(object? sender, RoboRunEventArgs e)
        {

        }

        private void Model_GameTimePaused(object? sender, RoboRunEventArgs e)
        {

        }

        private void Model_GameWin(object? sender, RoboRunEventArgs e)
        {

        }

        private void Model_RobotMoved(object? sender, EventArgs e)
        {

        }

        #endregion
    }
}
