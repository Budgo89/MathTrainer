using _Root.Scripts.Interfaces;
using _Root.Scripts.Localizations;
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
        private readonly AudioModel _audioModel;
        private readonly LocalizationModel _localizationText;

        private ProfilePlayers _profilePlayer;

        private IWorldGenerator _worldGenerator;
        private GameController _gameController;
        private GameUIController _gameUIController;
        private MainMenuUIController _mainMenuUIController;
        private GameOverUIController _gameOverUIController;
        private GameSettingsMenuUiController _gameSettingsMenuUiController;
        private RecordsUIController _recordsUIController;
        private SettingsUIController _settingsUIController;
        private TutorialUiController _tutorialUiController;
        
        private GameSettings _gameSettings;
        private float _pointCount = 100;

        public MainController(Transform placeFor, SwipeDetection swipeDetection, UIDocument uiDocument,
            UiManager uiManager, Records records, AudioModel audioModel,
            LocalizationModel localizationText)
        {
            _placeFor = placeFor;
            _swipeDetection = swipeDetection;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _records = records;
            _audioModel = audioModel;
            _localizationText = localizationText;

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
                    _mainMenuUIController = new MainMenuUIController(_profilePlayer, _uiDocument,_uiManager, _audioModel, _localizationText.Localization);
                    break;
                case GameState.GameSettingsMenuUiController:
                    _gameSettingsMenuUiController =
                        new GameSettingsMenuUiController(_profilePlayer, _uiDocument, _uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.Records:
                    _recordsUIController = new RecordsUIController(_profilePlayer, _uiDocument, _uiManager, _records, _audioModel, _localizationText.Localization);
                    break;
                case GameState.MultiplicationEasyGame:
                    _worldGenerator = new WorldGeneratorMultiplicationEasyController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.AdditionEasyGame:
                    _worldGenerator = new WorldGeneratorAdditionEasyController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.SubtractionEasyGame:
                    _worldGenerator = new WorldGeneratorSubtractionEasyController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.DivisionEasyGame:
                    _worldGenerator = new WorldGeneratorDivisionEasyController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.MultiplicationNormalGame:
                    _worldGenerator = new WorldGeneratorMultiplicationNormalController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.AdditionNormalGame:
                    _worldGenerator = new WorldGeneratorAdditionNormalController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.SubtractionNormalGame:
                    _worldGenerator = new WorldGeneratorSubtractionNormalController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.DivisionNormalGame:
                    _worldGenerator = new WorldGeneratorDivisionNormalController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.MultiplicationHardGame:
                    _worldGenerator = new WorldGeneratorMultiplicationHardController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.AdditionHardGame:
                    _worldGenerator = new WorldGeneratorAdditionHardController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.SubtractionHardGame:
                    _worldGenerator = new WorldGeneratorSubtractionHardController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.DivisionHardGame:
                    _worldGenerator = new WorldGeneratorDivisionHardController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection, _gameSettings, _audioModel);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.Settings:
                    _settingsUIController = new SettingsUIController(_profilePlayer, _uiDocument,_uiManager, _audioModel, _localizationText);
                    break;
                case GameState.GameOver:
                    _gameOverUIController = new GameOverUIController(_profilePlayer, _uiDocument,_uiManager, _records, _pointCount, _gameSettings, _audioModel, _localizationText.Localization);
                    break;
                case GameState.TutorialUiController:
                    _tutorialUiController = new TutorialUiController(_profilePlayer, _uiDocument, _uiManager);
                    break;
            }
        }

        public void Update()
        {
            _gameController?.Update();
        }

        private void DisposeControllers()
        {
            _uiDocument.rootVisualElement.Clear();
            _pointCount = _gameUIController?.GetPointCount() ?? _pointCount;
            _worldGenerator?.Dispose();
            _gameController?.Dispose();
            _gameUIController?.Dispose();
            _mainMenuUIController?.Dispose();
            _gameOverUIController?.Dispose();
            _gameSettingsMenuUiController?.Dispose();
            _recordsUIController?.Dispose();
            _settingsUIController?.Dispose();
            _tutorialUiController?.Dispose();
        }
    }
}