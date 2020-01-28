using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ludo._10_SupportClass;
using Ludo._80_View;

namespace Ludo._60_ViewModel
{
    class VM_MainWindow : INotifyPropertyChanged
    {
        #region Eigenschaften & Konstruktor
        public MainWindow View { get; set; }

        // Enthält Informationen über das aktuelle Spielgeschehen
        // Jedes Feld auf dem Brett hat hierbei einen festen Platz im Array.
        //      Index          Feld
        //      0 - 51         StandardFelder des Rundkurses, wobei mit der Uhr durchgezählt wird.
        //                          Board[0] ist gleich dem Startfeld des ersten Spielers unten links.
        //                          Board[51] ist gleich dem Endfeld des ersten Spielers.
        //      
        //  10x+60 - 10x+63    Häuser der Spieler, wobei x der Speilerzahl (0-3) entspricht.
        //  10x+65 - 10x+68    Zielfelder der Spieler, wobei x der Speilerzahl (0-3) entspricht.
        //
        //  Die Einträge können sein
        //        "": ein leeres Feld
        //      Px_n: Figur n von Spieler x steht auf dem Feld.

        public string[] BoardView { get; set; }

        //Farben
        public string Color_Background { get; set; }
        public string Color_P1 { get; set; }
        public string Color_P2 { get; set; }
        public string Color_P3 { get; set; }
        public string Color_P4 { get; set; }

        //Text
        public string Text_Die { get; set; }

        //Commands
        public ICommand Cmd_TestClick { get; set; }
        public ICommand Cmd_RollDie { get; set; }

        //Konstruktor
        public VM_MainWindow()
        {
            
            //Colors
            Color_Background = "gray";
            Color_P1 = "red";

            //Text
            Text_Die = "X";

            //Commands
            Cmd_TestClick = new RelayCommand(TestClick);
            Cmd_RollDie = new RelayCommand(RollDie);
        }
        #endregion
       
        #region View & VM Beziehungen
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Change_BoardView(int[][] pawnPosition)
        {
            for (int i = 0; i < 4; i++)
            {

            }
        }
        #endregion

        #region Commands
        public void RollDie(object obj)
        {

        }


        #endregion

        #region Testbereich
        public void TestClick(object obj)
        {
            if (Color_Background != "lightgray")
            {
                Color_Background = "lightgray";
            }
            else if (Color_Background == "lightgray")
            {
                Color_Background = "gray";
            }
            OnNotifyPropertyChanged("Color_Background");
        }


        #endregion
    }
}
