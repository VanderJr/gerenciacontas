using System;

namespace GerenciaContas.Models
{
    public class PessoaFisica : Pessoa
    {
        public virtual string Cpf { get; set; }
        public virtual string Nome { get; set; }
        public virtual DateTime DataNascimento { get; set; }
    }
}