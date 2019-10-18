using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tie_Server;

namespace UnitTests
{
    [TestClass]
    public class FileIO
    {
        [TestMethod]
        public void TestFileRead()
        {
            //Arrange

            //Act
            List<HighScore> scores = Game.GetHighScoresFromFile();

            //Assert
            Assert.IsNotNull(scores);
        }

        [TestMethod]
        public void TestFileWrite()
        {
            //Arrange
            List<HighScore> scores = new List<HighScore>();
            scores.Add(new HighScore("test1", 100));
            scores.Add(new HighScore("test2", 50));

            //Act
            Game.writeHighscoresToFile(scores);
            
            //Assert
        }

        [TestMethod]
        public void TestWriteAndRead()
        {
            //Arrange
            List<HighScore> scores = new List<HighScore>();
            HighScore test1 = new HighScore("test1", 100);
            scores.Add(test1);
            HighScore test2 = new HighScore("test2", 50);
            scores.Add(test2);

            //Act
            Game.writeHighscoresToFile(scores);
            scores = Game.GetHighScoresFromFile();

            //Assert
            Assert.IsTrue(scores.Contains(test1));
            Assert.IsTrue(scores.Contains(test2));
        }
    }
}
