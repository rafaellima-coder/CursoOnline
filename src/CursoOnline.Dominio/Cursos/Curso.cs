using CursoOnline.Dominio._Base;
using System;

namespace CursoOnline.Dominio.Cursos
{
    public class Curso:Entidade
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
        private Curso() { }
        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), "Deve ter um nome válido.")
                .Quando(cargaHoraria < 1, "A carga horária deve ser maior ou igual a 1")
                .Quando(valor < 1, "O valor deve ser maior ou igual a 1")
                .DispararExcecaoSeExistir();
            

            this.nome = nome;
            Descricao = descricao;
            this.cargaHoraria = cargaHoraria;
            this.publicoAlvo = publicoAlvo;
            this.valor = valor;
        }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
               .Quando(string.IsNullOrEmpty(nome), "Nome inválido.")
               .DispararExcecaoSeExistir();

            Nome = nome;
        }

        public void AlterarCargaHoraria(double cargaHoraria)
        {
            ValidadorDeRegra.Novo()
               .Quando(cargaHoraria < 1, "Carga horária inválida")
               .DispararExcecaoSeExistir();
            CargaHoraria = cargaHoraria;
        }

        public void AlterarValor(double valor)
        {
            ValidadorDeRegra.Novo()               
               .Quando(valor < 1, "Valor inválido.")
               .DispararExcecaoSeExistir();
            Valor = valor;
        }
    }
}
