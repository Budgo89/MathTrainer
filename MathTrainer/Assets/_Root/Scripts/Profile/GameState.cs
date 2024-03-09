namespace Profile
{
    public enum GameState
    {
        MainMenu,

        #region Easy

        MultiplicationEasyGame, // *
        DivisionEasyGame, // деление
        SubtractionEasyGame, // -
        AdditionEasyGame, //+

        #endregion

        #region Average

        MultiplicationNormalGame, // *
        DivisionNormalGame, // деление
        SubtractionNormalGame, // -
        AdditionNormalGame, //+

        #endregion

        #region Hard
        
        MultiplicationHardGame, // *
        DivisionHardGame, // деление
        SubtractionHardGame, // -
        AdditionHardGame, //+

        #endregion

        GameOver,
        GameSettingsMenuUiController,
        Records,
        Settings,
        TutorialUiController
    }
}
