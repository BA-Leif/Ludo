using Ludo._40_Model;
using Ludo._60_ViewModel;
using Ludo._20_Data;
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
            VM_MainWindow vm = new VM_MainWindow();
            Game_Main Game = new Game_Main(vm);
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = -1;
            Game.GameState.PawnPosition[1][2] = -1;
            Game.GameState.PawnPosition[1][3] = 0;
            Game.GameState.DieValue = 6;

            int[] expect = new int[4] { 90, 90, 90, 6 };

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
            VM_MainWindow vm = new VM_MainWindow();
            Game_Main Game = new Game_Main(vm);
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = 20;
            Game.GameState.PawnPosition[1][1] = 21;
            Game.GameState.PawnPosition[1][2] = 0;
            Game.GameState.PawnPosition[1][3] = 40;
            Game.GameState.DieValue = 4;

            int[] expect = new int[4] { 90, 90, 4, 90 };

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
            VM_MainWindow vm = new VM_MainWindow();
            Game_Main Game = new Game_Main(vm);
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = 3;
            Game.GameState.PawnPosition[1][2] = -1;
            Game.GameState.PawnPosition[1][3] = 34;
            Game.GameState.DieValue = 1;

            int[] expect = new int[4] { 90, 4, 90, 35 };

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
            VM_MainWindow vm = new VM_MainWindow();
            Game_Main Game = new Game_Main(vm);
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
            VM_MainWindow vm = new VM_MainWindow();
            Game_Main Game = new Game_Main(vm);
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = -1;
            Game.GameState.PawnPosition[1][2] = 47;
            Game.GameState.PawnPosition[1][3] = 46;
            Game.GameState.DieValue = 4;

            int[] expect = new int[4] { 90, 90, 51, 50 };

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

            VM_MainWindow vm = new VM_MainWindow();
            Game_Main Game = new Game_Main(vm);
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = -1;
            Game.GameState.PawnPosition[1][2] = 47;
            Game.GameState.PawnPosition[1][3] = 48;
            Game.GameState.DieValue = 3;

            int[] expect = new int[4] { 90, 90, 90, 51 };

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
            VM_MainWindow vm = new VM_MainWindow();
            Game_Main Game = new Game_Main(vm);
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = -1;
            Game.GameState.PawnPosition[1][2] = 48;
            Game.GameState.PawnPosition[1][3] = 51;
            Game.GameState.DieValue = 2;

            int[] expect = new int[4] { 90, 90, 50, 90 };

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
            VM_MainWindow vm = new VM_MainWindow();
            Game_Main Game = new Game_Main(vm);
            Game.GameState.ActivePlayer = 1;
            Game.GameState.PawnPosition[1][0] = -1;
            Game.GameState.PawnPosition[1][1] = -1;
            Game.GameState.PawnPosition[1][2] = 55;
            Game.GameState.PawnPosition[1][3] = 52;
            Game.GameState.DieValue = 5;

            int[] expect = new int[4] { 90, 90, 90, 90 };

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

        #region MovePawn
        [TestMethod]
        public void MovePawn_NoBeating_Normal()
        {
            //Arrange:
            VM_MainWindow VM = new VM_MainWindow();
            GameState GS = VM.GameState;

            GS.ActivePlayer = 1;
            int PawnID = 2;
            GS.PawnPosition[GS.ActivePlayer][PawnID] = 43;
            GS.PawnOptions = new int[4] { 90, 33, 45, 90 };

            int expected = 45;

            //Act:
            VM.Game.PawnMovement.MovePawn(PawnID);

            //Assert:
            int actual = GS.PawnPosition[GS.ActivePlayer][PawnID];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MovePawn_NoBeating_InGoalArea()
        {
            //Arrange:
            VM_MainWindow VM = new VM_MainWindow();
            GameState GS = VM.GameState;

            GS.ActivePlayer = 3;
            int PawnID = 2;
            GS.PawnPosition[GS.ActivePlayer][PawnID] = 45;
            GS.PawnOptions = new int[4] { 90, 33, 87, 90 };

            int expected = 87;

            //Act:
            VM.Game.PawnMovement.MovePawn(PawnID);

            //Assert:
            int actual = GS.PawnPosition[GS.ActivePlayer][PawnID];
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MovePawn_NoBeating_OutOfHouse()
        {
            //Arrange:
            VM_MainWindow VM = new VM_MainWindow();
            GameState GS = VM.GameState;

            GS.ActivePlayer = 1;
            int PawnID = 0;
            GS.PawnPosition[GS.ActivePlayer][PawnID] = -1;
            GS.PawnOptions = new int[4] { 0, 33, 45, 90 };

            int expected = 0;

            //Act:
            VM.Game.PawnMovement.MovePawn(PawnID);

            //Assert:
            int actual = GS.PawnPosition[GS.ActivePlayer][PawnID];
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void MovePawn_Beating_1()
        {
            //Arrange:
            VM_MainWindow VM = new VM_MainWindow();
            GameState GS = VM.GameState;

            GS.ActivePlayer = 0;
            int PawnID = 0;
            int otherPlayer = 1;
            int otherPawnID = 3;
            GS.PawnPosition[GS.ActivePlayer][PawnID] = 8;
            GS.PawnOptions = new int[4] { 13, 33, 45, 90 };
            GS.PawnPosition[otherPlayer][otherPawnID] = 1;
            int expected = -1;

            //Act:
            VM.Game.PawnMovement.MovePawn(PawnID);

            //Assert:
            int actual = GS.PawnPosition[otherPlayer][otherPawnID];
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void MovePawn_Beating_beimRauskommen()
        {
            //Arrange:
            VM_MainWindow VM = new VM_MainWindow();
            GameState GS = VM.GameState;

            GS.ActivePlayer = 1;
            int PawnID = 0;
            int otherPlayer = 0;
            int otherPawnID = 0;
            GS.PawnPosition[GS.ActivePlayer][PawnID] = -1;
            GS.PawnOptions = new int[4] { 0, 33, 45, 90 };
            GS.PawnPosition[otherPlayer][otherPawnID] = 12;
            int expected = -1;

            //Act:
            VM.Game.PawnMovement.MovePawn(PawnID);

            //Assert:
            int actual = GS.PawnPosition[otherPlayer][otherPawnID];
            Assert.AreEqual(expected, actual);
        }
        #endregion

    }
}
