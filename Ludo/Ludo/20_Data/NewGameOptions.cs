using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo._20_Data
{
    public class NewGameOptions
    {
        public string[] PlayerNames { get; set; }

        public int[] Player1Color { get; set; }
        public int[] Player2Color { get; set; }
        public int[] Player3Color { get; set; }
        public int[] Player4Color { get; set; }
        public int[][] PlayerColors
        {
            get {
                int[][] output = new int[4][];
                output[0] = Player1Color;
                output[1] = Player2Color;
                output[2] = Player3Color;
                output[3] = Player4Color;
                return output; }
            set { }
        }


        public bool[] PlayerAI { get; set; }

        public NewGameOptions()
        {
            PlayerNames = new string[] { "Linn", "Peter", "Reeta", "Joost" };
            //PlayerColors = new string[4];
            //PlayerColors[0] = "150,0,200";
            //PlayerColors[1] = "0,0,200";
            //PlayerColors[2] = "100,100,0";
            //PlayerColors[3] = "0,200,0";
            Player1Color = new int[3] { 150, 0, 200 };
            Player2Color = new int[3] { 0, 0, 200 };
            Player3Color = new int[3] { 100, 100, 0 };
            Player4Color = new int[3] { 0, 200, 0 };
            PlayerAI = new bool[4] { false, true, true, true };
        }


    }
}
