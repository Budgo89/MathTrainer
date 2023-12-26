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

        MultiplicationAverageGame, // *
        DivisionAverageGame, // деление
        SubtractionAverageGame, // -
        AdditionAverageGame, //+

        #endregion

        #region Hard
        
        MultiplicationHardGame, // *
        DivisionHardGame, // деление
        SubtractionHardGame, // -
        AdditionHardGame, //+

        #endregion

        TypeGame,
        Complexity,
        GameOver
    }
}
