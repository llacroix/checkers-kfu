using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Checkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCheckers
{
    /// <summary>
    /// Сводное описание для CheckerTest
    /// </summary>
    [TestClass]
    public class CheckerTest
    {
        public CheckerTest()
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
        /// <summary>
        /// Tests the Constructor
        /// </summary>
        [TestMethod]
        public void TestCreate()
        {
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var checker = new Checker(i, j, j % 2 == 0);
                    Assert.AreEqual(checker.x, i);
                    Assert.AreEqual(checker.y, j);
                    Assert.AreEqual(checker.white, j % 2 == 0);
                }
            }
        }

        /// <summary>
        /// Tests the Constructor
        /// </summary>
        [TestMethod]
        public void TestCreate2()
        {
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var checker = new Checker(new Point(i, j), j%2 == 0);
                    Assert.AreEqual(checker.x, i);
                    Assert.AreEqual(checker.y, j);
                    Assert.AreEqual(checker.white, j % 2 == 0);
                }
            }
        }

        /// <summary>
        /// Tests the move function.
        /// </summary>
        [TestMethod]
        public void TestMove()
        {
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var checker = new Checker(i, j, j % 2 == 0);

                    for (var k = 0; k < 10; k++)
                    {
                        for (var d = 0; d < 10; d++)
                        {
                            checker.move(k, d);
                            Assert.AreEqual(checker.x, k);
                            Assert.AreEqual(checker.y, d);
                            Assert.AreEqual(checker.white, j % 2 == 0);
                        }
                    }
                }
            }
        }
    }
}
