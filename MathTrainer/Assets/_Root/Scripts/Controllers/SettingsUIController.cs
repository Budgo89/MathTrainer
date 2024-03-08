using System;
using _Root.Scripts.Interfaces;
using _Root.Scripts.Localizations;
using _Root.Scripts.Models;
using _Root.Scripts.ScriptableObjects;
using Controllers;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class SettingsUIController : BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private readonly AudioModel _audioModel;
        private LocalizationModel _localizationText;

        private VisualElement _root;

        private Button _backButton;
        private Button _buttonOnSound;
        private Button _buttonOffSound;
        private VisualElement _visualElementOnSound;
        private VisualElement _visualElementOffSound;
        private Slider _slider;
        private VisualElement _bar;
        private VisualElement _dragger;
        private Label _progressText;
        private bool _isAudio;

        private Button _languageEngButton;
        private Button _languageRusButton;
        private VisualElement _engIcon;
        private VisualElement _rusIcon;

        private Label _volumeText;
        private Label _languageText;
        private Label _settingsText;


        public SettingsUIController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager,
            AudioModel audioModel, LocalizationModel localizationText)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _audioModel = audioModel;
            _localizationText = localizationText;

            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.SettingsUI;
            _root = _uiDocument.rootVisualElement;

            AddElement();
            Subscribe();

            if (_localizationText.MarkerLocalization == 1)
            {
                ClickRusButton();
            }
            else
            {
                ClickEngButton();
            }
        }

        private void AddElement()
        {
            _backButton = _root.Q<Button>(SettingsUIKey.BackButton);
            _visualElementOnSound = _root.Q<VisualElement>(SettingsUIKey.SoundOnIcon);
            _visualElementOffSound = _root.Q<VisualElement>(SettingsUIKey.SoundOffIcon);
            _progressText = _root.Q<Label>(SettingsUIKey.ProgressText);
            _settingsText = _root.Q<Label>(SettingsUIKey.SettingsText);
            _volumeText = _root.Q<Label>(SettingsUIKey.VolumeText);
            _languageText = _root.Q<Label>(SettingsUIKey.LanguageText);
            AddButtonSound();
            AddLocalization();
            AddSlider();
        }

        private void AddLocalization()
        {
            _languageEngButton = _root.Q<Button>(SettingsUIKey.LanguageEngButton);
            _languageRusButton = _root.Q<Button>(SettingsUIKey.LanguageRusButton);
            _engIcon = _root.Q<VisualElement>(SettingsUIKey.EngIcon);
            _rusIcon = _root.Q<VisualElement>(SettingsUIKey.RusIcon);
        }

        private void AddButtonSound()
        {
            _buttonOnSound = _root.Q<Button>(SettingsUIKey.SoundOnButton);
            _buttonOffSound = _root.Q<Button>(SettingsUIKey.SoundOffButton);

            if (PlayerPrefs.HasKey(SaveKey.IsAudio))
            {
                int isAudio = PlayerPrefs.GetInt(SaveKey.IsAudio);

                _isAudio = isAudio != 0;
                if (!_isAudio)
                {
                    float value = -100f;
                    _audioModel.AudioMixer.SetFloat("Volume", value);

                    RemoveStyle();
                    _visualElementOnSound.AddToClassList(SettingsUIKey.StyleSoundOnButtonDeActive);
                    _visualElementOffSound.AddToClassList(SettingsUIKey.StyleSoundOffActive);
                }
                else
                {
                    RemoveStyle();
                    _visualElementOnSound.AddToClassList(SettingsUIKey.StyleSoundOnButtonActive);
                    _visualElementOffSound.AddToClassList(SettingsUIKey.StyleSoundOffDeActive);
                }
            }
            else
            {
                RemoveStyle();
                _visualElementOnSound.AddToClassList(SettingsUIKey.StyleSoundOnButtonActive);
                _visualElementOffSound.AddToClassList(SettingsUIKey.StyleSoundOffDeActive);
            }
        }

        private void Subscribe()
        {
            _backButton.RegisterCallback<ClickEvent>(ClickBackButton);
            _buttonOnSound.RegisterCallback<ClickEvent>(ClickButtonOnSound);
            _buttonOffSound.RegisterCallback<ClickEvent>(ClickButtonOffSound);
            _slider.RegisterCallback<ChangeEvent<float>>(ClickSlider);

            _backButton.RegisterCallback<ClickEvent>(AudioPlay);
            _buttonOnSound.RegisterCallback<ClickEvent>(AudioPlay);
            _buttonOffSound.RegisterCallback<ClickEvent>(AudioPlay);

            _languageEngButton.RegisterCallback<ClickEvent>(ClickEngButton);
            _languageRusButton.RegisterCallback<ClickEvent>(ClickRusButton);
            
            _languageEngButton.RegisterCallback<ClickEvent>(AudioPlay);
            _languageRusButton.RegisterCallback<ClickEvent>(AudioPlay);
        }

        private void LocalizationVisual()
        {
            _volumeText.text = _localizationText.Localization.GetVolumeText();
            _languageText.text = _localizationText.Localization.GetLanguageText();
            _settingsText.text = _localizationText.Localization.GetSettingsText();
        }

        private void ClickRusButton()
        {
            RemoveStyleLocalization();
            _engIcon.AddToClassList(SettingsUIKey.LocalizationOffStyle);
            _rusIcon.AddToClassList(SettingsUIKey.LocalizationOnStyle);
            _localizationText.Localization = new RussianLocalization(new RussianText());
            _localizationText.MarkerLocalization = 1;
            LocalizationVisual();
            PlayerPrefs.SetInt(SaveKey.LocalizationKey, 1);
        }
        
        private void ClickRusButton(ClickEvent evt)
        {
            ClickRusButton();
        }

        private void ClickEngButton()
        {
            RemoveStyleLocalization();
            _engIcon.AddToClassList(SettingsUIKey.LocalizationOnStyle);
            _rusIcon.AddToClassList(SettingsUIKey.LocalizationOffStyle);
            _localizationText.Localization = new EnglishLocalization(new EnglishText());
            _localizationText.MarkerLocalization = 0;
            LocalizationVisual();
            PlayerPrefs.SetInt(SaveKey.LocalizationKey, 0);
        }
        private void ClickEngButton(ClickEvent evt)
        {
            ClickEngButton();
        }

        private void RemoveStyleLocalization()
        {
            _engIcon.RemoveFromClassList(SettingsUIKey.LocalizationOnStyle);
            _engIcon.RemoveFromClassList(SettingsUIKey.LocalizationOffStyle);
            _rusIcon.RemoveFromClassList(SettingsUIKey.LocalizationOnStyle);
            _rusIcon.RemoveFromClassList(SettingsUIKey.LocalizationOffStyle);
        }

        private void AudioPlay(ClickEvent evt)
        {
            _audioModel.AudioEffects.clip = _audioModel.AudioEffectsManager.ButtonClick;
            _audioModel.AudioEffects.Play();
        }

        private void ClickSlider(ChangeEvent<float> evt)
        {
            if (_isAudio)
            {
                float volume = _slider.value / 100;

                volume = volume == 0 ? 0.00001f : volume;
                float value = (float)(Math.Log10(volume) * 20);
                _audioModel.AudioMixer.SetFloat("Volume", value);

                IndicateValueOnSound();
            }
            else
            {
                IndicateValueOffSound();
            }

        }

        private void ClickButtonOffSound(ClickEvent evt)
        {
            RemoveStyle();
            _visualElementOnSound.AddToClassList(SettingsUIKey.StyleSoundOnButtonDeActive);
            _visualElementOffSound.AddToClassList(SettingsUIKey.StyleSoundOffActive);
            _isAudio = false;
            float value = -100f;
            _audioModel.AudioMixer.SetFloat("Volume", value);

            IndicateValueOffSound();
        }

        private void RemoveStyle()
        {
            _visualElementOnSound.RemoveFromClassList(SettingsUIKey.StyleSoundOnButtonActive);
            _visualElementOnSound.RemoveFromClassList(SettingsUIKey.StyleSoundOnButtonDeActive);
            _visualElementOffSound.RemoveFromClassList(SettingsUIKey.StyleSoundOffActive);
            _visualElementOffSound.RemoveFromClassList(SettingsUIKey.StyleSoundOffDeActive);
        }

        private void ClickButtonOnSound(ClickEvent evt)
        {
            RemoveStyle();
            _visualElementOnSound.AddToClassList(SettingsUIKey.StyleSoundOnButtonActive);
            _visualElementOffSound.AddToClassList(SettingsUIKey.StyleSoundOffDeActive);
            _isAudio = true;
            float volume = _slider.value / 100;
            volume = volume == 0 ? 0.00001f : volume;
            float value = (float)(Math.Log10(volume) * 20);
            _audioModel.AudioMixer.SetFloat("Volume", value);

            IndicateValueOnSound();
        }

        private void IndicateValueOffSound()
        {
            string progressText = 0 + "%";
            _progressText.text = progressText;
        }

        private void IndicateValueOnSound()
        {
            int rogress = (int)_slider.value;
            string progressText = rogress + "%";
            _progressText.text = progressText;
        }

        private void AddSlider()
        {
            _slider = _root.Q<Slider>(SettingsUIKey.SoundSlider);

            _dragger = _root.Q<VisualElement>(SettingsUIKey.UnityDragger);
            _bar = new VisualElement();
            _dragger.Add(_bar);
            _bar.AddToClassList(SettingsUIKey.Bar);
            _bar.style.alignSelf = new StyleEnum<Align>(Align.FlexEnd);

            float value = 100;
            if (PlayerPrefs.HasKey(SaveKey.AudioValueKey))
            {
                value = PlayerPrefs.GetFloat(SaveKey.AudioValueKey);
            }

            _slider.value = value;
            IndicateValueOnSound();

        }

        private void ClickBackButton(ClickEvent evt)
        {
            PlayerPrefs.SetFloat(SaveKey.AudioValueKey, _slider.value);
            int isAudio = _isAudio == true ? 1 : 0;

            PlayerPrefs.SetInt(SaveKey.IsAudio, isAudio);
            _backButton.RegisterCallback<TransitionEndEvent>(ClickBackButton);
        }

        private void ClickBackButton(TransitionEndEvent evt)
        {
            if (!_backButton.ClassListContains(SettingsUIKey.BackButtonStyle) && evt.target == _backButton)
            {
                _profilePlayer.CurrentState.Value = GameState.MainMenu;
            }

            _backButton.UnregisterCallback<TransitionEndEvent>(ClickBackButton);
        }
    }
}