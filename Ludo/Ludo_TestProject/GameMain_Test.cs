using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ludo._40_Model;
using Ludo._20_Data;
using System.Linq;

namespace Ludo_TestProject
{
    [TestClass]
    public class GameMain_Test
    {
        #region PlayerChange

        [TestMethod]
        public void PlayerChange_normal()
        {
            //Arrange
            Game_Main Game = new Game_Main();
            Game.GameState.ActivePlayer = 1;
            int expected = 2;

            //Act
            Game.ChangePlayer();

            //Assert
            int actual = Game.GameState.ActivePlayer;
            Assert.AreEqual(expected, actual, "Failed. actual:" + actual.ToString());
        }

        [TestMethod]
        public void PlayerChange_backToStart()
        {
            //Arrange
            Game_Main Game = new Game_Main();
            Game.GameState.ActivePlayer = 3;
            int expected = 0;

            //Act
            Game.ChangePlayer();

            //Assert
            int actual = Game.GameState.ActivePlayer;
            Assert.AreEqual(expected, actual, "Failed. actual:" + actual.ToString());
        }
        #endregion

        #region RollDie
        [TestMethod]
        public void RollDie()
        {
            //Arrange:
            Game_Main game = new Game_Main();
            int[] dieValues = new int[30];
            int[] expected = new int[2] { 1, 6 };

            //Act:
            for (int i = 0; i < 30; i++)
            {
                game.RollDie();
                dieValues[i] = game.GameState.DieValue;
            }

            //Assert:
            int[] actual = new int[2] { dieValues.Min(), dieValues.Max() };
            string errorMsg = "Failed: ";
            foreach (int x in actual)
            {
                errorMsg += x.ToString() + ", ";
            }
            CollectionAssert.AreEqual(expected, actual, errorMsg);
        }
        #endregion

        #region VictoryConditions
        [TestMethod]
        public void VictoryConditions_OneWinner()
        {
            //Arrange:
            Game_Main game = new Game_Main();
            game.GameState.PawnPosition[2] = new int[4] { 53, 52, 55, 54 };
            bool expected_GameOver = true;
            int expected_Winner = 2;

            //Act:
            game.VictoryConditions();

            //Assert:
            bool actual_GameOver = game.GameState.GameOver;
            int actual_Winner = game.GameState.Winner;
            Assert.AreEqual(expected_Winner, actual_Winner, "Failed. actual:" + actual_Winner.ToString());
            Assert.AreEqual(expected_GameOver, actual_GameOver, "Failed. actual:" + actual_GameOver.ToString());
        }

        [TestMethod]
        public void VictoryConditions_NoWinner()
        {
            //Arrange:
            Game_Main game = new Game_Main();
            game.GameState.PawnPosition[2] = new int[4] { 53, 52, 51, 54 };
            bool expected_GameOver = false;
            int expected_Winner = 100;

            //Act:
            game.VictoryConditions();

            //Assert:
            bool actual_GameOver = game.GameState.GameOver;
            int actual_Winner = game.GameState.Winner;
            Assert.AreEqual(expected_Winner, actual_Winner, "Failed. actual:" + actual_Winner.ToString());
            Assert.AreEqual(expected_GameOver, actual_GameOver, "Failed. actual:" + actual_GameOver.ToString());
        }
        #endregion
    }
}
