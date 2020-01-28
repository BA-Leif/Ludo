using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo._40_Model
{
    class Game_Main
    {

        private int ChangePlayer(int player)
        {
            int newPlayer = -1;
            if (player >= 3)
            {
                newPlayer = 0;
            }
            else
            {
                newPlayer = player++;
            }
            return newPlayer;
        }
    }
}
