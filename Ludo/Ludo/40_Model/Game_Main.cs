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


        public Game_Main()
        {
            this.GameState = new GameState();
        }
        #endregion

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
    }
}
