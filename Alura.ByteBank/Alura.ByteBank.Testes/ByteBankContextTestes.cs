using Xunit;
using System;
using Alura.ByteBank.Dados.Contexto;

namespace Alura.ByteBank.Testes
{
    public class ByteBankContextTestes
    {
        [Fact]
        public void TestaContextoConexaoComMySQL()
        {
            // Arrange
            var contexto = new ByteBankContexto();
            bool conectado;

            // Act
            try
            {
                conectado = contexto.Database.CanConnect();
            }
            catch (Exception)
            {
                throw new Exception("N�o foi poss�vel conectar � base de dados");
            }

            // Assert
            Assert.True(conectado);
        }
    }
}