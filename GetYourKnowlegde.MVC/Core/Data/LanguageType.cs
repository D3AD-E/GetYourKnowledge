namespace GetYourKnowledge.MVC.Core.Data
{
    /// <summary>
    /// Available languages for translation
    /// </summary>
    public class LanguageType
    {
        private LanguageType(string language)
        {
            Value = language;
        }

        public string Value { get; private set; }
        public static LanguageType English { get { return new LanguageType("en"); } }
        public static LanguageType Polish { get { return new LanguageType("pl"); } }

        public override string ToString()
        {
            return Value;
        }
    }
}
