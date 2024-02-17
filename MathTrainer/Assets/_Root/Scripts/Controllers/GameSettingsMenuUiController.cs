using _Root.Scripts.Enums;
using _Root.Scripts.Models;
using _Root.Scripts.ScriptableObjects;
using Controllers;
using Profile;
using Tool;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class GameSettingsMenuUiController : BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private readonly GameSettings _gameSettings;
        private readonly AudioModel _audioModel;

        private VisualElement _root;

        private Button _backButton;
        private Button _easyButton;
        private Button _normalButton;
        private Button _hardButton;
        private Button _additionButton;
        private Button _subtractionButton;
        private Button _multiplyButton;
        private Button _divisionButton;
        private Button _startButton;

        public GameSettingsMenuUiController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager,
            GameSettings gameSettings, AudioModel audioModel)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _gameSettings = gameSettings;
            _audioModel = audioModel;

            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.GameSettingsMenuUi;
            _root = _uiDocument.rootVisualElement;

            AddElement();
            Subscribe();
            AddVisualButton();
        }

        private void AddVisualButton()
        {
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition)
                _additionButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniAdditionFocus);
            else if (_gameSettings.TypeGameEnum == TypeGameEnum.Division)
                _divisionButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniDivisionFocus);
            else if (_gameSettings.TypeGameEnum == TypeGameEnum.Subtraction)
                _startButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniSubtractionFocus);
            else if (_gameSettings.TypeGameEnum == TypeGameEnum.Multiplication) _multiplyButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniMultiplyFocus);

            if (_gameSettings.ComplexityEnum == ComplexityEnum.Easy)
                _easyButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonFocus);
            if (_gameSettings.ComplexityEnum == ComplexityEnum.Normal)
                _normalButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonFocus);
            if (_gameSettings.ComplexityEnum == ComplexityEnum.Hard)
                _hardButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonFocus);
            
        }

        private void Subscribe()
        {
            _backButton.RegisterCallback<ClickEvent>(ClickBackButton);
            _startButton.RegisterCallback<ClickEvent>(ClickStartButton);
            _easyButton.RegisterCallback<ClickEvent>(ClickEasyButton);
            _normalButton.RegisterCallback<ClickEvent>(ClickNormalButton);
            _hardButton.RegisterCallback<ClickEvent>(ClickHardButton);
            _additionButton.RegisterCallback<ClickEvent>(ClickAdditionButton);
            _subtractionButton.RegisterCallback<ClickEvent>(ClickSubtractionButton);
            _multiplyButton.RegisterCallback<ClickEvent>(ClickMultiplyButton);
            _divisionButton.RegisterCallback<ClickEvent>(ClickDivisionButton);
            
            _backButton.RegisterCallback<ClickEvent>(AudioPlay);
            _startButton.RegisterCallback<ClickEvent>(AudioPlay);
            _easyButton.RegisterCallback<ClickEvent>(AudioPlay);
            _normalButton.RegisterCallback<ClickEvent>(AudioPlay);
            _hardButton.RegisterCallback<ClickEvent>(AudioPlay);
            _additionButton.RegisterCallback<ClickEvent>(AudioPlay);
            _subtractionButton.RegisterCallback<ClickEvent>(AudioPlay);
            _multiplyButton.RegisterCallback<ClickEvent>(AudioPlay);
            _divisionButton.RegisterCallback<ClickEvent>(AudioPlay);
        }
        
        private void AudioPlay(ClickEvent evt)
        {
            _audioModel.AudioEffects.clip = _audioModel.AudioEffectsManager.ButtonClick;
            _audioModel.AudioEffects.Play();
        }

        private void ClickDivisionButton(ClickEvent evt)
        {
            _gameSettings.TypeGameEnum = TypeGameEnum.Division;
            VisualTypeGameFocusButton();
            _divisionButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniDivisionFocus);
        }

        private void ClickMultiplyButton(ClickEvent evt)
        {
            _gameSettings.TypeGameEnum = TypeGameEnum.Multiplication;
            VisualTypeGameFocusButton();
            _multiplyButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniMultiplyFocus);
        }

        private void ClickSubtractionButton(ClickEvent evt)
        {
            _gameSettings.TypeGameEnum = TypeGameEnum.Subtraction;
            VisualTypeGameFocusButton();
            _subtractionButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniSubtractionFocus);
        }

        private void ClickAdditionButton(ClickEvent evt)
        {
            _gameSettings.TypeGameEnum = TypeGameEnum.Addition;
            VisualTypeGameFocusButton();
            _additionButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniAdditionFocus);
        }

        private void ClickHardButton(ClickEvent evt)
        {
            _gameSettings.ComplexityEnum = ComplexityEnum.Hard;
            VisualComplexityFocusButton();
            _hardButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonFocus);
        }

        private void ClickNormalButton(ClickEvent evt)
        {
            _gameSettings.ComplexityEnum = ComplexityEnum.Normal;
            VisualComplexityFocusButton();
            _normalButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonFocus);
        }

        private void ClickEasyButton(ClickEvent evt)
        {
            _gameSettings.ComplexityEnum = ComplexityEnum.Easy;
            VisualComplexityFocusButton();
            _easyButton.AddToClassList(GameSettingsMenuUiKey.StyleBackgroundButtonFocus);
        }

        private void ClickStartButton(ClickEvent evt)
        {
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Multiplication &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Easy)
                _profilePlayer.CurrentState.Value = GameState.MultiplicationEasyGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Multiplication &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Normal)
                _profilePlayer.CurrentState.Value = GameState.MultiplicationNormalGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Multiplication &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Hard)
                _profilePlayer.CurrentState.Value = GameState.MultiplicationHardGame;

            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Easy)
                _profilePlayer.CurrentState.Value = GameState.AdditionEasyGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Normal)
                _profilePlayer.CurrentState.Value = GameState.AdditionNormalGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Hard)
                _profilePlayer.CurrentState.Value = GameState.AdditionHardGame;

            if (_gameSettings.TypeGameEnum == TypeGameEnum.Division &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Easy)
                _profilePlayer.CurrentState.Value = GameState.DivisionEasyGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Division &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Normal)
                _profilePlayer.CurrentState.Value = GameState.DivisionNormalGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Division &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Hard)
                _profilePlayer.CurrentState.Value = GameState.DivisionHardGame;

            if (_gameSettings.TypeGameEnum == TypeGameEnum.Subtraction &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Easy)
                _profilePlayer.CurrentState.Value = GameState.SubtractionEasyGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Subtraction &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Normal)
                _profilePlayer.CurrentState.Value = GameState.SubtractionNormalGame;
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Subtraction &&
                _gameSettings.ComplexityEnum == ComplexityEnum.Hard)
                _profilePlayer.CurrentState.Value = GameState.SubtractionHardGame;
        }

        private void ClickBackButton(ClickEvent evt) => _profilePlayer.CurrentState.Value = GameState.MainMenu;

        private void AddElement()
        {

            _backButton = _root.Q<Button>(GameSettingsMenuUiKey.BackButton);
            _easyButton = _root.Q<Button>(GameSettingsMenuUiKey.EasyButton);
            _normalButton = _root.Q<Button>(GameSettingsMenuUiKey.NormalButton);
            _hardButton = _root.Q<Button>(GameSettingsMenuUiKey.HardButton);
            _additionButton = _root.Q<Button>(GameSettingsMenuUiKey.AdditionButton);
            _subtractionButton = _root.Q<Button>(GameSettingsMenuUiKey.Subtraction);
            _multiplyButton = _root.Q<Button>(GameSettingsMenuUiKey.MultiplyButton);
            _divisionButton = _root.Q<Button>(GameSettingsMenuUiKey.DivisionButton);
            _startButton = _root.Q<Button>(GameSettingsMenuUiKey.StartButton);
        }

        private void VisualComplexityFocusButton()
        {
            _easyButton.RemoveFromClassList(GameSettingsMenuUiKey.StyleBackgroundButtonFocus);
            _normalButton.RemoveFromClassList(GameSettingsMenuUiKey.StyleBackgroundButtonFocus);
            _hardButton.RemoveFromClassList(GameSettingsMenuUiKey.StyleBackgroundButtonFocus);
        }

        private void VisualTypeGameFocusButton()
        {
            _additionButton.RemoveFromClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniAdditionFocus);
            _subtractionButton.RemoveFromClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniSubtractionFocus);
            _multiplyButton.RemoveFromClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniMultiplyFocus);
            _divisionButton.RemoveFromClassList(GameSettingsMenuUiKey.StyleBackgroundButtonMiniDivisionFocus);
        }
    }
}