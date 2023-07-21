using Challenge.Controllers;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int numero1 = 5;
            int numero2 = 12;

            PruebaTecnica prueba = new PruebaTecnica();
            int resultado = prueba.SumaNumeros(numero1, numero2);
            Assert.Equals(17, resultado);
        }
    }
}