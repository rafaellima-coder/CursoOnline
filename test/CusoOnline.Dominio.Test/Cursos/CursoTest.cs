using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Extensoes;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test.Cursos
{
    
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
            var faker = new Faker();
            _nome = faker.Random.Words();
            _cargaHoraria = faker.Random.Double(50,1000);
            _valor = faker.Random.Double(100, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _descricao = faker.Lorem.Paragraph();

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
   
    
}
