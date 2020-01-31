using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ludo._20_Data;
using Ludo._60_ViewModel;

namespace Ludo._40_Model
{
    public class Game_Main
    {
        #region Eigenschaften und Konstruktor
        public GameState GameState { get; set; }
        public VM_MainWindow VM { get; set; }
        public Pawn_Movement PawnMovement { get; set; }


        public Game_Main(VM_MainWindow vm)
        {
            this.VM = vm;
            this.GameState = VM.GameState;
            this.PawnMovement = new Pawn_Movement(this);
        }
        #endregion

        #region Spielmechaniken
        /// <summary>
        /// Aufführen des Würfelwurfes und anschliessendes Aufrufen der Funktion
        ///     CheckWhichPawnCanMove, die in Abhängigkeit des Würfelergebnisses überprüft welcher Pöppel überhaupt ziehen darf und angibt wohin und
        ///         und Aufrufen der Funktion
        ///     RefreshView_DiePhase, die die View aktualisiert.
        /// </summary>
        public void DiePhase()
        {
            GameState.InDiePhase = false;
            RollDie();
            GameState.PawnOptions = PawnMovement.CheckWhichPawnCanMove();
            VM.RefreshView_DiePhase();
            //sollte es keinen Wert kleiner 90 geben, sokann der Spieler mit dem Würfelergebniss keinen gültigen Zug ausführen.
            if (GameState.PawnOptions.Min() == 90)
            {
                GameState.AmountOfRetries++;
                Player_Pass();
            }
        }

        /// <summary>
        /// Es wird eine zufällige Zahl zwischen 1 und 6 bestimmt, die dann als das aktuelle Würfelergebniss in GameState gespeichert wird.
        /// </summary>
        public void RollDie()
        {
            Random rnd = new Random();
            GameState.DieValue = rnd.Next(1, 7);
        }

        /// <summary>
        /// Ausführen eines Zuges mit dem Pöppel "pawnID" von Spieler "PlayerID".
        /// hierbei wird nur dann ein zug ausgeführt, wenn dier Wert des Pöppels in GameState.PawnOptions undgleich 90 ist.
        /// 
        /// Dann wird Die Methode MovePawn des Objektes PawnMovement aufgerufen, welche den Zugausführt.
        /// Anschliessend werden die Siegbedingungen überprüft,
        ///         der aktive Spieler gewechselt, sofern keine 6 geworfen wurde und
        ///         überprüft, ob eine KI dran ist, die dann den zug ausführt.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="pawnID"></param>
        public void MovePhase(int player, int pawnID)
        {
            GameState.InDiePhase = true;
            if (player == GameState.ActivePlayer)
            {               
                if (GameState.PawnOptions[pawnID] != 90)
                {
                    PawnMovement.MovePawn(pawnID);
                    VictoryConditions();
                    VM.RefreshView_MovePhase();

                    if (GameState.DieValue != 6)
                    {
                        ChangePlayer();
                        GameState.AmountOfRetries = 0;
                    }
                 
                    AI_Player();
                }
            }
        }



        /// <summary>
        /// Ein Spieler hat gewonnen, wenn alle seine Pöppel im Ziel sind, also wenn der kleinste Wert des pöppels gleich 52 ist.
        /// Tritt dies ein, wird dies in den Eigenschaften GameOver und Winner der Klasse GameState gespeichert.
        /// Winner enthält dabei die Ziffer des erfolgreichen Spielers.
        /// </summary>
        public void VictoryConditions()
        {
            for (int i = 0; i < 4; i++)
            {
                if (GameState.PawnPosition[i].Min() == 48)
                {
                    GameState.GameOver = true;
                    GameState.Winner = i;
                }
            }
        }

        /// <summary>
        /// Die Ziffer, die den aktiven Spieler anzeigt wird um 1 erhöht, bzw. auf 0 gesetzt, sollte der Speiler 3 seinen Zug gemacht haben.
        /// Die Ziffer wird in der Variable ActivePlayer der Klasse GameState gespeichert.
        /// </summary>
        public void ChangePlayer()
        {
            int newPlayer = -1;
            if (GameState.ActivePlayer >= 3)
            {
                newPlayer = 0;
            }
            else
            {
                newPlayer = GameState.ActivePlayer + 1;
            }
            GameState.ActivePlayer = newPlayer;
        }

        #endregion

        #region Verschiedenes

        public void Player_Pass()
        {
            GameState.InDiePhase = true;
            VM.RefreshView_MovePhase();
            if (GameState.AmountOfRetries < 3)
            {

            }
            else
            {
                ChangePlayer();
                GameState.AmountOfRetries = 0;
            }
            AI_Player();
        }

        /// <summary>
        /// Abhängig vom Wert des Arrays GameState.AI an der Stelle der SpielerID, wird automatisiert ein Zug der KI ausgeführt.
        /// </summary>
        public void AI_Player()
        {
            if (GameState.AI[GameState.ActivePlayer])
            {
                DiePhase();
                AI_Move(GameState.PawnOptions);

            }
        }

        /// <summary>
        /// Die KI bestimmt unter allen Pöppel, die einen gültigen Zug machen können einen zufälligen, dessen Zug dann ausgeführt wird.
        /// Sollte kein Zug möglich sein, so wird die Methode AI_Pass aufgerufen.
        /// </summary>
        /// <param name="move"></param>
        public void AI_Move(int[] move)
        {
            //Die Liste enthält gleich die IDs aller Pöppel, die einen gültigen zug ausführen können
            List<int> Options = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                if (move[i] != 90)
                {
                    Options.Add(i);
                }
            }
            //Züge möglich, also Liste nicht leer.
            if (Options.Count != 0)
            {
                Random rnd = new Random();
                int rndSelect = rnd.Next(0, Options.Count);
                int rndPawn = Options[rndSelect];
                MovePhase(GameState.ActivePlayer, rndPawn);
            }
            //Liste leer, also Passen
            else
            {
                Player_Pass();
            }
        }
        #endregion
    }
}
