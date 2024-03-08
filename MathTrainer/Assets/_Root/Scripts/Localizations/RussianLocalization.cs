using _Root.Scripts.Interfaces;

namespace _Root.Scripts.Localizations
{
    public class RussianLocalization : Localization<ILocalizationText>
    {
        public RussianLocalization(ILocalizationText localizationText)
        {
            _localizationText = localizationText;
        }
    }
}