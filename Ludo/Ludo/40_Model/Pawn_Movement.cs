using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ludo._20_Data;

namespace Ludo._40_Model
{
    public class Pawn_Movement
    {
        #region Eigenschaften und Konstruktor
        public Game_Main Game { get; set; }
        public GameState GS { get; set; }

        public Pawn_Movement(Game_Main game)
        {
            Game = game;
            GS = Game.GameState;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int[] CheckWhichPawnCanMove()
        {
            //Initialize the output variable with invalid moves for each pawn.
            int[] move = new int[4] { 100, 100, 100, 100};

            //Freiräumen des Startfeldes erzwingen
            for (int i = 0; i < 4; i++)
            {
                if (GS.PawnPosition[GS.ActivePlayer][i] == 0)
                {
                    move[i] = GS.DieValue;
                }
            }
            // existiert nun ein Wert kleiner 100, steht ein Pöppel auf dem Startfeld und 
            //      alle anderen Pöppel dürfen nicht ziehen. Ihre Wertte bleiben damit auf 100.
            if ((move.Min() == 100))
            {
                for (int i = 0; i < 4; i++)
                {
                    // Jedem Pöppel wird sein theoretisches Zielfeld zugewiesen. 
                    // Diese Operation wird rückgängig gemacht, sollte der Zug nicht erlaubt/möglich sein.
                    move[i] = GS.PawnPosition[GS.ActivePlayer][i] + GS.DieValue;

                    //Hat der Spieler eine 6 gewürfelt, darf ein belibiger Pöppel auf das Startfeld (Feld 0 aus nicht des Spielers) bewegt werden
                    if (GS.PawnPosition[GS.ActivePlayer][i] == -1)
                    {
                        if (GS.DieValue == 6)
                        {
                            move[i] = 0;
                        }
                        else
                        {
                            move[i] = 100;
                        }
                    }
                    //liegt das Zeilfeld zwischen 0 und 51, so ist der Zug erlaubt.
                    //  (theoretische würden Figuren geschlagen die dort stehen [nicht hier implementiert])
                    else if (move[i] <= 51)
                    {
                        
                    }
                    //liegt das Ziel über 55, so würde der Pöppel übers Ziel hinausschießen. -> ungültiger Zug
                    else if (move[i] > 55)
                    {
                        move[i] = 100;
                    }
                    //das Ziel der Bewegung liegt im Zielbereich. 
                    //      -> Überprüfen, ob ein Pöppel in diesem Bereich übersprungen würde
                    else if (true)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            //  nur die anderen Pöppel des Spieleres testen
                            if (i!= k && 
                                // nur Pöppel überprüfen, die im Ziel sind
                                GS.PawnPosition[GS.ActivePlayer][k] >= 52 &&
                                //kein Pöppel darf im Ziel überholt werden, 
                                //  heisst die neue Position darf nicht über der eines anderen liegen
                                GS.PawnPosition[GS.ActivePlayer][k] <= move[i])
                            {
                                move[i] = 100;
                            }
                        }
                    }
                }
            }
            return move;
        }
        #endregion
    }
}
