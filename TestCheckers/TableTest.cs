using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers;

namespace TestCheckers
{
    /// <summary>
    /// Сводное описание для TableTest
    /// </summary>
    [TestClass]
    public class TableTest
    {
        public TableTest()
        {
            //
            // TODO: добавьте логику конструктора
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        [TestMethod]
        public void TestCreate()
        {
            for (int i = 0; i < 10; i++)
            {
                var table = new Table(i);
                Assert.AreEqual(i, table.size);
            }
        }

        //проверка движения белой шашки
        [TestMethod]
        public void TestMoveSingleChecker()
        {
            var table = new Table(8);
            Checker checker = new Checker(5, 5, true);
            table.AddChecker(checker);

            Assert.AreEqual(false, table.Move(checker, 4, 5));
            Assert.AreEqual(false, table.Move(checker, 5, 6));
            Assert.AreEqual(false, table.Move(checker, 6, 5));
            Assert.AreEqual(false, table.Move(checker, 6, 4));
            Assert.AreEqual(false, table.Move(checker, 5, 4));
            Assert.AreEqual(false, table.Move(checker, 4, 4));
            Assert.AreEqual(true, table.Move(checker, 6, 6));
            Assert.AreEqual(true, table.Move(checker, 5, 7));
        }
        //проверка доски при движении одной белой шашки 
        [TestMethod]
        public void TestMoveSingleChecker1()
        {
            var table = new Table(8);
            Checker checker = new Checker(5, 5, true);
            table.AddChecker(checker);
            table.Move(checker, 6, 6);
            Assert.AreEqual(checker, table.GetChecker(6, 6));
            Assert.AreEqual(null, table.GetChecker(5, 5));
            Assert.AreEqual(6, checker.x);
            Assert.AreEqual(6, checker.y);

            table.Move(checker, 5, 5);
            Assert.AreEqual(checker, table.GetChecker(6, 6));
            Assert.AreEqual(null, table.GetChecker(5, 5));
            Assert.AreEqual(6, checker.x);
            Assert.AreEqual(6, checker.y);
            //Assert.AreEqual(true, false);
        }
        //ролевые игры между 2мя шашками

        [TestMethod]
        public void TestMoveTwoWhiteChekers()
        {
            var table = new Table(8);
            Checker checker1 = new Checker(5, 5, true);
            table.AddChecker(checker1);
            Checker checker2 = new Checker(6, 6, true);
            table.AddChecker(checker2);
            Assert.AreEqual(false, table.Move(checker1, 6, 6));
            Checker checker3 = new Checker(4, 6, true);
            table.AddChecker(checker3);
            Assert.AreEqual(false, table.Move(checker1, 4, 6));
        }

        [TestMethod]
        // doesn't work and should not
        // todo: add more tests with eating, should it be tested like that?
        public void TestMoveTwoDiffChekers()
        {
            var table = new Table(8);
            Checker checker1 = new Checker(5, 5, false);
            table.AddChecker(checker1);
            Checker checker2 = new Checker(6, 6, true);
            table.AddChecker(checker2);
            Assert.AreEqual(true, table.Move(checker1, 7, 7));
            Checker checker3 = new Checker(4, 6, false);
            checker1 = new Checker(5, 5, false);
            table.AddChecker(checker1);
            table.AddChecker(checker3);
            Assert.AreEqual(false, table.Move(checker1, 3, 7));
        }

        [TestMethod]
        public void TestAddChecker()
        {
            var table = new Table(8);

            for (var i = 0; i < table.size; i++)
            {
                for (var j = 0; j < table.size; j++)
                {
                    var white = (new Random()).Next(2) == 1;
                    var checker = new Checker(i, j, white);
                    table.AddChecker(checker);
                    Assert.AreEqual(checker, table.GetChecker(i, j));
                }
            }
            try
            {
                table.AddChecker(new Checker(8, 0, true));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Wrong indexes", e.Message);
            }

            try
            {
                table.AddChecker(new Checker(0, 8, true));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Wrong indexes", e.Message);
            }

            try
            {
                table.AddChecker(new Checker(8, 8, true));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Wrong indexes", e.Message);
            }

            try
            {
                table.AddChecker(new Checker(0, 0, true));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Already Exists", e.Message);
            }
        }

        [TestMethod]
        public void TestGetChecker()
        {
            var table = new Table(8);
            var checker = new Checker(0, 0, true);
            table.AddChecker(checker);

            for (int i = 0; i < table.size; i++)
            {
                for (int j = 0; j < table.size; j++)
                {
                    Assert.AreEqual(i == 0 && j == 0 ? checker : null, table.GetChecker(i, j));
                }
            }
        }

        [TestMethod]
        public void TestRemoveChecker()
        {
            var table = new Table(8);
            var checker = new Checker(0, 0, true);
            table.AddChecker(checker);
            table.RemoveChecker(checker);

            for (int i = 0; i < table.size; i++)
            {
                for (int j = 0; j < table.size; j++)
                {
                    Assert.AreEqual(null, table.GetChecker(i, j));
                }
            }
        }

        [TestMethod]
        public void TestMarkChecker()
        {
            var table = new Table(8);
            var checker = new Checker(0, 0, true);
            table.AddChecker(checker);
            table.MarkChecker(checker);
            Assert.AreEqual(1, table.removedCheckers.Count);
            Assert.AreEqual(checker, table.removedCheckers.First());
        }

        [TestMethod]
        public void TestInit()
        {
            var table = new Table(8);
            table.Init();

            for (var i = 0; i < 3; i++)
                for (var j = i % 2; j < table.size; j += 2)
                {
                    var checker = new Checker(j, i, true);
                    Assert.IsTrue(checker.Equals(table.GetChecker(j, i)));
                }

            for (var i = table.size - 1; i >= table.size - 3; i--)
                for (var j = i % 2; j < table.size; j += 2)
                {
                    var checker = new Checker(j, i, false);
                    Assert.IsTrue(checker.Equals(table.GetChecker(j, i)));
                }
        }

        [TestMethod]
        public void TestCheckQueen()
        {
            var table = new Table(8);

            // Check if queens are made queens
            for (var i = 1; i < table.size; i += 2)
            {
                // red
                table.AddChecker(new Checker(i, table.size - 1, true));
                
                // black
                table.AddChecker(new Checker(i - 1, 0, false));
            }

            foreach (var checker in table.checkers)
            {
                Assert.IsFalse(checker.queen);
                table.CheckQueen(checker);
                Assert.IsTrue(checker.queen);
            }

            table.checkers.Clear();

            // Check if not queens are not made queens
            for (var i = 1; i < table.size - 1; i++)
            {
                for (var j = (i % 2 + 1) % 2; j < table.size; j++)
                {
                    table.AddChecker(new Checker(j, i, new Random().Next(2) == 1));
                }
            }

            foreach (var checker in table.checkers)
            {
                Assert.IsFalse(checker.queen);
                table.CheckQueen(checker);
                Assert.IsFalse(checker.queen);
            }
        }

        /// <summary>
        /// Tests all of the possibilities for CanEatAnotherChecker.
        /// </summary>
        [TestMethod]
        public void TestCanEatAnotherCheckerAll()
        {
            TestCanEatAnotherCheckerColor(true);
            TestCanEatAnotherCheckerColor(false);
        }

        /// <summary>
        /// Tests one option.
        /// </summary>
        /// <param name="red">if set to <c>true</c> [red].</param>
        public void TestCanEatAnotherCheckerColor(bool red)
        {
            var table = new Table(8);
            var checker = new Checker(3, 3, red);
            table.AddChecker(checker);
            table.AddChecker(new Checker(4, 4, !red));
            table.AddChecker(new Checker(4, 2, !red));
            table.AddChecker(new Checker(2, 4, !red));
            table.AddChecker(new Checker(2, 2, !red));

            Assert.IsNotNull(table.CanEatAnotherChecker(checker, 5, 5));
            Assert.IsNotNull(table.CanEatAnotherChecker(checker, 5, 1));
            Assert.IsNotNull(table.CanEatAnotherChecker(checker, 1, 5));
            Assert.IsNotNull(table.CanEatAnotherChecker(checker, 1, 1));

            for (var i = 0; i < table.size; i++)
            {
                for (var j = 0; j < table.size; j++)
                {
                    if (i*j == 1 || i*j == 5 || i*j == 25)
                        continue;
                    Assert.IsNull(table.CanEatAnotherChecker(checker, i, j));
                }
            }

            table.checkers.Clear();

            table.AddChecker(checker);
            table.AddChecker(new Checker(4, 4, red));
            table.AddChecker(new Checker(4, 2, red));
            table.AddChecker(new Checker(2, 4, red));
            table.AddChecker(new Checker(2, 2, red));

            for (var i = 0; i < table.size; i++)
            {
                for (var j = 0; j < table.size; j++)
                {
                    Assert.IsNull(table.CanEatAnotherChecker(checker, i, j));
                }
            }
        }

        [TestMethod]
        public void TestGetMoves(){
            var table = new Table(8);
            var checker = new Checker(3, 3, true) {queen = true};
            table.AddChecker(checker);
            var expected = new List<Point>
            {
                new Point(0, 0),
                new Point(1, 1),
                new Point(2, 2),
                new Point(4, 4),
                new Point(5, 5),
                new Point(6, 6),
                new Point(7, 7),
                new Point(0, 6),
                new Point(1, 5),
                new Point(2, 4),
                new Point(4, 2),
                new Point(5, 1),
                new Point(6, 0)
            };
            var actual = table.GetMoves(checker);
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestGetMoves2()
        {
            var table = new Table(8);
            var checker = new Checker(3, 3, true);
            table.AddChecker(checker);
            var expected = new List<Point>
            {
                new Point(1, 1),
                new Point(2, 2),
                new Point(4, 4),
                new Point(5, 5),
                new Point(1, 5),
                new Point(2, 4),
                new Point(4, 2),
                new Point(5, 1)
            };
            var actual = table.GetMoves(checker);
            CollectionAssert.AreEquivalent(expected, actual);
        }

        /// <summary>
        /// Tests the can eat.
        /// </summary>
        [TestMethod]
        public void TestCanEat()
        {
            TestCanEatColor(true);
            TestCanEatColor(false);
        }

        /// <summary>
        /// Tests the one option of can eat.
        /// </summary>
        public void TestCanEatColor(bool red)
        {
            var table = new Table(8);
            var checker = new Checker(3, 3, red);

            table.AddChecker(checker);
            Assert.IsFalse(table.CanEat(checker));

            for (int i = -1; i >= 1; i += 2)
            {
                for (int j = -1; j >= 1; j += 2)
                {
                    var tempChecker = new Checker(3 + i, 3 + j, !red);
                    table.AddChecker(tempChecker);

                    Assert.IsTrue(table.CanEat(checker));

                    table.RemoveChecker(tempChecker);
                }
            }

            table.RemoveChecker(checker);
            checker = new Checker(3, 3, red) {queen = true};

            table.AddChecker(checker);
            Assert.IsFalse(table.CanEat(checker));

            for (int i = -2; i >= 2; i += 4)
            {
                for (int j = -2; j >= 2; j += 4)
                {
                    var tempChecker = new Checker(3 + i, 3 + j, !red);
                    table.AddChecker(tempChecker);

                    Assert.IsTrue(table.CanEat(checker));

                    checker.queen = false;
                    Assert.IsFalse(table.CanEat(checker));
                    checker.queen = true;

                    table.RemoveChecker(tempChecker);
                }
            }

            table.AddChecker(new Checker(5, 3, !red));

            Assert.IsFalse(table.CanEat(checker));

            table.AddChecker(new Checker(4, 4, red));

            Assert.IsFalse(table.CanEat(checker));
        }

        [TestMethod]
        public void TestCanMove()
        {
            var table = new Table(8);
            var checker = new Checker(0, 0, false);
            table.AddChecker(checker);
            Assert.AreEqual(table.CanMove(checker, -1, 1), 0);
        }

        [TestMethod]
        public void TestCanMove2()
        {
            var table = new Table(8);
            var checker = new Checker(0, 0, true);
            table.AddChecker(checker);
            Assert.AreEqual(table.CanMove(checker, 1, 1), 1);
        }

        [TestMethod]
        public void TestCanMove3()
        {
            var table = new Table(8);
            var checker1 = new Checker(0, 0, true);
            var checker2 = new Checker(1, 1, false);
            table.AddChecker(checker1);
            table.AddChecker(checker2);
            Assert.AreEqual(table.CanMove(checker1, 2, 2), 2);
        }
    }
}
