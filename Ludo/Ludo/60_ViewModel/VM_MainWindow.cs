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
        //      0 - 47         StandardFelder des Rundkurses, wobei mit der Uhr durchgezählt wird.
        //                          Board[0] ist gleich dem Startfeld des ersten Spielers unten links.
        //                          Board[47] ist gleich dem Endfeld des ersten Spielers.
        //      
        //  10x+50 - 10x+53    Häuser der Spieler, wobei x der Speilerzahl (0-3) entspricht.
        //  10x+55 - 10x+58    Zielfelder der Spieler, wobei x der Speilerzahl (0-3) entspricht.
        //
        //  Die Einträge können sein
        //      {9, 9}: ein leeres Feld
        //      {n, x}: Figur n von Spieler x steht auf dem Feld.
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
        public string Text_Information { get; set; }

        //Commands
        public ICommand Cmd_TestClick { get; set; }
        public ICommand Cmd_DiePhase { get; set; }
        public ICommand Cmd_MovePhase { get; set; }
        public ICommand Cmd_NewGame { get; set; }
        public bool CanEnable_NewGame { get; set; }

        //FieldButtons
        //  Die Arrays haben eine Länge von 90 (siehe BoardView) 
        //x- bzw. Y-Position des Feldes im Gitter der View
        public int[] Field_GridX { get; }
        public int[] Field_GridY { get; }
            //Farbe jedes einzelnen Feldes
        public string[] Field_Color { get; set; }

        //Pawns
        // Die Arrays haben eine Länge von 16, wobei jedem Eintrag genau ein Pöppel zugeordnet werden kann.
        //          Index=(SpielerID * 4) + PöppelID
            //X- bzw. Y-Position des Pöppels
            //  diese ist immer gleich der Position eines Feldes
        public int[] Pawn_PositionX { get; set; }
        public int[] Pawn_PositionY { get; set; }
            //enthält Farbeninformationen über den Rand des Pöppels (orange: Zug möglich)
        public string[] Pawn_Active { get; set; }

        //Konstruktor
        public VM_MainWindow()
        {
            //Initialisieren von Game und GameState
            GameState = new GameState();
            Game = new Game_Main(this);

            //soll eine Konstante sein
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

            //Initialisieren der BoardView und setzen alle Feldfarben.
            BoardView = new List<int[]>();
            Field_Color = new string[90];
            for (int i = 0; i < 90; i++)
            {
                BoardView.Add(new int[2] { 9, 9 });
                Field_Color[i] = "beige";
            }

            //Pöppel an ihre Startpositionen setzen
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
            

            //Farben bestimmen
            Color_Background = "lightgray";
            Color_EmptyField = "Beige";
            Color_P1 = "red";
            Color_P2 = "blue";
            Color_P3 = "yellow";
            Color_P4 = "green";


            //Texte
            Text_Die = "X";
            Text_Information = "Hier stehen Dinge.";

            //Commands
            Cmd_TestClick = new RelayCommand(TestClick);
            Cmd_DiePhase = new RelayCommand(DiePhase_Start, parameter => GameState.InDiePhase);
            Cmd_MovePhase = new RelayCommand(MovePhase_Start);
            CanEnable_NewGame = true;
            Cmd_NewGame = new RelayCommand(NewGame, parameter => CanEnable_NewGame);

            RefreshView_MovePhase();
        }
        #endregion

        #region View & VM Beziehungen
        //View über Änderungen informieren
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Diese Funktion erstellt anhand der Eigenschaft "PawnPositions" des Objektes "GameState" eine globale Beschreibung der Pöppelpositionen.
        /// 
        /// </summary>
        public void Change_BoardView()
        {
            //jedes Feld wird zurückgesetzt
            for (int i = 0; i < 90; i++)
            {
                if (BoardView[i].Max() < 9)
                {
                    BoardView[i] = new int[2] { 9, 9 };
                }
            }
            //betrachten eines jeden Pöppels
            for (int playerID = 0; playerID < 4; playerID++)
            {
                for (int pawnID = 0; pawnID < 4; pawnID++)
                {
                    // Position des Pöppels, als Entfernung zum eigenen Start
                    int Position_PlayersView = GameState.PawnPosition[playerID][pawnID];
                    //Pöppel steht auf einem normalen Feld
                    if (Position_PlayersView >= 0 && Position_PlayersView <= 47)
                    {
                        BoardView[(Position_PlayersView + (12 * playerID)) % 48] = new int[2] { playerID, pawnID };
                    }
                    //Pöppel steht auf einem Zielfeld
                    else if (Position_PlayersView >= 48)
                    {
                        BoardView[Position_PlayersView - 48 + 50 + 5 + (10 * playerID)] = new int[2] { playerID, pawnID };
                    }
                    //Pöppel hat das Haus noch nicht verlassen
                    else if (Position_PlayersView == -1)
                    {
                        BoardView[50 + (10 * playerID) + pawnID] = new int[2] { playerID, pawnID };
                    }
                }
            }
        }

        /// <summary>
        /// Aktualisiert die View nach dem Würfelwurf.
        ///     Anzeige des Würfelergebnisses und Hervorhebung der Pöppel, die ziehen können.
        /// </summary>
        public void RefreshView_DiePhase()
        {
            Change_BoardView();
            //Anzeige des Würfelergebnisses
            Text_Die = GameState.DieValue.ToString();
            // nur Pöppel, deren Wert in der Variable GameState.PawnOptions ungleich 90 sind können sich bewegen
            for (int i = 0; i < 4; i++)
            {
                if (GameState.PawnOptions[i] != 90)
                {
                    Pawn_Active[(GameState.ActivePlayer * 4) + i] = "orange";
                }
            }
            //
            //Hover soll anzeigen, wo der Pöppel hingeht
            //
            //Informieren der View, dass eine Änderung erfolgt ist.
            SetInformation();
            OnNotifyPropertyChanged("Pawn_Active");
            OnNotifyPropertyChanged("Text_Die");
        }

        /// <summary>
        /// Aktualisiert die View nach dem Würfelwurf.
        ///     inbesondere die Bewegung der Pöppel.
        /// </summary>
        public void RefreshView_MovePhase()
        {
            Change_BoardView();
            //Löschung des vorigen Würfelergebnisses
            Text_Die = "!";
            //Ändern der Pöppelpositionen anhand der Variable BoardView
            for (int i = 0; i < BoardView.Count; i++)
            {
                //Es lohnen sich nur Felder zu betrachten, die nicht leer sind (leer: {9,9})
                if (BoardView[i].Min() < 9)
                {
                    int player = BoardView[i][0];
                    int pawnID = BoardView[i][1];
                    Pawn_PositionX[(player * 4) + pawnID] = Field_GridX[i];
                    Pawn_PositionY[(player * 4) + pawnID] = Field_GridY[i];
                }
            }
            SetInformation();
            //visuelle deaktivierung aller Pöppel
            for (int pawnID = 0; pawnID < 4; pawnID++)
            {
                for (int player = 0; player < 4; player++)
                {
                    Pawn_Active[(player * 4) + pawnID] = "black";
                }
                GameState.PawnOptions[pawnID] = 90;
            }
            //Informieren der View, dass eine Änderung erfolgt ist.
            
            OnNotifyPropertyChanged("Text_Die");
            OnNotifyPropertyChanged("Pawn_PositionX");
            OnNotifyPropertyChanged("Pawn_PositionY");
            OnNotifyPropertyChanged("Pawn_Active");
        }
        #endregion

        #region Commands
        /// <summary>
        /// Diese Fiunktion wird gestartet, wenn ein Spieler das WürfelFeld anklickt.
        /// Anschliessend wird die Funktion DiePhase vom Objekt "Game" aufgerufen, welche ein zufälliges Würfelergebnis bestimmt
        ///     und überprüfen lässt, ob und wohin die Pöppel des aktiven Spielers gehen können.
        /// </summary>
        /// <param name="obj"></param>
        public void DiePhase_Start(object obj)
        {
            Game.DiePhase();
        }

        /// <summary>
        /// Diese Funktion wird gestartet, wenn ein Spieler einen Pöppel klickt.
        /// Anschliessend wird die Funktion MovePhase vom Objekt "Game" aufgerufen, welche nach Überprüfung, ob dieser pöppel bewegt werden darf,
        ///     diesen bewegt. Anschliessend werden die Siegbedingungen überprüft, der aktive Spieler gewechselt und eventuell KI-Züge ausgeführt.
        ///     
        /// Als Paramater wird von der View die Spieler- und PöppelID des angeklickten Pöppels übergeben.
        /// </summary>
        /// <param name="obj"></param>
        public void MovePhase_Start(object obj)
        {
            string Pawn_Information = obj as string;
            int playerID = Int32.Parse(Pawn_Information.Substring(0, 1));
            int pawnID = Int32.Parse(Pawn_Information.Substring(1, 1));
            Game.MovePhase(playerID, pawnID);

        }


        public void NewGame(object obj)
        {
            NewGameWindow newGame = new NewGameWindow(this);
            newGame.Show();

        }

        #endregion

        #region Hilfsfunktionen
        /// <summary>
        /// Die Funktion wählt eine Farbe anhand der übergebenen SpielerID.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
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


        public void SetInformation()
        {
            string newInformation = "";
            int activePlayer = GameState.ActivePlayer + 1;
            newInformation += "Spieler " + activePlayer.ToString() + " ist am Zug.\r\n";
            if (GameState.DieValue == 6 && !GameState.InDiePhase)
            {
                newInformation += "Glückwunsch. Durch die 6 hast du gleich einen weiteren Zug.\r\n";
            }
            if (GameState.PawnOptions.Min() == 90 && GameState.InDiePhase)
            {
                newInformation += "Du konntest leider keinen Zug ausführen. Du hast noch " + (3 - GameState.AmountOfRetries).ToString() + " weitere Versuche.\r\n";
            }
            Text_Information = newInformation;
            OnNotifyPropertyChanged("Text_Information");
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

