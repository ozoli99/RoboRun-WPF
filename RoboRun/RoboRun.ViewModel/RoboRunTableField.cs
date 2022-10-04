namespace RoboRun.ViewModel
{
    public class RoboRunTableField : ViewModelBase
    {
        #region Private fields

        private bool _hasWall;
        private bool _hasCollapsedWall;
        private bool _isRobot;
        private bool _isHome;
        private bool _isFloor;

        #endregion

        #region Properties

        public int X { get; set; }

        public int Y { get; set; }

        public bool HasWall
        { 
            get { return _hasWall; }
            set
            {
                if (_hasWall != value)
                {
                    _hasWall = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasCollapsedWall
        { 
            get { return _hasCollapsedWall; }
            set
            {
                if (_hasCollapsedWall != value)
                {
                    _hasCollapsedWall = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsRobot
        { 
            get { return _isRobot; } 
            set
            {
                if (_isRobot != value)
                {
                    _isRobot = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsHome
        {
            get { return _isHome; }
            set
            {
                if (_isHome != value)
                {
                    _isHome = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsFloor
        {
            get { return _isFloor; }
            set
            {
                if (_isFloor != value)
                {
                    _isFloor = value;
                    OnPropertyChanged();
                }
            }
        }

        public DelegateCommand StepCommand { get; set; }

        #endregion
    }
}
