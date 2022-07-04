using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCoiner.Models
{
    public class Dashboard
    {
        public int Tokens { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00000000}", ApplyFormatInEditMode = true)]
        public decimal BTC { get; set; }
        public decimal Raised { get; set; }

        public int InvestmentAmount { get; set; }
        public int CurrentBalance { get; set; }
    }
}