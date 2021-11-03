using GetYourKnowledge.MVC.Core.Data;

namespace GetYourKnowledge.MVC.Models
{
    public record AdviceWithTranslationModel : GenericAdvice
    {
        public string Translation { get; set; }

        public AdviceWithTranslationModel(GenericAdvice advice) : base(advice)
        {

        }
    }
}
