using _Root.Scripts.Interfaces;

namespace _Root.Scripts.Localizations
{
    public abstract class Localization<TLocalizationText> : ILocalization
        where TLocalizationText : ILocalizationText
    {
        protected TLocalizationText _localizationText;

        string ILocalization.GetPlyText() => _localizationText.PlyText;

        string ILocalization.GetRecordsText() => _localizationText.RecordsText;

        string ILocalization.GetRecordText() => _localizationText.RecordText;

        string ILocalization.GetSettingsText() => _localizationText.SettingsText;

        string ILocalization.GetMenuText() => _localizationText.MenuText;

        string ILocalization.GetPointsText() => _localizationText.PointsText;

        string ILocalization.GetStartText() => _localizationText.StartText;

        string ILocalization.GetAttemptsText() => _localizationText.AttemptsText;

        string ILocalization.GetVolumeText() => _localizationText.VolumeText;

        string ILocalization.GetEasyTest() => _localizationText.EasyTest;

        string ILocalization.GetNormalText() => _localizationText.NormalText;

        string ILocalization.GetHardText() => _localizationText.HardText;
        string ILocalization.GetLanguageText() => _localizationText.LanguageText;
        string ILocalization.GetTutorialText() => _localizationText.TutorialText;
        string ILocalization.GetOnText() => _localizationText.OnText;
        string ILocalization.GetOffText() => _localizationText.OffText;
    }
}