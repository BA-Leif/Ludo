using Ludo._60_ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ludo_TestProject
{

    [TestClass]
    public class VM_MainTest
    {
        #region BoardView
        [TestMethod]
        public void BoardView_AllInHouse()
        {
            //Arrange:
            VM_MainWindow VM = new VM_MainWindow();
            
            VM.GameState.PawnPosition[0] = new int[4] { -1, -1, -1, -1 };
            VM.GameState.PawnPosition[1] = new int[4] { -1, -1, -1, -1 };
            VM.GameState.PawnPosition[2] = new int[4] { -1, -1, -1, -1 };
            VM.GameState.PawnPosition[3] = new int[4] { -1, -1, -1, -1 };

            List<int[]>  expected = new List<int[]>();
            for (int i = 0; i < 100; i++)
            {
                expected.Add(new int[2] { 9, 9 });
            }
            for (int i = 0; i < 4; i++)
            {
                expected[60 + (10 * i) + 0] = new int[2] { i, 0 };
                expected[60 + (10 * i) + 1] = new int[2] { i, 1 };
                expected[60 + (10 * i) + 2] = new int[2] { i, 2 };
                expected[60 + (10 * i) + 3] = new int[2] { i, 3 };
            }

            //Act:
            VM.Change_BoardView();

            //Assert:
            List<int[]> actual = VM.BoardView;
            string expected_string = "";
            string actual_string = "";
            for (int i = 0; i < 100; i++)
            {
                expected_string = expected_string + expected[i][0].ToString() + "," + expected[i][1].ToString() + "|";
                actual_string = actual_string + actual[i][0].ToString() + "," + actual[i][1].ToString() + "|";
            }
            Assert.AreEqual(expected_string, actual_string);
        }

        [TestMethod]
        public void BoardView_AllInGoal()
        {
            //Arrange:
            VM_MainWindow VM = new VM_MainWindow();
            VM.GameState.PawnPosition[0] = new int[4] { 52,53,54,55 };
            VM.GameState.PawnPosition[1] = new int[4] { 55,53,54,52 };
            VM.GameState.PawnPosition[2] = new int[4] { 55,54,53,52 };
            VM.GameState.PawnPosition[3] = new int[4] { 54,52,53,55 };

            List<int[]> expected = new List<int[]>();
            for (int i = 0; i < 100; i++)
            {
                expected.Add(new int[2] { 9, 9 });
            }
            expected[60 + 5] = new int[2] { 0, 0 };
            expected[61 + 5] = new int[2] { 0, 1 };
            expected[62 + 5] = new int[2] { 0, 2 };
            expected[63 + 5] = new int[2] { 0, 3 };

            expected[73 + 5] = new int[2] { 1, 0 };
            expected[71 + 5] = new int[2] { 1, 1 };
            expected[72 + 5] = new int[2] { 1, 2 };
            expected[70 + 5] = new int[2] { 1, 3 };

            expected[83 + 5] = new int[2] { 2, 0 };
            expected[82 + 5] = new int[2] { 2, 1 };
            expected[81 + 5] = new int[2] { 2, 2 };
            expected[80 + 5] = new int[2] { 2, 3 };

            expected[92 + 5] = new int[2] { 3, 0 };
            expected[90 + 5] = new int[2] { 3, 1 };
            expected[91 + 5] = new int[2] { 3, 2 };
            expected[93 + 5] = new int[2] { 3, 3 };


            //Act:
            VM.Change_BoardView();

            //Assert:
            List<int[]> actual = VM.BoardView;
            string expected_string = "";
            string actual_string = "";
            for (int i = 0; i < 100; i++)
            {
                expected_string = expected_string + expected[i][0].ToString() + "," + expected[i][1].ToString() + "|";
                actual_string = actual_string + actual[i][0].ToString() + "," + actual[i][1].ToString() + "|";
            }
            Assert.AreEqual(expected_string, actual_string);
        }

        [TestMethod]
        public void BoardView_Mixed()
        {
            //Arrange:
            VM_MainWindow VM = new VM_MainWindow();
            VM.GameState.PawnPosition[0] = new int[4] { 52, 34, 54, 55 };
            VM.GameState.PawnPosition[1] = new int[4] { 55, 53, 9, 52 };
            VM.GameState.PawnPosition[2] = new int[4] { 55, 54, 53, 0 };
            VM.GameState.PawnPosition[3] = new int[4] { 11, 52, 43, 55 };

            List<int[]> expected = new List<int[]>();
            for (int i = 0; i < 100; i++)
            {
                expected.Add(new int[2] { 9, 9 });
            }
            expected[60+5] = new int[2] { 0, 0 };
            expected[34] = new int[2] { 0, 1 };
            expected[62+5] = new int[2] { 0, 2 };
            expected[63+5] = new int[2] { 0, 3 };
                       
            expected[73+5] = new int[2] { 1, 0 };
            expected[71+5] = new int[2] { 1, 1 };
            expected[9+13] = new int[2] { 1, 2 };
            expected[70+5] = new int[2] { 1, 3 };
                       
            expected[83+5] = new int[2] { 2, 0 };
            expected[82+5] = new int[2] { 2, 1 };
            expected[81+5] = new int[2] { 2, 2 };
            expected[26] = new int[2] { 2, 3 };
                       
            expected[50] = new int[2] { 3, 0 };
            expected[90+5] = new int[2] { 3, 1 };
            expected[30] = new int[2] { 3, 2 };
            expected[93+5] = new int[2] { 3, 3 };


            //Act:
            VM.Change_BoardView();

            //Assert:
            List<int[]> actual = VM.BoardView;
            string expected_string = "";
            string actual_string = "";
            for (int i = 0; i < 100; i++)
            {
                expected_string = expected_string + expected[i][0].ToString() + "," + expected[i][1].ToString() + "|";
                actual_string = actual_string + actual[i][0].ToString() + "," + actual[i][1].ToString() + "|";
            }
            Assert.AreEqual(expected_string, actual_string);
        }

        #endregion
    }
}
