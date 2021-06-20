using Xunit;

namespace CusoOnline.Dominio.Test
{
    public class MeuPrimeiroTeste
    {
        [Fact(DisplayName ="Testar")]
        public void DeveAsVariaveisTeremOMesmoValor()
        {
            //Organização
            var variavel1 = 2;
            var variavel2 = 2;
            
            //Ação 
            variavel2 = variavel1;
            
            //Assert - verifica se estar correto
            Assert.Equal(variavel1, variavel2);
        }
    }
}
