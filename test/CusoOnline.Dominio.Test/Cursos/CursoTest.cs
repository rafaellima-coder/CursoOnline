using CursoOnline.Dominio.Test.Builders;
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
    //- Curso deve ter uma descrição.
    public class CursoTest : IDisposable
    {
        readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly double _valor;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper outputHelper)
        {
            _output = outputHelper;
            _output.WriteLine("Construtor sendo executado");
            _nome = "Informática básica";
            _cargaHoraria = (double)80;
            _valor = (double)950;
            _publicoAlvo = PublicoAlvo.Estudante;
            _descricao = "Uma descrição";

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
                PublicoAlvo = _publicoAlvo,
                Descricao = _descricao
            };

            var curso = new Curso(cursoEsperado.Nome, _descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {

            Assert.Throws<ArgumentException>(() =>
             CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                 .ComMensagem("Deve ter um nome válido.");

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {

            Assert.Throws<ArgumentException>(() =>
        CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                   .ComMensagem("A carga horária deve ser maior ou igual a 1");

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenorQue1(double valorInvalido)
        {

            Assert.Throws<ArgumentException>(() =>
       CursoBuilder.Novo().ComValor(valorInvalido).Build())
                 .ComMensagem("O valor deve ser maior ou igual a 1");

        }


    }
    public enum PublicoAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }
    public class Curso
    {
        private string nome;
        private double cargaHoraria;
        private PublicoAlvo publicoAlvo;
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
        public PublicoAlvo PublicoAlvo
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

        public string Descricao { get; set; }

        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Deve ter um nome válido.");

            if (cargaHoraria < 1)
                throw new ArgumentException("A carga horária deve ser maior ou igual a 1");

            if (valor < 1)
                throw new ArgumentException("O valor deve ser maior ou igual a 1");

            this.nome = nome;
            Descricao = descricao;
            this.cargaHoraria = cargaHoraria;
            this.publicoAlvo = publicoAlvo;
            this.valor = valor;
        }
    }
}
