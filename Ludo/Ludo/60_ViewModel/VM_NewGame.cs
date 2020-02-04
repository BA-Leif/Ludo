using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ludo._10_SupportClass;
using Ludo._60_ViewModel;
using Ludo._20_Data;
using Ludo._50_Controller;

namespace Ludo._60_ViewModel 
{
    public class VM_NewGame : INotifyPropertyChanged
    {
        #region Eigenschaften und Konstruktor
        public MainController Controller { get; set; }
        
        //Spieleroptionen
        public NewGameOptions Settings { get; set; }


        //Commands
        public ICommand Cmd_RefreshView { get; set; }
        public ICommand Cmd_NewGame { get; set; }


        public VM_NewGame(NewGameOptions ngo, MainController controller)
        {
            Controller = controller;
            Settings = ngo;


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
            OnNotifyPropertyChanged("Settings");

        }

        public void NewGame(object obj)
        {

        }
        #endregion
    }
}
