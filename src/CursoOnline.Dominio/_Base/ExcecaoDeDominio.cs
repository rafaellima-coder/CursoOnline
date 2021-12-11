using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CursoOnline.Dominio._Base
{
    [Serializable]
    public class ExcecaoDeDominio : ArgumentException
    {
        public List<string> MensagensDeErro;

        public ExcecaoDeDominio()
        {
        }

        public ExcecaoDeDominio(List<string> mensagensDeErros)
        {
            this.MensagensDeErro = mensagensDeErros;
        }

        public ExcecaoDeDominio(string message) : base(message)
        {
        }

        public ExcecaoDeDominio(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExcecaoDeDominio(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}