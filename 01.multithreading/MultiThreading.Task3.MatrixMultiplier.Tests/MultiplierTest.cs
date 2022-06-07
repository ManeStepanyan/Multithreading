using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;

namespace MultiThreading.Task3.MatrixMultiplier.Tests
{
    [TestClass]
    public class MultiplierTest
    {
        [TestMethod]
        public void MultiplyMatrix3On3Test()
        {
            TestMatrix3On3(new MatricesMultiplier());
            TestMatrix3On3(new MatricesMultiplierParallel());
        }

        [TestMethod]
        public void ParallelEfficiencyTest()
        {
            var m1 = new Matrix(50, 50);
            m1.SetElement(0, 0, 34);
            m1.SetElement(0, 1, 2);
            m1.SetElement(0, 2, 6);
            m1.SetElement(0, 3, 6);
            m1.SetElement(0, 4, 6);

            m1.SetElement(1, 0, 5);
            m1.SetElement(1, 1, 4);
            m1.SetElement(1, 2, 54);
            m1.SetElement(1, 3, 4);
            m1.SetElement(1, 4, 54);

            m1.SetElement(2, 0, 2);
            m1.SetElement(2, 1, 9);
            m1.SetElement(2, 2, 8);
            m1.SetElement(2, 3, 9);
            m1.SetElement(2, 4, 8);

            m1.SetElement(3, 0, 2);
            m1.SetElement(3, 1, 9);
            m1.SetElement(3, 2, 8);
            m1.SetElement(3, 3, 9);
            m1.SetElement(3, 4, 8);

            m1.SetElement(4, 0, 2);
            m1.SetElement(4, 1, 9);
            m1.SetElement(4, 2, 8);
            m1.SetElement(4, 3, 9);
            m1.SetElement(4, 4, 8);

            var m2 = new Matrix(50, 50);
            m2.SetElement(0, 0, 12);
            m2.SetElement(0, 1, 52);
            m2.SetElement(0, 2, 85);
            m2.SetElement(0, 3, 6);
            m2.SetElement(0, 4, 6);

            m2.SetElement(1, 0, 5);
            m2.SetElement(1, 1, 5);
            m2.SetElement(1, 2, 54);
            m2.SetElement(0, 3, 4);
            m2.SetElement(0, 4, 22);

            m2.SetElement(2, 0, 5);
            m2.SetElement(2, 1, 8);
            m2.SetElement(2, 2, 9);
            m2.SetElement(0, 3, 10);
            m2.SetElement(0, 4, 5);

            m2.SetElement(3, 0, 5);
            m2.SetElement(3, 1, 8);
            m2.SetElement(3, 2, 9);
            m2.SetElement(3, 3, 10);
            m2.SetElement(3, 4, 5);

            m2.SetElement(4, 0, 5);
            m2.SetElement(4, 1, 8);
            m2.SetElement(4, 2, 9);
            m2.SetElement(4, 3, 10);
            m2.SetElement(4, 4, 5);

            Stopwatch sp = new Stopwatch();
            sp.Start();
            new MatricesMultiplier().Multiply(m1, m2);
            sp.Stop();
            var syncTime = sp.ElapsedMilliseconds;
            sp.Restart();
            sp.Start();
            new MatricesMultiplierParallel().Multiply(m1, m2);
            sp.Stop();
            var parrallelTime = sp.ElapsedMilliseconds;
            Assert.IsTrue(parrallelTime < syncTime);
        }

        #region private methods

        void TestMatrix3On3(IMatricesMultiplier matrixMultiplier)
        {
            if (matrixMultiplier == null)
            {
                throw new ArgumentNullException(nameof(matrixMultiplier));
            }

            var m1 = new Matrix(3, 3);
            m1.SetElement(0, 0, 34);
            m1.SetElement(0, 1, 2);
            m1.SetElement(0, 2, 6);

            m1.SetElement(1, 0, 5);
            m1.SetElement(1, 1, 4);
            m1.SetElement(1, 2, 54);

            m1.SetElement(2, 0, 2);
            m1.SetElement(2, 1, 9);
            m1.SetElement(2, 2, 8);

            var m2 = new Matrix(3, 3);
            m2.SetElement(0, 0, 12);
            m2.SetElement(0, 1, 52);
            m2.SetElement(0, 2, 85);

            m2.SetElement(1, 0, 5);
            m2.SetElement(1, 1, 5);
            m2.SetElement(1, 2, 54);

            m2.SetElement(2, 0, 5);
            m2.SetElement(2, 1, 8);
            m2.SetElement(2, 2, 9);

            var multiplied = matrixMultiplier.Multiply(m1, m2);
            Assert.AreEqual(448, multiplied.GetElement(0, 0));
            Assert.AreEqual(1826, multiplied.GetElement(0, 1));
            Assert.AreEqual(3052, multiplied.GetElement(0, 2));

            Assert.AreEqual(350, multiplied.GetElement(1, 0));
            Assert.AreEqual(712, multiplied.GetElement(1, 1));
            Assert.AreEqual(1127, multiplied.GetElement(1, 2));

            Assert.AreEqual(109, multiplied.GetElement(2, 0));
            Assert.AreEqual(213, multiplied.GetElement(2, 1));
            Assert.AreEqual(728, multiplied.GetElement(2, 2));
        }

        #endregion
    }
}
