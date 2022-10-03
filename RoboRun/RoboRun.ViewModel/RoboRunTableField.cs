namespace RoboRun.ViewModel
{
    public class RoboRunTableField : ViewModelBase
    {
        #region Private fields

        #endregion

        #region Properties

        public int X { get; set; }

        public int Y { get; set; }

        public DelegateCommand StepCommand { get; set; }

        #endregion
    }
}
