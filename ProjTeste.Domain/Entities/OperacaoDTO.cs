using System;

namespace ProjTeste.Domain.Entities
{
    public class OperacaoDTO
    {
        public int Num_SeqlOperacao { get; set; }
        public int Num_Operacao { get; set; }
        public byte Num_TipoOperacao { get; set; }
        public int Num_idConta1 { get; set; }
        public int Num_idConta2 { get; set; }
        public decimal Num_Valor { get; set; }
        public DateTime Date_DataOperacao { get; set; }

        public string Nom_TipoOperacao{ get; set; }
        public string Nom_Destinatario { get; set; }
    }
}
