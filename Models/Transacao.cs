using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciaContas.Models
{
    public class Transacao
    {
        public virtual Guid Id { get; set; }
        public virtual Conta Debitada { get; set; }
        public virtual Conta Creditada { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual TipoTransacao TipoTransacao { get; set; }
        public virtual StatusTransacao StatusTransacao { get; set; }
    }
}
