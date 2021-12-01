using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CursoOline.Web.Controllers
{
    public class CursoController : Controller
    {
        public readonly ArmazenadorDeCurso _amarzenadorDeCurso;
        private readonly IRepositorio<Curso> _cursoRepositorio;
        public CursoController(ArmazenadorDeCurso armazenadorDeCurso, IRepositorio<Curso> cursoRepositorio)
        {
            _amarzenadorDeCurso = armazenadorDeCurso;
            _cursoRepositorio = cursoRepositorio;
        }
        public IActionResult Index()
        {
            var cursos = _cursoRepositorio.Consultar();
            if (cursos.Any())
            {
                var dto = cursos.Select(c=> new CursoParaListagemDto
                {
                    Id=c.Id
                    ,Nome = c.Nome
                    ,CargaHoraria = c.CargaHoraria
                    ,PublicoAlvo = c.PublicoAlvo.ToString()
                    ,Valor = c.Valor

                });
                return View("Index", PaginatedList<CursoParaListagemDto>.Create(dto, Request));
            }
            return View("Index", PaginatedList<CursoParaListagemDto>.Create(null, Request));
        }
        public IActionResult Novo()
        {
            return View("NovoOuEditar", new CursoDto());
        }
        [HttpPost]
        public IActionResult Salvar(CursoDto model)
        {
            _amarzenadorDeCurso.Armazenar(model);
            return RedirectToAction("Index");
        }
    }
}
