using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GetYourKnowledge.Models
{
    public class InputAdviceAmountModel
    {
        [Required(ErrorMessage = "Please enter a number")]
        [Range(5, 20, ErrorMessage = "Enter a number between 5 and 20")]
        public int Amount { get; set; }
    }
}
