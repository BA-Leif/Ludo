using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo._20_Data
{
    public class GameState
    {
        #region Eigenschaften
        //enthält die Namen der Spieler
        public string[] PlayerNames { get; set; }
        //Enthält Informationen, ob und an wlechen Stellen eine KI mitspielt (true: KI spielt an Stelle ... mit)
        public bool[] AI { get; set; }
        //enthält die ID des Spielers, der gerade am Zug ist
        public int ActivePlayer { get; set; }
        //enthält das aktuelle Würfelergebniss
        public int DieValue { get; set; }
        //Der Wert ist TRUE, wenn ein Spieler gewonnen hat
        public bool GameOver { get; set; }
        //enthält die ID des Siegers
        public int Winner { get; set; }
        //Spiel ist in der Würfel-Phase (TRUE) oder der Zug-Phase (FALSE)
        public bool InDiePhase { get; set; }

        // Die Eigenschaft enthält für jeden Pöppel die Information, wie weit er von seinem EIGENEN Start aus bereits gelaufen ist.
        // Hierbei steht der Wert -1 für "im Haus stehend".
        // Die erste Dimension gibt den Besitzer des Pöppel an, die zweite Dimension die individuelle ID (0-3)
        public int[][] PawnPosition { get; set; }

        //Die Eignschaft enthält für jeden der vier Pöppel des aktiven Spielers, wohin er aufgrund des aktuellen Würfelergebnisses ziehen kann.
        //    Das Ziel ist als Entfernung vom EIGENEN Start angegeben
        //    Ist der Wert = 90, so ist kein Zug möglich.
        public int[] PawnOptions { get; set; }


        #endregion

        #region Konstruktor
        public GameState()
        {
            //Spieler 1 beginnt
            ActivePlayer = 0;
            //Alle Pöppel bis auf den Ersten stehen im Haus.
            PawnPosition = new int[4][];
            PawnPosition[0] = new int[4] { 0, -1, -1, -1 };
            PawnPosition[1] = new int[4] { 0, -1, -1, -1 };
            PawnPosition[2] = new int[4] { 0, -1, -1, -1 };
            PawnPosition[3] = new int[4] { 0, -1, -1, -1 };
            DieValue = 0;
            //Standerdeinstellung: Spieler 2-4 sind KI
            AI = new bool[4] { false, true, true, true};
            //StandardNamen
            PlayerNames = new string[4] { "Tom", "tOm", "toM", "TOM" };
            //Spiel ist nicht zuende
            GameOver = false;
            Winner= 100;
            //Spiel beginnt in der Würfelphase
            InDiePhase = true;
            //am Anfang kann kein Pöppel ziehen
            PawnOptions = new int[4] { 90,90,90,90 };
        }
        #endregion
    }
}
