using _Root.Scripts.Models;
using _Root.Scripts.ScriptableObjects;
using Controllers;
using Profile;
using Tool;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class MainMenuUIController : BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private readonly Records _records;
        private VisualElement _root;

        private Button _buttonGame;
        private Label _recordLabel;

        public MainMenuUIController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager,
            Records records)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _records = records;

            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.MainUi;
            _root = _uiDocument.rootVisualElement;
            
            AddElement();
            Subscribe();
            _recordLabel.text = GetRecord().ToString();
            
        }

        private int GetRecord()
        {
            var record = _records.RecordMultiplication;
            if (record < _records.RecordAddition)
                record = _records.RecordAddition;
            if (record < _records.RecordDivision)
                record = _records.RecordDivision;
            if (record < _records.RecordSubtraction)
                record = _records.RecordSubtraction;
            return record;
        }

        private void Subscribe()
        {
            _buttonGame.RegisterCallback<ClickEvent>(ClickButtonGame);
        }

        private void ClickButtonGame(ClickEvent evt) => _profilePlayer.CurrentState.Value = GameState.TypeGame;

        private void AddElement()
        {
            _buttonGame = _root.Q<Button>(MainMenuUIKey.StartButtonKey);
            _recordLabel = _root.Q<Label>(MainMenuUIKey.RecordCountKey);
        }
    }
}