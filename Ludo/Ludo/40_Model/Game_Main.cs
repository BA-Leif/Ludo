using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ludo._20_Data;

namespace Ludo._40_Model
{
    public class Game_Main
    {
        #region Eigenschaften und Konstruktor
        public GameState GameState { get; set; }
        public Pawn_Movement PawnMovement { get; set; }


        public Game_Main()
        {
            this.GameState = new GameState();
            this.PawnMovement = new Pawn_Movement(this);
        }
        #endregion

        #region Spielmechaniken

        public void MovePhase()
        {

        }

        public void movePawn(int pawnID, int targetSpot)
        {
            GameState.PawnPosition[GameState.ActivePlayer][pawnID] = targetSpot;

        }


        public void BeatPawn(int pawnID, int targetSpot)
        {

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
                if (GameState.PawnPosition[i].Min() == 52)
                {
                    GameState.GameOver = true;
                    GameState.Winner = i;
                }
            }
        }
        #endregion



        #region Verschiedenes
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

        /// <summary>
        /// Es wird eine zufällige Zahl zwischen 1 und 6 bestimmt, die dann als das aktuelle Würfelergebniss in GameState gespeichert wird.
        /// </summary>
        public void RollDie()
        {
            Random rnd = new Random();
            GameState.DieValue = rnd.Next(1, 7);
        }


        public void AI_Move(int[] move)
        {
            List<int> Options = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                if (move[i] != 100)
                {
                    Options.Add(i);
                }
            }
            Random rnd = new Random();
            int rndSelect = rnd.Next(0, Options.Count);
            int rndPawn = Options[rndSelect];
            movePawn(rndPawn, move[rndPawn]);
        }


        public void RefreshViewModel()
        {

        }
        #endregion
    }
}
