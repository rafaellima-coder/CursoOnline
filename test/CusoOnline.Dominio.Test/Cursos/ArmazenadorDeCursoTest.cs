using CursoOnline.Dominio.Cursos;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    
    public class ArmazenadorDeCursoTest
    {
        [Fact]
        public void DeveAdicionarCurso()
        {
            var cursoDto = new CursoDto()
            {
                Nome = "Curso A",
                Descricao = "Descricao",
                CargaHoraria = 80,
                PublicoAlvo = 1,
                Valor = 850.00
            };

            var cursoRepositorioMock = new Mock<ICursoRepositorio>();
            var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositorioMock.Object);
            armazenadorDeCurso.Armazenar(cursoDto);
            cursoRepositorioMock.Verify(r=> r.Adicionar(It.IsAny<Curso>()));
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
        public int CargaHoraria { get; set; }
        public int PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }
}
