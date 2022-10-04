using Microsoft.Win32;
using RoboRun.Model;
using RoboRun.Persistence;
using RoboRun.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace RoboRun.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Private fields

        private RoboRunModel _model;
        private RoboRunViewModel _viewModel;
        private MainWindow _view;
        private DispatcherTimer _timer;
        private DispatcherTimer _robotTimer;

        #endregion

        #region Constructor

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        #endregion

        #region Application event handlers

        private void App_Startup(object? sender, StartupEventArgs e)
        {
            _model = new RoboRunModel(new RoboRunFileDataAccess());
            _model.GameWin += new EventHandler<RoboRunEventArgs>(Model_GameWin);
            _model.GameTimePaused += new EventHandler<RoboRunEventArgs>(Model_GameTimePaused);
            Model_RandomNewGame();

            _viewModel = new RoboRunViewModel(_model);
            _viewModel.NewGame += new EventHandler(ViewModel_NewGame);
            _viewModel.LoadGame += new EventHandler(ViewModel_LoadGame);
            _viewModel.SaveGame += new EventHandler(ViewModel_SaveGame);
            _viewModel.ExitGame += new EventHandler(ViewModel_ExitGame);

            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Closing += new CancelEventHandler(View_Closing);
            _view.Show();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(Timer_Tick);
            _robotTimer = new DispatcherTimer();
            _robotTimer.Interval = TimeSpan.FromMilliseconds(150);
            _robotTimer.Tick += new EventHandler(RobotTimer_Tick);

            _timer.Start();
            _robotTimer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _model.AdvanceTime();
        }

        private void RobotTimer_Tick(object? sender, EventArgs e)
        {
            _model.MoveRobot();
        }

        #endregion

        #region View event handlers

        private void View_Closing(object? sender, CancelEventArgs e)
        {
            bool restartTimers = _timer.IsEnabled;

            _timer.Stop();
            _robotTimer.Stop();

            if (MessageBox.Show("Are you sure you want to quit?", "RoboRun", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;

                if (restartTimers)
                {
                    _timer.Start();
                    _robotTimer.Start();
                }
            }
        }

        #endregion

        #region ViewModel event handlers

        private void ViewModel_NewGame(object? sender, EventArgs e)
        {
            Model_RandomNewGame();
            _timer.Start();
            _robotTimer.Start();
        }

        private async void ViewModel_LoadGame(object? sender, EventArgs e)
        {
            bool restartTimers = _timer.IsEnabled;

            _timer.Stop();
            _robotTimer.Stop();

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Loading RoboRun table";
                openFileDialog.Filter = "RoboRun table (*.rrt)|*.rrt";
                if (openFileDialog.ShowDialog() == true)
                {
                    await _model.LoadGameAsync(openFileDialog.FileName);

                    _timer.Start();
                    _robotTimer.Start();
                }
            }
            catch (RoboRunDataException)
            {
                MessageBox.Show("Error occurred during load!" + Environment.NewLine + "Wrong path or file format.", "RoboRun Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (restartTimers)
            {
                _timer.Start();
                _robotTimer.Start();
            }
        }

        private async void ViewModel_SaveGame(object? sender, EventArgs e)
        {
            bool restartTimers = _timer.IsEnabled;

            _timer.Stop();
            _robotTimer.Stop();

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Saving RoboRun table";
                saveFileDialog.Filter = "RoboRun table (*.rrt)|*.rrt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    await _model.SaveGameAsync(saveFileDialog.FileName);

                    _timer.Start();
                    _robotTimer.Start();
                }
            }
            catch (RoboRunDataException)
            {
                MessageBox.Show("Error occurred during save!" + Environment.NewLine + "Wrong path or directory has no writing access.", "RoboRun Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (restartTimers)
            {
                _timer.Start();
                _robotTimer.Start();
            }
        }

        private void ViewModel_ExitGame(object? sender, EventArgs e)
        {
            _view.Close();
        }

        #endregion

        #region Model event handlers

        private void Model_GameWin(object? sender, RoboRunEventArgs e)
        {
            _timer.Stop();
            _robotTimer.Stop();

            MessageBox.Show("You Won!" + Environment.NewLine + "Time: " + TimeSpan.FromSeconds(e.ElapsedTime).ToString("g"), "RoboRun", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void Model_GameTimePaused(object? sender, RoboRunEventArgs e)
        {

        }

        #endregion

        #region Private methods

        private void Model_RandomNewGame()
        {
            Random random = new Random();
            int x, y;
            x = random.Next(_model.GameTable.Size);
            y = random.Next(_model.GameTable.Size);
            while (x == _model.GameTable.Size / 2 && y == _model.GameTable.Size / 2)
            {
                x = random.Next(_model.GameTable.Size);
                y = random.Next(_model.GameTable.Size);
            }
            Array values = Enum.GetValues(typeof(Direction));
            Direction randomDirection = (Direction)values.GetValue(random.Next(values.Length));
            _model.NewGame(x, y, randomDirection);
        }

        #endregion
    }
}
