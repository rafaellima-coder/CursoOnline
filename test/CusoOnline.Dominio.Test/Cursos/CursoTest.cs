using ExpectedObjects;
using Xunit;

namespace CusoOnline.Dominio.Test.Cursos
{
    //Eu, enquanto adiministrador, quero criar e editar cursos para que sejam abertas
    //matriculas para o mesmo.

    //Critério de aceite

    //- Criar um curso com nome. carga horária, publico alvo e valor do curso
    //- As opções para o publico alvo são: Estudante, Universitário, Empregado e Empreendedor
    //- Todos os campos do cruso são obrigatórios.
    public class CursoTest
    {
        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                Valor = (double)950,
                PublicoAlvo = "Estudante"
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }
    }

    internal class Curso
    {
        private string nome;
        private double cargaHoraria;
        private string publicoAlvo;
        private double valor;

        public string Nome
        {
            get
            {
                return nome;
            }
            private set
            {
                nome = value;
            }
        }
        public double CargaHoraria
        {
            get
            {
                return cargaHoraria;
            }
            private set
            {
                cargaHoraria = value;
            }
        }
        public string PublicoAlvo
        {
            get
            {
                return publicoAlvo;
            }
            private set
            {
                publicoAlvo = value;
            }
        }
        public double Valor
        {
            get
            {
                return valor;
            }
            private set
            {
                valor = value;
            }
        }


        public Curso(string nome, double cargaHoraria, string publicoAlvo, double valor)
        {
            this.nome = nome;
            this.cargaHoraria = cargaHoraria;
            this.publicoAlvo = publicoAlvo;
            this.valor = valor;
        }
    }
}
