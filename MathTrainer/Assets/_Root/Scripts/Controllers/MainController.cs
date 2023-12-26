using _Root.Scripts.Interfaces;
using _Root.Scripts.Models;
using _Root.Scripts.ScriptableObjects;
using Controllers;
using MB;
using Profile;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class MainController : BaseController
    {
        private readonly Transform _placeFor;
        private readonly SwipeDetection _swipeDetection;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private Records _records;
        
        private ProfilePlayers _profilePlayer;

        private IWorldGenerator _worldGenerator;
        private GameController _gameController;
        private GameUIController _gameUIController;
        private MainMenuUIController _mainMenuUIController;
        private GameStateUIController _gameStateUIController;
        private ComplexityUIController _complexityUIController;
        private GameOverUIController _gameOverUIController;
        
        private GameSettings _gameSettings;
        private int _pointCount = 100;

        public MainController(Transform placeFor, SwipeDetection swipeDetection, UIDocument uiDocument,
            UiManager uiManager, Records records)
        {
            _placeFor = placeFor;
            _swipeDetection = swipeDetection;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _records = records;

            _gameSettings = new GameSettings();

            _profilePlayer = new ProfilePlayers(GameState.MainMenu);
            _profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);
        }

        private void OnChangeGameState(GameState state)
        {
            DisposeControllers();
            switch (state)
            {
                case GameState.MainMenu:
                    _mainMenuUIController = new MainMenuUIController(_profilePlayer, _uiDocument,_uiManager, _records);
                    break;
                case GameState.TypeGame:
                    _gameStateUIController = new GameStateUIController(_profilePlayer, _uiDocument,_uiManager, _gameSettings);
                    break;
                case GameState.Complexity:
                    _complexityUIController = new ComplexityUIController(_profilePlayer, _uiDocument,_uiManager, _gameSettings);
                    break;
                case GameState.MultiplicationEasyGame:
                    _worldGenerator = new WorldGeneratorMultiplicationEasyController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager);
                    break;
                case GameState.AdditionEasyGame:
                    _worldGenerator = new WorldGeneratorAdditionEasyController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager);
                    break;
                case GameState.SubtractionEasyGame:
                    _worldGenerator = new WorldGeneratorSubtractionEasyController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager);
                    break;
                case GameState.DivisionEasyGame:
                    _worldGenerator = new WorldGeneratorDivisionEasyController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager);
                    break;
                case GameState.GameOver:
                    _gameOverUIController = new GameOverUIController(_profilePlayer, _uiDocument,_uiManager, _records, _pointCount, _gameSettings);
                    break;
            }
        }

        private void DisposeControllers()
        {
            _uiDocument.rootVisualElement.Clear();
            _pointCount = _gameUIController?.GetPointCount() ?? _pointCount;
            _worldGenerator?.Dispose();
            _gameController?.Dispose();
            _gameUIController?.Dispose();
            _mainMenuUIController?.Dispose();
            _gameStateUIController?.Dispose();
            _complexityUIController?.Dispose();
            _gameOverUIController?.Dispose();
        }
    }
}