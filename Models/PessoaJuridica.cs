namespace GerenciaContas.Models
{
    public class PessoaJuridica : Pessoa
    {
        public virtual string Cnpj { get; set; }
        public virtual string RazaoSocial { get; set; }
        public virtual string NomeFantasia { get; set; }
    }
}