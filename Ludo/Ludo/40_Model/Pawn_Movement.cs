using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ludo._20_Data;

namespace Ludo._40_Model
{
    class Pawn_Movement
    {
        #region Eigenschaften und Konstruktor
        public Game_Main Game { get; set; }
        public GameState GameState { get; set; }

        public Pawn_Movement(Game_Main game)
        {
            Game = game;
            GameState = Game.GameState;
        }
        #endregion

        #region Methods
        public int[] WhichPawnCanMove()
        {
            //Initialize the output variable with invalid moves for each pawn.
            int[] moves = new int[4] { 100, 100, 100, 100};

            for (int i = 0; i < 4; i++)
            {
                if (true)
                {

                }
            }


            return moves;
        }
        #endregion
    }
}
