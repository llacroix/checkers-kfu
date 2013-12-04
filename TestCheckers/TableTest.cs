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
        public void TestMoveTwoDiffChekers()
        {
            var table = new Table(8);
            Checker checker1 = new Checker(5, 5, true);
            table.AddChecker(checker1);
            Checker checker2 = new Checker(6, 6, false);
            table.AddChecker(checker2);
            Assert.AreEqual(true, table.Move(checker1, 6, 6));
            Checker checker3 = new Checker(4, 6, true);
            table.AddChecker(checker3);
            Assert.AreEqual(false, table.Move(checker1, 4, 6));
        }
    }
}
