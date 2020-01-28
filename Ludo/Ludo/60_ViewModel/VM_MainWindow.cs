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
       
        #region Binding- und Commandkram
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
            public void RollDie(object obj)
        {
            Random rnd = new Random();
            Text_Die = rnd.Next(1, 7).ToString();
            OnNotifyPropertyChanged("Text_Die");
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
