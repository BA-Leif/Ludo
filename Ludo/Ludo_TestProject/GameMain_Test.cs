using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ludo._40_Model;
using Ludo._20_Data;

namespace Ludo_TestProject
{
    [TestClass]
    public class GameMain_Test
    {
        [TestMethod]
        public void PlayerChange_normal()
        {
            //Arrange
            Game_Main Game1 = new Game_Main(1);
            int expected = 2;

            //Act
            Game1.ChangePlayer();

            //Assert
            int actual = Game1.GameState.ActivePlayer;
            Assert.AreEqual(expected, actual, "Failed. actual:" + actual.ToString());
        }

        [TestMethod]
        public void PlayerChange_backToStart()
        {
            //Arrange
            Game_Main Game1 = new Game_Main(3);
            int expected = 0;

            //Act
            Game1.ChangePlayer();

            //Assert
            int actual = Game1.GameState.ActivePlayer;
            Assert.AreEqual(expected, actual, "Failed. actual:" + actual.ToString());         
        }
    }
}
