﻿using Bogus;
using CursoOnline.Dominio._Base;
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
            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
              .ComMensagem(Resource.NomeDoCursoJaExiste);
        }
        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido() 
        {
            var publicoAlvoIvalido = "Medico";
            _cursoDto.PublicoAlvo = publicoAlvoIvalido;
            Assert.Throws<ExcecaoDeDominio>(()=>_armazenadorDeCurso.Armazenar(_cursoDto))
               .ComMensagem(Resource.PublicoAlvoInvalido) ;
        }
        [Fact]
        public void DeveAlterarDadosDoCurso()
        {
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(r=> r.ObterPorId(_cursoDto.Id)).Returns(curso); 

            _armazenadorDeCurso.Armazenar(_cursoDto);

            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
        }
        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoCursoJaExiste()
        {
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(r=>r.Adicionar(It.IsAny<Curso>()),Times.Never);
        }

    }
   
    

   
}
