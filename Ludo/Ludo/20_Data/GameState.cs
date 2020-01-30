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
        public string[] PlayerNames { get; set; }
        public bool[] AI { get; set; }
        public int ActivePlayer { get; set; }
        public int DieValue { get; set; }
        public bool GameOver { get; set; }
        public int Winner { get; set; }
        public bool InDiePhase { get; set; }

        // Die Eigenschaft enthält für jeden Pöppel die Information, wie weit er von seinem EIGENEN Start aus bereits gelaufen ist.
        // Hierbei steht der Wert -1 für "im Haus stehend".
        // Die erste Dimension gibt den Besitzer des Pöppel an, die zweite Dimension die individuelle ID (0-3)
        public int[][] PawnPosition { get; set; }

        public int[] PawnOptions { get; set; }


        #endregion

        #region Konstruktor
        public GameState()
        {
            ActivePlayer = 1;
            PawnPosition = new int[4][];
            PawnPosition[0] = new int[4] { -1, -1, -1, -1 };
            PawnPosition[1] = new int[4] { -1, -1, -1, -1 };
            PawnPosition[2] = new int[4] { -1, -1, -1, -1 };
            PawnPosition[3] = new int[4] { -1, -1, -1, -1 };
            DieValue = 1;
            AI = new bool[4] { false, true, true, true};
            PlayerNames = new string[4] { "Tom", "tOm", "toM", "TOM" };
            GameOver = false;
            Winner= 100;
            InDiePhase = true;
            PawnOptions = new int[4] { 100, 100, 100, 100 };

        }


        #endregion

        #region Verworfenes
        
        #endregion
    }
}
