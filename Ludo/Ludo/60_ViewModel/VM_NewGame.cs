using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ludo._10_SupportClass;
using Ludo._60_ViewModel;

namespace Ludo._60_ViewModel 
{
    class VM_NewGame : INotifyPropertyChanged
    {
        #region Eigenschaften und Konstruktor
        //Spieleroptionen
        public string[] PlayerNames { get; set; }
        public string[] PlayerColors { get; set; }
        public int[][] PlayerColorArray { get; set; }
        public bool[] PlayerAI { get; set; }

        //Commands
        public ICommand Cmd_RefreshView { get; set; }
        public ICommand Cmd_NewGame { get; set; }


        public VM_NewGame()
        {
            PlayerNames = new string[] { "Linn", "Peter", "Reeta", "Joost" };
            PlayerColors = new string[4];
            PlayerColors[0] = "150,0,200";
            PlayerColors[1] = "0,0,200";
            PlayerColors[2] = "100,100,0";
            PlayerColors[3] = "0,200,0";
            PlayerColorArray = new int[4][];
            PlayerColorArray[0] = new int[3] { 150,0,200};
            PlayerColorArray[1] = new int[3] { 0,0,200};
            PlayerColorArray[2] = new int[3] { 100,100,0};
            PlayerColorArray[3] = new int[3] { 0,200,0};
            PlayerAI = new bool[4] { false, true, true, true };

            Cmd_RefreshView = new RelayCommand(RefreshView);
            Cmd_NewGame = new RelayCommand(NewGame);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Commands
        public void RefreshView(object obj)
        {
            OnNotifyPropertyChanged("PlayerColors");
            OnNotifyPropertyChanged("PlayerNames");
            OnNotifyPropertyChanged("PlayerAI");  
        }

        public void NewGame(object obj)
        {

        }
        #endregion
    }
}
