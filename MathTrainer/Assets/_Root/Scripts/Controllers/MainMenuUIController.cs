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
        private VisualElement _root;

        private Button _buttonGame;
        private Button _buttonRecords;
        private Button _buttonSettings;

        public MainMenuUIController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager,
            AudioModel audioModel)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _audioModel = audioModel;

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
            _buttonGame.RegisterCallback<ClickEvent>(AudioPlay);
            _buttonRecords.RegisterCallback<ClickEvent>(AudioPlay);
            _buttonSettings.RegisterCallback<ClickEvent>(AudioPlay);
        }

        private void AudioPlay(ClickEvent evt)
        {
            _audioModel.AudioEffects.clip = _audioModel.AudioEffectsManager.ButtonClick;
            _audioModel.AudioEffects.Play();
        }

        private void ClickButtonSettings(ClickEvent evt) => _profilePlayer.CurrentState.Value = GameState.Settings;

        private void ClickButtonRecords(ClickEvent evt) => _profilePlayer.CurrentState.Value = GameState.Records;

        private void ClickButtonGame(ClickEvent evt) => _profilePlayer.CurrentState.Value = GameState.GameSettingsMenuUiController;

        private void AddElement()
        {
            _buttonGame = _root.Q<Button>(MainMenuUIKey.StartButtonKey);
            _buttonRecords = _root.Q<Button>(MainMenuUIKey.RecordButtonKey);
            _buttonSettings = _root.Q<Button>(MainMenuUIKey.SettingsButton);
        }
    }
}