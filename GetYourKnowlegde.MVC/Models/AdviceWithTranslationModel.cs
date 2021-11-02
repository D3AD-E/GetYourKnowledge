using GetYourKnowledge.MVC.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetYourKnowledge.MVC.Models
{
    public record AdviceWithTranslationModel : GenericAdvice
    {
        public string Translation { get; set; }

        public AdviceWithTranslationModel(GenericAdvice advice):base(advice)
        {

        }
    }
}
