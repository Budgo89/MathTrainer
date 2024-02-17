using _Root.Scripts.Enums;
using Controllers;
using Profile;

namespace _Root.Scripts.Controllers
{
    public class StartGameController : BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private readonly GameSettings _gameSettings;

        public StartGameController(ProfilePlayers profilePlayer, GameSettings gameSettings)
        {
            _profilePlayer = profilePlayer;
            _gameSettings = gameSettings;

            StartGame();
        }

        private void StartGame()
        {
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Multiplication &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Easy)
                _profilePlayer.CurrentState.Value = GameState.MultiplicationEasyGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Easy)
                _profilePlayer.CurrentState.Value = GameState.AdditionEasyGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Division &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Easy)
                _profilePlayer.CurrentState.Value = GameState.DivisionEasyGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Subtraction &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Easy)
                _profilePlayer.CurrentState.Value = GameState.SubtractionEasyGame;
            
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Multiplication &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Normal)
                _profilePlayer.CurrentState.Value = GameState.MultiplicationNormalGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Normal)
                _profilePlayer.CurrentState.Value = GameState.AdditionNormalGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Division &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Normal)
                _profilePlayer.CurrentState.Value = GameState.DivisionNormalGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Subtraction &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Normal)
                _profilePlayer.CurrentState.Value = GameState.SubtractionNormalGame;
            
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Multiplication &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Hard)
                _profilePlayer.CurrentState.Value = GameState.MultiplicationHardGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Hard)
                _profilePlayer.CurrentState.Value = GameState.AdditionHardGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Division &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Hard)
                _profilePlayer.CurrentState.Value = GameState.DivisionHardGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Subtraction &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Hard)
                _profilePlayer.CurrentState.Value = GameState.SubtractionHardGame;
        }
    }
}