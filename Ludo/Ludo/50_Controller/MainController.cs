using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ludo._80_View;
using Ludo._60_ViewModel;
using Ludo._20_Data;


namespace Ludo._50_Controller
{
    public class MainController
    {
        #region Eigenschaften und Konstruktor
        //Views
        public MainWindow MainWindow { get; set; }
        public VM_MainWindow VM_MainWindow { get; set; }
        public NewGameWindow NewGameWindow { get; set; }
        public VM_NewGame VM_NewGame { get; set; }

        //Data
        public GameState GS { get; set; }
        public NewGameOptions NewGameOptions { get; set; }


        #endregion

        #region Neue Fenster
        public void GetMainWindow()
        {
            MainWindow = new MainWindow();
            this.GS = new GameState();
            VM_MainWindow = new VM_MainWindow(GS, this);
            MainWindow.DataContext = VM_MainWindow;
            MainWindow.Show();
        }

        public void GetNewGameWindow()
        {
            NewGameWindow = new NewGameWindow();
            this.NewGameOptions = new NewGameOptions();
            VM_NewGame = new VM_NewGame(NewGameOptions, this);
            NewGameWindow.DataContext = VM_NewGame;
            NewGameWindow.Show();
        }
        #endregion

        #region Spielrelevantes
        public void StartNewGame()
        {
            MainWindow.Close();
            
            MainWindow = new MainWindow();
            this.GS = new GameState();
            GS.PlayerNames = NewGameOptions.PlayerNames;
            GS.AI = NewGameOptions.PlayerAI;
            VM_MainWindow = new VM_MainWindow(GS, this);
            VM_MainWindow.PlayerColors = NewGameOptions.PlayerColors;
            MainWindow.DataContext = VM_MainWindow;
            MainWindow.Show();
            NewGameWindow.Close();
        }

        #endregion
    }
}
