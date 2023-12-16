namespace Profile
{
    public class GameLevel
    {
        /// <summary>
        /// Текущий уровень
        /// </summary>
        public int CurrentLevel = 0;

        public string CurrentLevelKey = "CurrentLevel";

        public GameLevel()
        {
        }

        public GameLevel(int currentLevel)
        {
            CurrentLevel = currentLevel;
        }
    }
}
