using System;

namespace ProjTeste.Domain.Entities
{
    public class ContaDTO
    {
        public int ContaId { get; set; }

        public int NumeroConta { get; set; }

        public string Nome { get; set; }

        public decimal Saldo { get; set; }

        public DateTime DataCriacao { get; set; }

        public byte TipoConta { get; set; }
    }
}
