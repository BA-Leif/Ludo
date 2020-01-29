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
    public class VM_MainWindow : INotifyPropertyChanged
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
        public List<int[]> BoardView { get; set; }

        //Farben
        public string Color_Background { get; set; }
        public string Color_EmptyField { get; set; }
        public string Color_P1 { get; set; }
        public string Color_P2 { get; set; }
        public string Color_P3 { get; set; }
        public string Color_P4 { get; set; }

        //Text
        public string Text_Die { get; set; }

        //Commands
        public ICommand Cmd_TestClick { get; set; }
        public ICommand Cmd_RollDie { get; set; }

        //FieldButtons
        public bool[] Field_Enable { get; set; }
        public string[] Field_PawnColor { get; set; }
        public bool[] Field_Pawn { get; set; }

        //Konstruktor
        public VM_MainWindow()
        {
            BoardView = new List<int[]>();
            Field_Enable = new bool[100];
            Field_PawnColor = new string[100];
            Field_Pawn = new bool[100];

            for (int i = 0; i < 100; i++)
            {
                BoardView.Add(new int[2] { 9, 9 });
                Field_Enable[i] = true;
                Field_PawnColor[i] = "lightgray";
                Field_Pawn[i] = false;
            }            

            //Colors
            Color_Background = "gray";
            Color_EmptyField = "black";
            Color_P1 = "red";
            Color_P2 = "blue";
            Color_P3 = "yellow";
            Color_P4 = "green";


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
            for (int i = 0; i < 100; i++)
            {
                if (BoardView[i].Max() < 9)
                {
                    BoardView[i] = new int[2] { 9, 9 };
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    int Position_PlayersView = pawnPosition[i][k];
                    //Pöppel steht auf einem normalen Feld
                    if (Position_PlayersView >=0   &&   Position_PlayersView<=51)
                    {
                        BoardView[(Position_PlayersView+(13*i))%52] = new  int[2] { i, k };
                    }
                    //Pöppel steht auf einem Zielfeld
                    else if (Position_PlayersView >= 52)
                    {
                        BoardView[Position_PlayersView-52+60+5+(10*i)] = new int[2] { i, k };
                    }
                    //Pöppel hat das Haus noch nicht verlassen
                    else if (Position_PlayersView == -1)
                    {
                        BoardView[60+(10*i)+k] = new int[2] { i, k };
                    }
                }
            }
        }
        #endregion

        #region Commands
        public void RollDie(object obj)
        {

        }


        #endregion

        #region Hilfsfunktionen
        public string ChooseColor(int player)
        {
            string color = "";
            switch (player)
            {
                case 0:
                    color = Color_P1;
                    break;

                case 1:
                    color = Color_P2;
                    break;

                case 2:
                    color = Color_P3;
                    break;

                case 3:
                    color = Color_P4;
                    break;
                default:
                    color = "black";
                    break;
            }

            return color;
        }
        #endregion

        #region Testbereich
        public void TestClick(object obj)
        {
            List<int[]> Pre_BoardView = new List<int[]>();
            for (int i = 0; i < 100; i++)
            {
                Pre_BoardView.Add(new int[2] { 9, 9 });
            }
            Pre_BoardView[60] = new int[2] { 0, 0 };
            Pre_BoardView[4] = new int[2] { 0, 1 };
            Pre_BoardView[2] = new int[2] { 0, 2 };
            Pre_BoardView[63] = new int[2] { 0, 3 };

            BoardView = Pre_BoardView;

            for (int i = 0; i < BoardView.Count; i++)
            {
                if (BoardView[i].Max() != 9)
                {
                    Field_Enable[i] = true;
                    Field_PawnColor[i] = ChooseColor(0);
                    Field_Pawn[i] = true;
                }
                else
                {
                    Field_Enable[i] = false;
                    Field_PawnColor[i] = Color_EmptyField;
                    Field_Pawn[i] = false;
                }
                Field_Enable[60] = false;
                Field_Pawn[60] = true;
                OnNotifyPropertyChanged("Field_PawnColor");
                OnNotifyPropertyChanged("Field_Enable");
                OnNotifyPropertyChanged("Field_Pawn");
            }

        }


        #endregion
    }
}
