using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ludo._60_ViewModel;

namespace Ludo._60_ViewModel
{
    class VM_NewGame
    {
        #region Eigenschaften und Konstruktor
        //Spieleroptionen
        public string[] PlayerNames { get; set; }
        public string[] PlayerColors { get; set; }
        public bool[] PlayerAI { get; set; }


        public VM_NewGame()
        {
            PlayerNames = new string[] { "Linn", "Peter", "Reeta", "Joost" };
            PlayerColors = new string[4];
            PlayerColors[0] = "200,0,0";
            PlayerColors[1] = "0,0,200";
            PlayerColors[2] = "100,100,0";
            PlayerColors[3] = "0,200,0";
            PlayerAI = new bool[] { false, true, true, true };

        }


        #endregion
    }
}
