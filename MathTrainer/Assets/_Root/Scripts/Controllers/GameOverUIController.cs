using _Root.Scripts.Enums;
using _Root.Scripts.Models;
using _Root.Scripts.ScriptableObjects;
using Controllers;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class GameOverUIController : BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private readonly Records _records;
        private readonly float _pointCount;
        private readonly GameSettings _gameSettings;
        private readonly AudioModel _audioModel;

        private VisualElement _root;

        private Button _mainMenuButton;
        private Label _resultLabel;
        private VisualElement _recordsMultiplication;
        private VisualElement _recordsDivision;
        private VisualElement _recordsSubtraction;
        private VisualElement _recordsAddition;
        private Label _recordsMultiplicationText;
        private Label _recordsDivisionText;
        private Label _recordsSubtractionText;
        private Label _recordsAdditionText;

        public GameOverUIController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager,
            Records records, float pointCount, GameSettings gameSettings, AudioModel audioModel)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _records = records;
            _pointCount = pointCount;
            _gameSettings = gameSettings;
            _audioModel = audioModel;

            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.GameOverUi;
            _root = _uiDocument.rootVisualElement;
            
            AddElement();
            Subscribe();
            RecordUpdate();
        }

        private void RecordUpdate()
        {
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Multiplication)
            {
                var record = SaveRecord(_records.RecordMultiplication);
                _recordsMultiplication.style.display = DisplayStyle.Flex;
                _recordsMultiplicationText.text = record.ToString();

            }
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition)
            {
                var record = SaveRecord(_records.RecordAddition);
                _recordsAddition.style.display = DisplayStyle.Flex;
                _recordsAdditionText.text = record.ToString();
            }
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Division)
            {
                var record = SaveRecord(_records.RecordDivision);
                _recordsDivision.style.display = DisplayStyle.Flex;
                _recordsDivisionText.text = record.ToString();
            }
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Subtraction)
            {
                var record = SaveRecord(_records.RecordSubtraction);
                _recordsSubtraction.style.display = DisplayStyle.Flex;
                _recordsSubtractionText.text = record.ToString();
            }
            
            _resultLabel.text = _pointCount.ToString();
        }

        private float SaveRecord(float record)
        {
            if (record < _pointCount)
            {
                record = _pointCount;
                Save(record);
            }

            return record;
        }

        private void Save(float record)
        {
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Multiplication)
            {
                _records.RecordMultiplication = record;
                PlayerPrefs.SetFloat(SaveKey.RecordMultiplicationKey, record);
            }
                
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition)
            {
                _records.RecordAddition = record;
                PlayerPrefs.SetFloat(SaveKey.RecordAdditionKey, record);
            }
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Division)
            {
                _records.RecordDivision = record;
                PlayerPrefs.SetFloat(SaveKey.RecordDivisionKey, record);
            }
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Subtraction)
            {
                _records.RecordSubtraction = record;
                PlayerPrefs.SetFloat(SaveKey.RecordSubtractionKey, record);
            }
        }

        private void Subscribe()
        {
            _mainMenuButton.RegisterCallback<ClickEvent>(ClickMainMenuButton);
            _mainMenuButton.RegisterCallback<ClickEvent>(AudioPlay);
        }
        
        private void AudioPlay(ClickEvent evt)
        {
            _audioModel.AudioEffects.clip = _audioModel.AudioEffectsManager.ButtonClick;
            _audioModel.AudioEffects.Play();
        }

        private void ClickMainMenuButton(ClickEvent evt) => _profilePlayer.CurrentState.Value = GameState.MainMenu;

        private void AddElement()
        {
            _mainMenuButton = _root.Q<Button>(GameOverKey.MenuButtonKey);
            _resultLabel = _root.Q<Label>(GameOverKey.ResultCountKey);
            
            _recordsMultiplication = _root.Q<VisualElement>(GameOverKey.RecordsMultiplication);
            _recordsDivision = _root.Q<VisualElement>(GameOverKey.RecordsDivision);
            _recordsSubtraction = _root.Q<VisualElement>(GameOverKey.RecordsSubtraction);
            _recordsAddition = _root.Q<VisualElement>(GameOverKey.RecordsAddition);
            _recordsMultiplicationText = _root.Q<Label>(GameOverKey.RecordsMultiplicationText);
            _recordsDivisionText = _root.Q<Label>(GameOverKey.RecordsDivisionText);
            _recordsSubtractionText = _root.Q<Label>(GameOverKey.RecordsSubtractionText);
            _recordsAdditionText = _root.Q<Label>(GameOverKey.RecordsAdditionText);
        }
    }
}