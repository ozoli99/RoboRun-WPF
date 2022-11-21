using RoboRun.Model;
using System.Collections.ObjectModel;

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

        public DelegateCommand PauseGameCommand { get; private set; }

        public ObservableCollection<RoboRunTableField> Fields { get; set; }

        public int GameTableSize
        { 
            get { return (int)_model.GameTableSize; }
            set
            {
                if ((int)_model.GameTableSize != value)
                {
                    _model.GameTableSize = (GameTableSize)value;
                    OnPropertyChanged();
                }
            }
        }

        public string GameTime { get { return TimeSpan.FromSeconds(_model.GameTime).ToString("g"); } }

        public bool IsGameSmall
        {
            get { return _model.GameTableSize == RoboRun.Model.GameTableSize.Small; }
            set
            {
                if (_model.GameTableSize == RoboRun.Model.GameTableSize.Small)
                    return;

                GameTableSize = (int)RoboRun.Model.GameTableSize.Small;
                GameTableSizeChanged?.Invoke(this, EventArgs.Empty);

                OnPropertyChanged("IsGameSmall");
                OnPropertyChanged("IsGameMedium");
                OnPropertyChanged("IsGameBig");
            }
        }

        public bool IsGameMedium
        {
            get { return _model.GameTableSize == RoboRun.Model.GameTableSize.Medium; }
            set
            {
                if (_model.GameTableSize == RoboRun.Model.GameTableSize.Medium)
                    return;

                GameTableSize = (int)RoboRun.Model.GameTableSize.Medium;
                GameTableSizeChanged?.Invoke(this, EventArgs.Empty);

                OnPropertyChanged("IsGameSmall");
                OnPropertyChanged("IsGameMedium");
                OnPropertyChanged("IsGameBig");
            }
        }

        public bool IsGameBig
        {
            get { return _model.GameTableSize == RoboRun.Model.GameTableSize.Big; }
            set
            {
                if (_model.GameTableSize == RoboRun.Model.GameTableSize.Big)
                    return;

                GameTableSize = (int)RoboRun.Model.GameTableSize.Big;
                GameTableSizeChanged?.Invoke(this, EventArgs.Empty);

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
        public event EventHandler? PauseGame;
        public event EventHandler? GameTableSizeChanged;

        #endregion

        #region Constructor

        public RoboRunViewModel(RoboRunModel model)
        {
            _model = model;
            _model.GameTimeAdvanced += new EventHandler<RoboRunEventArgs>(Model_GameTimeAdvanced);
            _model.RobotMoved += new EventHandler(Model_RobotMoved);

            NewGameCommand = new DelegateCommand(param => NewGame?.Invoke(this, EventArgs.Empty));
            LoadGameCommand = new DelegateCommand(param => LoadGame?.Invoke(this, EventArgs.Empty));
            SaveGameCommand = new DelegateCommand(param => SaveGame?.Invoke(this, EventArgs.Empty));
            ExitGameCommand = new DelegateCommand(param => ExitGame?.Invoke(this, EventArgs.Empty));
            PauseGameCommand = new DelegateCommand(param => PauseGame?.Invoke(this, EventArgs.Empty));

            Fields = new ObservableCollection<RoboRunTableField>();
            for (int i = 0; i < _model.GameTable.Size; i++)
            {
                for (int j = 0; j < _model.GameTable.Size; j++)
                {
                    Fields.Add(new RoboRunTableField
                    {
                        X = i,
                        Y = j,
                        HasWall = _model.GameTable.HasWall(i, j),
                        HasCollapsedWall = _model.GameTable.HasWall(i, j) ? _model.GameTable.GetWall(i, j).Collapsed : false,
                        HasCollapsedWallRobot = _model.GameTable.HasWall(i, j) ? (_model.GameTable.GetWall(i, j).Collapsed && _model.GameTable.IsRobot(i, j)) : false,
                        IsRobot = _model.GameTable.IsRobot(i, j),
                        IsHome = _model.GameTable.IsHome(i, j),
                        IsFloor = !_model.GameTable.IsRobot(i, j) && !_model.GameTable.HasWall(i, j) && !_model.GameTable.IsHome(i, j),
                        Number = i * _model.GameTable.Size + j,
                        StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                    });
                }
            }
        }

        #endregion

        #region Game event handlers

        private void Model_GameTimeAdvanced(object? sender, RoboRunEventArgs e)
        {
            OnPropertyChanged("GameTime");
        }

        private void Model_RobotMoved(object? sender, EventArgs e)
        {
            RefreshFieldsState();
        }

        #endregion

        #region Public methods

        public void GenerateNewFields()
        {
            // TODO: Pause the game before loading and change GameTableSize
            OnPropertyChanged("GameTableSize");
            Fields.Clear();
            for (int i = 0; i < _model.GameTable.Size; i++)
            {
                for (int j = 0; j < _model.GameTable.Size; j++)
                {
                    Fields.Add(new RoboRunTableField
                    {
                        X = i,
                        Y = j,
                        HasWall = _model.GameTable.HasWall(i, j),
                        HasCollapsedWall = _model.GameTable.HasWall(i, j) ? _model.GameTable.GetWall(i, j).Collapsed : false,
                        HasCollapsedWallRobot = _model.GameTable.HasWall(i, j) ? (_model.GameTable.GetWall(i, j).Collapsed && _model.GameTable.IsRobot(i, j)) : false,
                        IsRobot = _model.GameTable.IsRobot(i, j),
                        IsHome = _model.GameTable.IsHome(i, j),
                        IsFloor = !_model.GameTable.IsRobot(i, j) && !_model.GameTable.HasWall(i, j) && !_model.GameTable.IsHome(i, j),
                        Number = i * _model.GameTable.Size + j,
                        StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                    });
                }
            }
        }

        #endregion

        #region Private methods

        private void StepGame(int index)
        {
            RoboRunTableField fieldStep = Fields[index];

            _model.Step(fieldStep.X, fieldStep.Y);

            RefreshFieldsState();
        }

        private void RefreshFieldsState()
        {
            foreach (RoboRunTableField field in Fields)
            {
                field.IsRobot = _model.GameTable.IsRobot(field.X, field.Y);
                field.IsHome = _model.GameTable.IsHome(field.X, field.Y);
                field.HasWall = _model.GameTable.HasWall(field.X, field.Y);
                field.HasCollapsedWall = _model.GameTable.HasWall(field.X, field.Y) ? _model.GameTable.GetWall(field.X, field.Y).Collapsed : false;
                field.HasCollapsedWallRobot = (field.HasCollapsedWallRobot && field.IsRobot);
                field.IsFloor = (!field.IsRobot && !field.IsHome && !field.HasWall && !field.HasCollapsedWall);
            }
        }

        #endregion
    }
}
