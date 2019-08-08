using System;
using System.ComponentModel.DataAnnotations;

namespace ProjTeste.Web.Application.Conta.Model
{
    public class ContaModel
    {
        public int ContaId { get; set; }

        [Display(Name = "Numero")]
        public int NumeroConta { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Saldo")]
        public decimal Saldo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da Criação")]
        public DateTime DataCriacao { get; set; }

        public int TipoConta { get; set; }

        public ContaModel()
        {
            Random randNum = new Random();
            NumeroConta = randNum.Next(255);
        }
    }
}
