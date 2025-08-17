using NaureBack.Services;

namespace NaureTest
{
    public class PalabraClaveServicioTest
    {
        [Fact]
        public void InsertarToken()
        {
            PalabraClaveServicio palabraClaveServicio = new PalabraClaveServicio();
            Guid guid = Guid.NewGuid();
            bool res;

            palabraClaveServicio.Insertar(guid.ToString());
            res = palabraClaveServicio.Existe(guid.ToString());

            Assert.True(res);
        }

        [Fact]
        public void NoExisteToken()
        {
            PalabraClaveServicio palabraClaveServicio = new PalabraClaveServicio();
            Guid guid = Guid.NewGuid();
            Guid guidFalse = Guid.NewGuid();
            bool res;

            palabraClaveServicio.Insertar(guid.ToString());
            res = palabraClaveServicio.Existe(guidFalse.ToString());

            Assert.False(res);
        }
    }
}