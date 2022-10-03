using RoboRun.Model;
using RoboRun.Persistence;
using RoboRun.ViewModel;
using System;
using System.Windows;

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

            _viewModel = new RoboRunViewModel(_model);
            _viewModel.NewGame += new EventHandler(ViewModel_NewGame);
            _viewModel.LoadGame += new EventHandler(ViewModel_LoadGame);
            _viewModel.SaveGame += new EventHandler(ViewModel_SaveGame);
            _viewModel.ExitGame += new EventHandler(ViewModel_ExitGame);

            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing);
            _view.Show();
        }

        #endregion
    }
}
