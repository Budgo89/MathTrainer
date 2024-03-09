using _Root.Scripts.Interfaces;
using _Root.Scripts.Localizations;
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
        private readonly AudioModel _audioModel;
        private readonly ILocalization _localizationText;
        private VisualElement _root;

        private Button _buttonGame;
        private Button _buttonRecords;
        private Button _buttonSettings;
        private Button _buttonTutorial;
        

        public MainMenuUIController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager,
            AudioModel audioModel, ILocalization localizationText)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _audioModel = audioModel;
            _localizationText = localizationText;

            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.MainUi;
            _root = _uiDocument.rootVisualElement;
            
            AddElement();
            Subscribe();
        }

        private void Subscribe()
        {
            _buttonGame.RegisterCallback<ClickEvent>(ClickButtonGame);
            _buttonRecords.RegisterCallback<ClickEvent>(ClickButtonRecords);
            _buttonSettings.RegisterCallback<ClickEvent>(ClickButtonSettings);
            _buttonTutorial.RegisterCallback<ClickEvent>(ClickButtonTutorial);
            _buttonGame.RegisterCallback<ClickEvent>(AudioPlay);
            _buttonRecords.RegisterCallback<ClickEvent>(AudioPlay);
            _buttonSettings.RegisterCallback<ClickEvent>(AudioPlay);
            _buttonTutorial.RegisterCallback<ClickEvent>(AudioPlay);
        }

        private void ClickButtonTutorial(ClickEvent evt)
        {
            _buttonTutorial.RegisterCallback<TransitionEndEvent>(ClickButtonTutorial);
        }

        private void ClickButtonTutorial(TransitionEndEvent evt)
        {
            if (!_buttonTutorial.ClassListContains(MainMenuUIKey.StartButtonStyle) && evt.target == _buttonTutorial)
            {
                _profilePlayer.CurrentState.Value = GameState.TutorialUiController;
            }
            _buttonTutorial.UnregisterCallback<TransitionEndEvent>(ClickButtonTutorial);
        }

        private void ClickButtonGame(TransitionEndEvent evt)
        {
            if (!_buttonGame.ClassListContains(MainMenuUIKey.StartButtonStyle) && evt.target == _buttonGame)
            {
                _profilePlayer.CurrentState.Value = GameState.GameSettingsMenuUiController;
            }
            _buttonGame.UnregisterCallback<TransitionEndEvent>(ClickButtonGame);
        }

        private void AudioPlay(ClickEvent evt)
        {
            _audioModel.AudioEffects.clip = _audioModel.AudioEffectsManager.ButtonClick;
            _audioModel.AudioEffects.Play();
        }

        private void ClickButtonSettings(ClickEvent evt)
        {
            _buttonSettings.RegisterCallback<TransitionEndEvent>(ClickButtonSettings);
        }

        private void ClickButtonSettings(TransitionEndEvent evt)
        {
            if (!_buttonSettings.ClassListContains(MainMenuUIKey.SettingsButtonStyle) && evt.target == _buttonSettings)
            {
                _profilePlayer.CurrentState.Value = GameState.Settings;
            }
            _buttonSettings.UnregisterCallback<TransitionEndEvent>(ClickButtonSettings);
        }

        private void ClickButtonRecords(ClickEvent evt)
        {
            _buttonRecords.RegisterCallback<TransitionEndEvent>(ClickButtonRecords);
        }

        private void ClickButtonRecords(TransitionEndEvent evt)
        {
            if (!_buttonRecords.ClassListContains(MainMenuUIKey.RecordButtonStyle) && evt.target == _buttonRecords)
            {
                _profilePlayer.CurrentState.Value = GameState.Records;
            }
            _buttonRecords.UnregisterCallback<TransitionEndEvent>(ClickButtonRecords);
        }

        private void ClickButtonGame(ClickEvent evt)
        {
            _buttonGame.RegisterCallback<TransitionEndEvent>(ClickButtonGame);
        }

        private void AddElement()
        {
            _buttonGame = _root.Q<Button>(MainMenuUIKey.StartButtonKey);
            _buttonGame.text = _localizationText.GetPlyText();
            _buttonRecords = _root.Q<Button>(MainMenuUIKey.RecordButtonKey);
            _buttonRecords.text = _localizationText.GetRecordsText();
            _buttonSettings = _root.Q<Button>(MainMenuUIKey.SettingsButtonKey);
            _buttonSettings.text = _localizationText.GetSettingsText();
            _buttonTutorial = _root.Q<Button>(MainMenuUIKey.TutorialButtonKey);
            _buttonTutorial.text = _localizationText.GetTutorialText();
        }
    }
}