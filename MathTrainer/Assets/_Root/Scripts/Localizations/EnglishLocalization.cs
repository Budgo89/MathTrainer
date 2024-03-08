using _Root.Scripts.Interfaces;

namespace _Root.Scripts.Localizations
{
    public class EnglishLocalization: Localization<ILocalizationText>
    {
        public EnglishLocalization(ILocalizationText localizationText)
        {
            _localizationText = localizationText;
        }
    }
}