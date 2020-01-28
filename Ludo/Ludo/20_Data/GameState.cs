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

        // Die Eigenschaft enthält für jeden Pöppel die Information, wie weit er von seinem EIGENEN Start aus bereits gelaufen ist.
        // Hierbei steht der Wert -1 für "im Haus stehend".
        // Die erste Dimension gibt den Besitzer des Pöppel an, die zweite Dimension die individuelle ID (0-3)
        public int[,] PawnPosition { get; set; }


        #endregion

        #region Konstruktor
        public GameState()
        {
            ActivePlayer = 1;
            PawnPosition = new int[4,4] { { -1,-1,-1,-1 }, { -1, -1, -1, -1 }, { -1, -1, -1, -1 }, { -1, -1, -1, -1 } };
        }


        #endregion

        #region Verworfenes
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

        public string[] Board { get; set; }
        #endregion
    }
}
