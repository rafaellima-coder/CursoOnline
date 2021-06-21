using CusoOnline.Dominio.Test.Extensoes;
using ExpectedObjects;
using System;
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
                PublicoAlvo = PublicAlvo.Estudante
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                Valor = (double)950,
                PublicoAlvo = PublicAlvo.Estudante
            };
           Assert.Throws<ArgumentException>(() =>
            new Curso(nomeInvalido, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor))
                .ComMensagem("Deve ter um nome válido.");
            
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                Valor = (double)950,
                PublicoAlvo = PublicAlvo.Estudante
            };
         Assert.Throws<ArgumentException>(() =>
      new Curso(cursoEsperado.Nome, cargaHorariaInvalida, cursoEsperado.PublicoAlvo, cursoEsperado.Valor))
                .ComMensagem("A carga horária deve ser maior ou igual a 1");
           
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenorQue1(double valorInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                Valor = (double)950,
                PublicoAlvo = PublicAlvo.Estudante
            };
           Assert.Throws<ArgumentException>(() =>
      new Curso(cursoEsperado.Nome, cursoEsperado.Valor, cursoEsperado.PublicoAlvo, valorInvalido))
                .ComMensagem("O valor deve ser maior ou igual a 1");
           
        }
    }
    internal enum PublicAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }
    internal class Curso
    {
        private string nome;
        private double cargaHoraria;
        private PublicAlvo publicoAlvo;
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
        public PublicAlvo PublicoAlvo
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
        public Curso(string nome, double cargaHoraria, PublicAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Deve ter um nome válido.");

            if (cargaHoraria < 1)
                throw new ArgumentException("A carga horária deve ser maior ou igual a 1");

            if (valor < 1)
                throw new ArgumentException("O valor deve ser maior ou igual a 1");

            this.nome = nome;
            this.cargaHoraria = cargaHoraria;
            this.publicoAlvo = publicoAlvo;
            this.valor = valor;
        }
    }
}
