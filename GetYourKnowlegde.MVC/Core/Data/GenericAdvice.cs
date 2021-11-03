using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetYourKnowledge.MVC.Core.Data
{
    /// <summary>
    /// Base class for advice model
    /// </summary>
    public record GenericAdvice
    {
        public int Id { get; set; }

        public string Advice { get; set; }

        public GenericAdvice(GenericAdvice other)
        {
            Id = other.Id;
            Advice = other.Advice;
        }
    }

}
