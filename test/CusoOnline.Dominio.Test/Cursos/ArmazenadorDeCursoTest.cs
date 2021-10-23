using Bogus;
using CursoOnline.Dominio.Cursos;
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
                PublicoAlvo = 1,
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

    }
    public interface ICursoRepositorio
    {
       void Adicionar(Curso curso);
    }
    public  class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            this._cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var curso = 
                new Curso(cursoDto.Nome,cursoDto.Descricao,cursoDto.CargaHoraria,PublicoAlvo.Estudante,cursoDto.Valor);
            _cursoRepositorio.Adicionar(curso);
           
        }
    }

    public  class CursoDto
    {
        public CursoDto()
        {
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public int PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }
}
