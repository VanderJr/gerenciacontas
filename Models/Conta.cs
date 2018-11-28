using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciaContas.Models
{
    public class Conta
    {
        public virtual StatusConta StatusConta { get; set; }
        public virtual TipoConta TipoConta { get; set; }
        public virtual Guid Id { get; set; }
        public virtual Conta ContaRaiz { get; set; }
        public virtual Conta ContaPai { get; set; }
        public virtual IList<Conta> ContasFilhas { get; set; }


    }
}
