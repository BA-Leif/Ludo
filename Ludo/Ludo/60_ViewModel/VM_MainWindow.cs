using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ludo._10_SupportClass;
using Ludo._80_View;
using Ludo._40_Model;
using Ludo._20_Data;

namespace Ludo._60_ViewModel
{
    public class VM_MainWindow : INotifyPropertyChanged
    {
        #region Eigenschaften & Konstruktor
        public MainWindow View { get; set; }
        public Game_Main Game { get; set; }
        public GameState GameState { get; set; }

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
        public ICommand Cmd_DiePhase { get; set; }

        //FieldButtons
        public int[] Field_GridX { get; set; }
        public int[] Field_GridY { get; set; }
        public string[] Field_Color { get; set; }

        //Pawns
        public int[] Pawn_PositionX { get; set; }
        public int[] Pawn_PositionY { get; set; }
        public string[] Pawn_Active { get; set; }

        //Konstruktor
        public VM_MainWindow()
        {
            GameState = new GameState();
            Game = new Game_Main(this);

            Field_GridX = new int[90]
                {8,8,8,8,7,6,6,6,5,4,3,3,
                 3,4,5,6,6,6,7,8,8,8,8,9,
                 10,10,10,10,11,12,12,12,13,14,15,15,
                 15,14,13,12,12,12,11,10,10,10,10,9,
                 0,0,
                 7,6,6,7,0,9,9,9,9,0,
                 3,3,4,4,0,4,5,6,7,0,
                 11,12,12,11,0,9,9,9,9,0,
                 15,15,14,14,0,14,13,12,11,0};

            Field_GridY = new int[90]
                {16,15,14,13,13,13,12,11,11,11,11,10,
                 9,9,9,9,8,7,7,7,6,5,4,4,
                 4,5,6,7,7,7,8,9,9,9,9,10,
                 11,11,11,11,12,13,13,13,14,15,16,16,
                 0,0,
                 16,16,15,15,0,15,14,13,12,0,
                 8,7,7,8,0,10,10,10,10,0,
                 4,4,5,5,0,5,6,7,8,0,
                 12,13,13,12,0,10,10,10,10,0};



            BoardView = new List<int[]>();
            Field_Color = new string[90];

            for (int i = 0; i < 90; i++)
            {
                BoardView.Add(new int[2] { 9, 9 });
                Field_Color[i] = "lightgray";
            }

            //Pawns
            Pawn_PositionX = new int[16];
            Pawn_PositionY = new int[16];
            Pawn_Active = new string[16];
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    Pawn_PositionX[(4 * i) + k] = Field_GridX[50 + (10 * i) + k];
                    Pawn_PositionY[(4 * i) + k] = Field_GridY[50 + (10 * i) + k];
                    Pawn_Active[(4 * i) + k] = "black";
                }
            }
            

            //Colors
            Color_Background = "gray";
            Color_EmptyField = "lightgray";
            Color_P1 = "red";
            Color_P2 = "blue";
            Color_P3 = "yellow";
            Color_P4 = "green";


            //Text
            Text_Die = "X";

            //Commands
            Cmd_TestClick = new RelayCommand(TestClick);
            Cmd_DiePhase = new RelayCommand(DiePhase_Start);
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

        public void Change_BoardView()
        {
            for (int i = 0; i < 90; i++)
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
                    int Position_PlayersView = GameState.PawnPosition[i][k];
                    //Pöppel steht auf einem normalen Feld
                    if (Position_PlayersView >= 0 && Position_PlayersView <= 47)
                    {
                        BoardView[(Position_PlayersView + (12 * i)) % 48] = new int[2] { i, k };
                    }
                    //Pöppel steht auf einem Zielfeld
                    else if (Position_PlayersView >= 48)
                    {
                        BoardView[Position_PlayersView - 48 + 50 + 5 + (10 * i)] = new int[2] { i, k };
                    }
                    //Pöppel hat das Haus noch nicht verlassen
                    else if (Position_PlayersView == -1)
                    {
                        BoardView[50 + (10 * i) + k] = new int[2] { i, k };
                    }
                }
            }
        }

        public void RefreshView_DiePhase()
        {
            Change_BoardView();
            Text_Die = GameState.DieValue.ToString();
            for (int i = 0; i < 4; i++)
            {
                if (GameState.PawnOptions[i] != 90)
                {
                    Pawn_Active[(GameState.ActivePlayer * 4) + i] = "orange";
                }  
            }
            //Hover soll anzeigen, wo der Pöppel hingeht
            OnNotifyPropertyChanged("Pawn_Active");
            OnNotifyPropertyChanged("Text_Die");
        }

        public void RefreshView_MovePhase()
        {
            Change_BoardView();
            Text_Die = "!";
            for (int i = 0; i < BoardView.Count; i++)
            {
                if (BoardView[i].Min() < 9)
                {
                    int player = BoardView[i][0];
                    int pawnID = BoardView[i][1];
                    Pawn_PositionX[(player * 4) + pawnID] = Field_GridX[i];
                    Pawn_PositionY[(player * 4) + pawnID] = Field_GridY[i];
                }
            }
            for (int pawnID = 0; pawnID < 3; pawnID++)
            {
                for (int player = 0; player < 4; player++)
                {
                    Pawn_Active[(player * 4) + pawnID] = "black";
                }
            }
                OnNotifyPropertyChanged("Text_Die");
            OnNotifyPropertyChanged("Pawn_PositionX");
            OnNotifyPropertyChanged("Pawn_PositionY");
        }
        #endregion

        #region Commands
        public void DiePhase_Start(object obj)
        {
            Game.DiePhase();
            RefreshView_DiePhase();
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
            GameState.PawnPosition[0][0] = -1;
            GameState.PawnPosition[0][1] = 26;
            GameState.PawnPosition[0][2] = 2;
            GameState.PawnPosition[0][3] = 49;
            GameState.ActivePlayer = 0;
            GameState.DieValue = 1;
            RefreshView_MovePhase();

        }
        #endregion
    }
}

