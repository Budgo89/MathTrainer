using _Root.Scripts.Enums;
using _Root.Scripts.ScriptableObjects;
using Controllers;
using Profile;
using Tool;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class ComplexityUIController : BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private readonly GameSettings _gameSettings;
        
        private VisualElement _root;
        
        private Button _easyButton;
        private Button _averageButton;
        private Button _hardButton;

        private StartGameController _startGameController;

        public ComplexityUIController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager, GameSettings gameSettings)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _gameSettings = gameSettings;
            
            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.ComplexityUi;
            _root = _uiDocument.rootVisualElement;
            
            AddElement();
            Subscribe();
        }

        private void Subscribe()
        {
            _easyButton.RegisterCallback<ClickEvent>(ClickEasyButton);
            _averageButton.RegisterCallback<ClickEvent>(ClickAverageButton);
            _hardButton.RegisterCallback<ClickEvent>(ClickHardButton);
        }

        private void ClickHardButton(ClickEvent evt)
        {
            _gameSettings.ComplexityEnum = ComplexityEnum.Hard;
            StartGame();
        }

        private void ClickAverageButton(ClickEvent evt)
        {
            _gameSettings.ComplexityEnum = ComplexityEnum.Average;
            StartGame();
        }

        private void ClickEasyButton(ClickEvent evt)
        {
            _gameSettings.ComplexityEnum = ComplexityEnum.Easy;
            StartGame();
        }

        private void AddElement()
        {
            _easyButton = _root.Q<Button>(ComplexityUiKey.EasyButtonKey);
            _averageButton = _root.Q<Button>(ComplexityUiKey.AverageButtonKey);
            _hardButton = _root.Q<Button>(ComplexityUiKey.HardButtonKey);
        }

        private void StartGame()
        {
            _startGameController = new StartGameController(_profilePlayer, _gameSettings);
        }

        protected override void OnDispose()
        {
            _startGameController?.Dispose();
        }
    }
}