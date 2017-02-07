using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SnakeClass snake = new SnakeClass();
            for (int i = 0; i < 147; i++)
            {
                snake.sizeSnake++;
            }
            Assert.IsTrue(snake.size());
        }

        [TestMethod]
        public void TestMethod2()
        {
            SnakeClass snake = new SnakeClass();
            char[,] boxTest = new char[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    boxTest[i, j] = ' ';
                }
            }
            for (int x = 0; x < 14; x++)
            {
                boxTest[x, 0] = 'X';
                boxTest[0, x] = 'X';
                boxTest[13, x] = 'X';
                boxTest[x, 13] = 'X';
            }
            
            if (СhekSum(boxTest, snake.box))
            {
                Assert.IsTrue(true);
            }
            else 
            {
                Assert.IsTrue(false);
            }
        }

        private bool СhekSum(char[,] box1, char[,] box2)
        {
            bool chek = true;
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    if (box1[i, j] != box2[i, j])
                    {
                        chek = false;
                    }
                    else if (!chek)
                    {
                        break;
                    }
                }
                if (!chek)
                {
                    break;
                }
            }
            return chek;
        }

        [TestMethod]
        public void TestMethod3()
        {
            SnakeClass snake = new SnakeClass();
            for (int i = 0; i < 147; i++)
            {
                snake.sizeSnake++;
            }
            Assert.IsTrue(!snake.t1.IsAlive);
        }

        [TestMethod]
        public void TestMethod4()
        {
            SnakeClass snake = new SnakeClass();
            char[,] boxTest = new char[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    boxTest[i, j] = ' ';
                }
            }
            for (int x = 0; x < 14; x++)
            {
                boxTest[x, 0] = 'X';
                boxTest[0, x] = 'X';
                boxTest[13, x] = 'X';
                boxTest[x, 13] = 'X';
            }
            
            Array.Copy(boxTest, snake.boxHelp, boxTest.Length);
            snake.changer();
            snake.boxHelp[snake.p[0],snake.p[1]] = '!';
            snake.finder('!');
            if (snake.arrey[0]!=0 && snake.arrey[1]!=0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            SnakeClass snake = new SnakeClass();
            snake.stopMov();
            Assert.IsTrue(!snake.t1.IsAlive);
        }

    }
}
