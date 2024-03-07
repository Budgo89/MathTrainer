using _Root.Scripts.Interfaces;

namespace _Root.Scripts.Localizations
{
    public class EnglishLocalization: ILocalization
    {
        private EnglishText _text;
        
        public EnglishLocalization()
        {
            _text = new EnglishText();
        }
        
        public string GetPlyText() => _text.GetPlyText;

        public string GetRecordsText() => _text.GetRecordsText;

        public string GetRecordText() => _text.GetRecordText;

        public string GetSettingsText() => _text.GetSettingsText;

        public string GetMenuText() => _text.GetMenuText;

        public string GetPointsText() => _text.GetPointsText;

        public string GetStartText() => _text.GetStartText;

        public string GetAttemptsText() => _text.GetAttemptsText;

        public string GetVolumeText() => _text.GetVolumeText;

        public string GetEasyTest() => _text.GetEasyTest;

        public string GetNormalText() => _text.GetNormalText;

        public string GetHardText() => _text.GetHardText;
    }
}