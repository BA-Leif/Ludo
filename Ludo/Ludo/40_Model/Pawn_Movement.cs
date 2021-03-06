﻿using System;
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
        /// Überprüft, für jeden der Pöppel des aktiven Spielers, ob diese einen gültigen Zug amchen können.
        /// Dies ist abhängig vom jeweiligen Standort und dem Würfelergebniss.
        /// 
        /// Am Ende gibt die Funktion ein Array aus, welches die jeweiligen Zielfelder - so ein Zug möglich ist- für jeden Pöppel erthält.
        /// Sollte keine Zug möglich sein, so ist der Eintrag eine "90".
        /// </summary>
        /// <returns></returns>
        public int[] CheckWhichPawnCanMove()
        {
            //Initialize  the output variable with invaalid moves for each pawn.
            int[] move = new int[4] { 90, 90, 90, 90};

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
            if ((move.Min() == 90))
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
                            move[i] = 90;
                        }
                    }
                    //liegt das Zeilfeld zwischen 0 und 51, so ist der Zug erlaubt.
                    //  (theoretische würden Figuren geschlagen die dort stehen [nicht hier implementiert])
                    else if (move[i] <= 47)
                    {
                        
                    }
                    //liegt das Ziel über 55, so würde der Pöppel übers Ziel hinausschießen. -> ungültiger Zug
                    else if (move[i] > 51)
                    {
                        move[i] = 90;
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
                                GS.PawnPosition[GS.ActivePlayer][k] >= 48 &&
                                //kein Pöppel darf im Ziel überholt werden, 
                                //  heisst die neue Position darf nicht über der eines anderen liegen
                                GS.PawnPosition[GS.ActivePlayer][k] <= move[i])
                            {
                                move[i] = 90;
                            }
                        }
                    }
                }
            }
            return move;
        }

        /// <summary>
        /// Bewegt den Pöppel auf das in "PawnOptions" zugewiesene Feld. Davor wird überprüft, 
        ///     ob das Feld besetzt war und dieser Pöppel gegebnenfalls ins Haus gesetzt.
        /// </summary>
        /// <param name="pawnID"></param>
        public void MovePawn(int pawnID)
        {
            int targetSpot = GS.PawnOptions[pawnID];
            BeatPawn(targetSpot);
            GS.PawnPosition[GS.ActivePlayer][pawnID] = targetSpot;
        }

        /// <summary>
        /// Überprüft für das in targetSpot angegebene Feld, ob dieses Besetzt ist. 
        /// Ist dies der Fall wird der dort stehende Pöppel in sein Haus zurückgesetzt.
        /// Die Variable targetSpor beschreibt das Feld aus Sicht des aktiven Spielers.
        /// </summary>
        /// <param name="targetSpot"></param>
        public void BeatPawn(int targetSpot)
        {
            //Überprüfung nur relevant, wenn das Feld ein Standerdfeld ist, also weder ein Ziel, noch ein Hausfeld
            if (targetSpot != -1 &&
                targetSpot <= 47)
            {
                //durchgehen alle Spieler
                for (int otherPlayer = 0; otherPlayer < 4; otherPlayer++)
                {
                    //Bestimmen der Feldbeschreibung aus Sicht des anderen Spielers
                    int targetSpot_FromOtherPlayersView = (targetSpot + (48 + (12 * GS.ActivePlayer)) + (48 - (12 * otherPlayer))) % 48;
                    //Durchgehen aller Pöppel des anderen Spielers
                    for (int otherPawnID = 0; otherPawnID < 4; otherPawnID++)
                    {
                        //Überprüfen ergibt auch hierr nur Sinn, wenn dieser auf einem Standardfeld ist.
                        int otherSpot = GS.PawnPosition[otherPlayer][otherPawnID];
                        if (otherSpot != -1 &&
                            otherSpot <= 47)
                        {
                            //zurücksetzen des anderen Pöppels, sofern das Zielfeld und das Feld des betrachtenden Pöppels 
                            //  aus sich des anderen Spielers übereinstimmen.
                            if (targetSpot_FromOtherPlayersView == otherSpot)
                            {
                                GS.PawnPosition[otherPlayer][otherPawnID] = -1;
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
