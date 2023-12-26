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
        private readonly int _pointCount;
        private readonly GameSettings _gameSettings;

        private VisualElement _root;

        private Button _mainMenuButton;
        private Label _resultLabel;
        private Label _recordLabel;

        public GameOverUIController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager,
            Records records, int pointCount, GameSettings gameSettings)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _records = records;
            _pointCount = pointCount;
            _gameSettings = gameSettings;

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
                AddRecord(_records.RecordMultiplication);
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition)
                AddRecord(_records.RecordAddition);
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Division)
                AddRecord(_records.RecordDivision);
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Subtraction)
                AddRecord(_records.RecordSubtraction);
        }

        private void AddRecord(int record)
        {
            if (record < _pointCount)
            {
                record = _pointCount;
                Save(record);
            }

            _recordLabel.text = record.ToString();
            _resultLabel.text = _pointCount.ToString();
        }

        private void Save(int record)
        {
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Multiplication)
            {
                _records.RecordMultiplication = record;
                PlayerPrefs.SetInt(SaveKey.RecordMultiplicationKey, record);
            }
                
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Addition)
            {
                _records.RecordAddition = record;
                PlayerPrefs.SetInt(SaveKey.RecordAdditionKey, record);
            }
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Division)
            {
                _records.RecordDivision = record;
                PlayerPrefs.SetInt(SaveKey.RecordDivisionKey, record);
            }
            if (_gameSettings.TypeGameEnum == TypeGameEnum.Subtraction)
            {
                _records.RecordSubtraction = record;
                PlayerPrefs.SetInt(SaveKey.RecordSubtractionKey, record);
            }
        }

        private void Subscribe()
        {
            _mainMenuButton.RegisterCallback<ClickEvent>(ClickMainMenuButton);
        }

        private void ClickMainMenuButton(ClickEvent evt) => _profilePlayer.CurrentState.Value = GameState.MainMenu;

        private void AddElement()
        {
            _mainMenuButton = _root.Q<Button>(GameOverKey.MenuButtonKey);
            _resultLabel = _root.Q<Label>(GameOverKey.ResultCountKey);
            _recordLabel = _root.Q<Label>(GameOverKey.RecordCountKey);
        }
    }
}