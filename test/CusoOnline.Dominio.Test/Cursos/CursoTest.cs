using CursoOnline.Dominio.Test.Extensoes;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test.Cursos
{
    //Eu, enquanto adiministrador, quero criar e editar cursos para que sejam abertas
    //matriculas para o mesmo.

    //Critério de aceite

    //- Criar um curso com nome. carga horária, publico alvo e valor do curso
    //- As opções para o publico alvo são: Estudante, Universitário, Empregado e Empreendedor
    //- Todos os campos do cruso são obrigatórios.
    public class CursoTest:IDisposable
    {
        readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly double _valor;
        private readonly PublicAlvo _publicoAlvo;

        public CursoTest(ITestOutputHelper outputHelper)
        {
            _output = outputHelper;
            _output.WriteLine("Construtor sendo executado");
            _nome = "Informática básica";
            _cargaHoraria = (double)80;
            _valor = (double)950;
            _publicoAlvo = PublicAlvo.Estudante;
          
        }
        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }
        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                Valor = _valor,
                PublicoAlvo = _publicoAlvo
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            
           Assert.Throws<ArgumentException>(() =>
            new Curso(nomeInvalido, _cargaHoraria, _publicoAlvo, _valor))
                .ComMensagem("Deve ter um nome válido.");
            
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            
         Assert.Throws<ArgumentException>(() =>
      new Curso(_nome, cargaHorariaInvalida, _publicoAlvo, _valor))
                .ComMensagem("A carga horária deve ser maior ou igual a 1");
           
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenorQue1(double valorInvalido)
        {
           
           Assert.Throws<ArgumentException>(() =>
      new Curso(_nome, _cargaHoraria, _publicoAlvo, valorInvalido))
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
