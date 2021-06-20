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
            string nome = "Informática básica";
            int cargaHoraria = 80;
            double valor = 950;
            string publicoAlvo = "Estudante";

            var curso = new Curso(nome, cargaHoraria, publicoAlvo, valor);
            Assert.Equal(nome, curso.Nome);
            Assert.Equal(cargaHoraria, curso.CargaHoraria);
            Assert.Equal(publicoAlvo, curso.PublicoAlvo);
            Assert.Equal(valor, curso.Valor);
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


        public Curso(string nome, int cargaHoraria, string publicoAlvo, double valor)
        {
            this.nome = nome;
            this.cargaHoraria = cargaHoraria;
            this.publicoAlvo = publicoAlvo;
            this.valor = valor;
        }
    }
}
