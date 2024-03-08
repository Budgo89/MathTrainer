using _Root.Scripts.Interfaces;

namespace _Root.Scripts.Models
{
    public class LocalizationModel
    {
        /// <summary>
        /// 0 - eng, 1 - rus
        /// </summary>
        public int MarkerLocalization;
        public ILocalization Localization;

        public LocalizationModel(int markerLocalization, ILocalization localization)
        {
            MarkerLocalization = markerLocalization;
            Localization = localization;
        }
    }
}