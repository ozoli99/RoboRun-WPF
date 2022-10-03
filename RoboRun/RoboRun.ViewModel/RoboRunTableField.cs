namespace RoboRun.ViewModel
{
    public class RoboRunTableField : ViewModelBase
    {
        #region Private fields

        #endregion

        #region Properties

        public int X { get; set; }

        public int Y { get; set; }

        public bool HasWall { get; set; }

        public bool HasCollapsedWall { get; set; }

        public bool IsRobot { get; set; }

        public bool IsHome { get; set; }

        public DelegateCommand StepCommand { get; set; }

        #endregion
    }
}
