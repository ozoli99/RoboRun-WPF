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

        #endregion

        #region Events

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
