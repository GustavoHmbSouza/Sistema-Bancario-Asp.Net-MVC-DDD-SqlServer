using System;
using System.ComponentModel.DataAnnotations;

namespace ProjTeste.Web.Application.Conta.Model
{
    public class OperacaoModel
    {
        public int Num_SeqlOperacao { get; set; }

        [Display(Name = "Numero da Operação")]
        public int Num_Operacao { get; set; }

        [Display(Name = "Numero do Tipo da Operação")]
        public byte Num_TipoOperacao { get; set; }

        [Display(Name = "Primeira Conta")]
        public int Num_idConta1 { get; set; }

        [Display(Name = "Segunda Conta")]
        public int Num_idConta2 { get; set; }

        [Display(Name = "Valor")]
        public decimal Num_Valor { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da Operação")]
        public DateTime Date_DataOperacao { get; set; }

        [Display(Name = "Tipo da Operação")]
        public string Nom_TipoOperacao { get; set; }

        [Display(Name = "Destinatário")]
        public string Nom_Destinatario { get; set; }

        public OperacaoModel()
        {
            Random randNum = new Random();
            Num_Operacao = randNum.Next(255);
        }
    }
}
