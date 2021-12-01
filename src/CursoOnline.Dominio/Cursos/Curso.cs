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
