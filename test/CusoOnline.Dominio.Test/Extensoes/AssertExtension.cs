using CursoOnline.Dominio._Base;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Extensoes
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ExcecaoDeDominio exception,string mensagem) 
        {
            if (exception.MensagensDeErro.Contains(mensagem))
                Assert.True(true);
            else
                Assert.False(true,$"Esperava mensagem: '{mensagem}'");
        }
    }
}
