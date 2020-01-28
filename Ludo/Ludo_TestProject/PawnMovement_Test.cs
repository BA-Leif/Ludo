using Ludo._40_Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ludo_TestProject
{
    
    [TestClass]
    public class PawnMovement_Test
    {
        #region PawnCanMove
        [TestMethod]
        public void CanMoveQuestion_StartOccupied_1()
        {
            //Arrange:
            Game_Main Game = new Game_Main();
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = -1;
            Game.GameState.PawnPosition[1][2] = -1;
            Game.GameState.PawnPosition[1][3] = 0;
            Game.GameState.DieValue = 6;

            int[] expect = new int[4] { 100, 100, 100, 6 };

            //Act:
            int[] actual = Game.PawnMovement.CheckWhichPawnCanMove();

            //Assert:
            string errorMsg = "Failed: ";
            foreach (int x in actual)
            {
                errorMsg += x.ToString() + ", ";
            }
            CollectionAssert.AreEqual(actual, expect, errorMsg);

        }

        [TestMethod]
        public void CanMoveQuestion_StartOccupied_2()
        {
            //Arrange:
            Game_Main Game = new Game_Main();
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = 20;
            Game.GameState.PawnPosition[1][1] = 21;
            Game.GameState.PawnPosition[1][2] = 0;
            Game.GameState.PawnPosition[1][3] = 40;
            Game.GameState.DieValue = 4;

            int[] expect = new int[4] { 100, 100, 4, 100 };

            //Act:
            int[] actual = Game.PawnMovement.CheckWhichPawnCanMove();

            //Assert:
            string errorMsg = "Failed: ";
            foreach (int x in actual)
            {
                errorMsg += x.ToString() + ", ";
            }
            CollectionAssert.AreEqual(actual, expect, errorMsg);

        }

        [TestMethod]
        public void CanMoveQuestion_MoveOutby6_1()
        {
            //Arrange:
            Game_Main Game = new Game_Main();
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = 3;
            Game.GameState.PawnPosition[1][2] = -1;
            Game.GameState.PawnPosition[1][3] = 34;
            Game.GameState.DieValue = 1;

            int[] expect = new int[4] { 100, 4, 100, 35 };

            //Act:
            int[] actual = Game.PawnMovement.CheckWhichPawnCanMove();

            //Assert:
            string errorMsg = "Failed: ";
            foreach (int x in actual)
            {
                errorMsg += x.ToString() + ", ";
            }
            CollectionAssert.AreEqual(actual, expect, errorMsg);

        }

        [TestMethod]
        public void CanMoveQuestion_MoveOutby6_2()
        {
            //Arrange:
            Game_Main Game = new Game_Main();
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = 3;
            Game.GameState.PawnPosition[1][2] = -1;
            Game.GameState.PawnPosition[1][3] = 34;
            Game.GameState.DieValue = 6;

            int[] expect = new int[4] { 0, 9, 0, 40 };

            //Act:
            int[] actual = Game.PawnMovement.CheckWhichPawnCanMove();

            //Assert:
            string errorMsg = "Failed: ";
            foreach (int x in actual)
            {
                errorMsg += x.ToString() + ", ";
            }
            CollectionAssert.AreEqual(actual, expect, errorMsg);
        }

        [TestMethod]
        public void CanMoveQuestion_Overtake_1()
        {
            //Arrange:
            Game_Main Game = new Game_Main();
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = -1;
            Game.GameState.PawnPosition[1][2] = 51;
            Game.GameState.PawnPosition[1][3] = 50;
            Game.GameState.DieValue = 4;

            int[] expect = new int[4] { 100, 100, 55, 54 };

            //Act:
            int[] actual = Game.PawnMovement.CheckWhichPawnCanMove();

            //Assert:
            string errorMsg = "Failed: ";
            foreach (int x in actual)
            {
                errorMsg += x.ToString() + ", ";
            }
            CollectionAssert.AreEqual(actual, expect, errorMsg);
        }

        [TestMethod]
        public void CanMoveQuestion_Overtake_2()
        {
            //Arrange:
            Game_Main Game = new Game_Main();
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = -1;
            Game.GameState.PawnPosition[1][2] = 51;
            Game.GameState.PawnPosition[1][3] = 52;
            Game.GameState.DieValue = 3;

            int[] expect = new int[4] { 100, 100, 100, 55 };

            //Act:
            int[] actual = Game.PawnMovement.CheckWhichPawnCanMove();

            //Assert:
            string errorMsg = "Failed: ";
            foreach (int x in actual)
            {
                errorMsg += x.ToString() + ", ";
            }
            CollectionAssert.AreEqual(actual, expect, errorMsg);
        }

        [TestMethod]
        public void CanMoveQuestion_MovementOutOfBounds()
        {
            //Arrange:
            Game_Main Game = new Game_Main();
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = -1;
            Game.GameState.PawnPosition[1][2] = 55;
            Game.GameState.PawnPosition[1][3] = 52;
            Game.GameState.DieValue = 2;

            int[] expect = new int[4] { 100, 100, 100, 54 };

            //Act:
            int[] actual = Game.PawnMovement.CheckWhichPawnCanMove();

            //Assert:
            string errorMsg = "Failed: ";
            foreach (int x in actual)
            {
                errorMsg += x.ToString() + ", ";
            }
            CollectionAssert.AreEqual(actual, expect, errorMsg);
        }

        [TestMethod]
        public void CanMoveQuestion_NoMovePossible()
        {
            //Arrange:
            Game_Main Game = new Game_Main();
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = -1;
            Game.GameState.PawnPosition[1][2] = 55;
            Game.GameState.PawnPosition[1][3] = 52;
            Game.GameState.DieValue = 5;

            int[] expect = new int[4] { 100, 100, 100, 100 };

            //Act:
            int[] actual = Game.PawnMovement.CheckWhichPawnCanMove();

            //Assert:
            string errorMsg = "Failed: ";
            foreach (int x in actual)
            {
                errorMsg += x.ToString() + ", ";
            }
            CollectionAssert.AreEqual(actual, expect, errorMsg);
        }
        #endregion

    }
}
