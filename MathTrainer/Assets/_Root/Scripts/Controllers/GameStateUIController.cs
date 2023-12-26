using _Root.Scripts.Enums;
using _Root.Scripts.ScriptableObjects;
using Controllers;
using Profile;
using Tool;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class GameStateUIController : BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private GameSettings _gameSettings;
        
        private VisualElement _root;
        
        private Button _multiplicationButton;
        private Button _divisionButton;
        private Button _subtractionButton;
        private Button _additionButton;

        public GameStateUIController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager, GameSettings gameSettings)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _gameSettings = gameSettings;
            
            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.TypeGameUi;
            _root = _uiDocument.rootVisualElement;
            
            AddElement();
            Subscribe();
        }

        private void Subscribe()
        {
            _multiplicationButton.RegisterCallback<ClickEvent>(ClickMultiplicationButton);
            _divisionButton.RegisterCallback<ClickEvent>(ClickDivisionButton);
            _subtractionButton.RegisterCallback<ClickEvent>(ClickSubtractionButton);
            _additionButton.RegisterCallback<ClickEvent>(ClickAdditionButton);
        }

        private void ClickAdditionButton(ClickEvent evt)
        {
            _gameSettings.TypeGameEnum = TypeGameEnum.Addition;
            _profilePlayer.CurrentState.Value = GameState.Complexity;
        }

        private void ClickSubtractionButton(ClickEvent evt)
        {
            _gameSettings.TypeGameEnum = TypeGameEnum.Subtraction;
            _profilePlayer.CurrentState.Value = GameState.Complexity;
        }

        private void ClickDivisionButton(ClickEvent evt)
        {
            _gameSettings.TypeGameEnum = TypeGameEnum.Division;
            _profilePlayer.CurrentState.Value = GameState.Complexity;
        }

        private void ClickMultiplicationButton(ClickEvent evt)
        {
            _gameSettings.TypeGameEnum = TypeGameEnum.Multiplication;
            _profilePlayer.CurrentState.Value = GameState.Complexity;
        }

        private void AddElement()
        {
            _multiplicationButton = _root.Q<Button>(TypeGameUIKey.MultiplicationButtonKey);
            _divisionButton = _root.Q<Button>(TypeGameUIKey.DivisionButtonKey);
            _subtractionButton = _root.Q<Button>(TypeGameUIKey.SubtractionButtonKey);
            _additionButton = _root.Q<Button>(TypeGameUIKey.AdditionButtonKey);
        }
    }
}