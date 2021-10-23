using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Extensoes;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();
            _cursoDto = new CursoDto()
            {
                Nome = fake.Random.Word(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Double(50,1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1000,2000)
            };
            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {   
            _armazenadorDeCurso.Armazenar(_cursoDto);
            _cursoRepositorioMock.Verify(r=> r.Adicionar(
                It.Is<Curso>(
                    c=> c.Nome == _cursoDto.Nome
                    &&
                    c.Descricao == _cursoDto.Descricao
                )
                ));
        }
        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);
            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
              .ComMensagem("Nome do curso já consta no banco de dados");
        }
        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido() 
        {
            var publicoAlvoIvalido = "Medico";
            _cursoDto.PublicoAlvo = publicoAlvoIvalido;
            Assert.Throws<ArgumentException>(()=>_armazenadorDeCurso.Armazenar(_cursoDto))
               .ComMensagem("Publico Alvo inválido") ;
        }
       

    }
   
    

   
}
