using _Root.Scripts.Models;
using _Root.Scripts.ScriptableObjects;
using Controllers;
using Profile;
using Tool;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class RecordsUIController : BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private readonly Records _records;
        private readonly AudioModel _audioModel;

        private VisualElement _root;
        
        private Button _backButton;
        private Label _additionRecordText;
        private Label _subtractionRecordsText;
        private Label _multiplicationRecordsText;
        private Label _divisionRecordsText;

        public RecordsUIController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager,
            Records records, AudioModel audioModel)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _records = records;
            _audioModel = audioModel;

            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.RecordsUI;
            _root = _uiDocument.rootVisualElement;

            AddElement();
            Subscribe();
            VisualRecords();
        }

        private void VisualRecords()
        {
            _additionRecordText.text = _records.RecordAddition.ToString();
            _subtractionRecordsText.text = _records.RecordSubtraction.ToString();
            _multiplicationRecordsText.text = _records.RecordMultiplication.ToString();
            _divisionRecordsText.text = _records.RecordDivision.ToString();
        }

        private void Subscribe()
        {
            _backButton.RegisterCallback<ClickEvent>(ClickBackButton);
            _backButton.RegisterCallback<ClickEvent>(AudioPlay);
        }
        
        private void AudioPlay(ClickEvent evt)
        {
            _audioModel.AudioEffects.clip = _audioModel.AudioEffectsManager.ButtonClick;
            _audioModel.AudioEffects.Play();
        }

        private void AddElement()
        {
            _backButton = _root.Q<Button>(RecordsUiKey.BackButton);
            _additionRecordText = _root.Q<Label>(RecordsUiKey.AdditionRecordText);
            _subtractionRecordsText = _root.Q<Label>(RecordsUiKey.SubtractionRecordsText);
            _multiplicationRecordsText = _root.Q<Label>(RecordsUiKey.MultiplicationRecordsText);
            _divisionRecordsText = _root.Q<Label>(RecordsUiKey.DivisionRecordsText);
        }
        
        private void ClickBackButton(ClickEvent evt) => _profilePlayer.CurrentState.Value = GameState.MainMenu;
    }
}