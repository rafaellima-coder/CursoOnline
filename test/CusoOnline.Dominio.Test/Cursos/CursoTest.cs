using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Extensoes;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{

    public class CursoTest 
    {
        
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly double _valor;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly string _descricao;
        private readonly Faker faker;
        public CursoTest()
        {
          
            faker = new Faker();
            _nome = faker.Random.Words();
            _cargaHoraria = faker.Random.Double(50,1000);
            _valor = faker.Random.Double(100, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _descricao = faker.Lorem.Paragraph();

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

            Assert.Throws<ExcecaoDeDominio>(() =>
             CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                 .ComMensagem(Resource.NomeInvalido);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {

            Assert.Throws<ExcecaoDeDominio>(() =>
        CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                   .ComMensagem(Resource.CargaHorariaInvalida);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenorQue1(double valorInvalido)
        {

            Assert.Throws<ExcecaoDeDominio>(() =>
       CursoBuilder.Novo().ComValor(valorInvalido).Build())
                 .ComMensagem(Resource.ValorInvalido);

        }
        [Fact]
        public void DeveAlterarNome() 
        {
            var nomeEsperado = faker.Person.FullName;
            var curso = CursoBuilder.Novo().Build();
            curso.AlterarNome(nomeEsperado);
            Assert.Equal(nomeEsperado,curso.Nome);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() =>
             curso.AlterarNome(nomeInvalido))
                 .ComMensagem(Resource.NomeInvalido);

        }
        [Fact]
        public void DeveAlterarCargaHoraria()
        {
            var cargaHorariaEsperada = 20.5;
            var curso = CursoBuilder.Novo().Build();
            curso.AlterarCargaHoraria(cargaHorariaEsperada);
            Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarComCargaHorariaInvalida(double cargaHorariaInvalida)
        {

            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() =>
             curso.AlterarCargaHoraria(cargaHorariaInvalida))
                 .ComMensagem(Resource.CargaHorariaInvalida);

        }
        [Fact]
        public void DeveAlterarValor()
        {
            var valorEsperado = 134.56;
            var curso = CursoBuilder.Novo().Build();
            curso.AlterarValor(valorEsperado);
            Assert.Equal(valorEsperado, curso.Valor);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarComValorIvalido(double valorInvalido)
        {

            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() =>
             curso.AlterarValor(valorInvalido))
                 .ComMensagem(Resource.ValorInvalido);

        }

    }


}
