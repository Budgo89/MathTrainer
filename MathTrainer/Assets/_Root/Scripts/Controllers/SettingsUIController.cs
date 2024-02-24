using System;
using _Root.Scripts.Models;
using _Root.Scripts.ScriptableObjects;
using Controllers;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class SettingsUIController: BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private readonly AudioModel _audioModel;
        
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
        
        

        public SettingsUIController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager, AudioModel audioModel)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _audioModel = audioModel;
            
            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.SettingsUI;
            _root = _uiDocument.rootVisualElement;
            
            AddElement();
            Subscribe();
        }
        
        private void AddElement()
        {
            _backButton = _root.Q<Button>(SettingsUIKey.BackButton);
            _visualElementOnSound = _root.Q<VisualElement>(SettingsUIKey.SoundOnIcon);
            _visualElementOffSound = _root.Q<VisualElement>(SettingsUIKey.SoundOffIcon);
            _progressText = _root.Q<Label>(SettingsUIKey.ProgressText);
            AddButtonSound();
            AddSlider();
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
            if (PlayerPrefs.HasKey(SaveKey.AudioValue))
            {
                value = PlayerPrefs.GetFloat(SaveKey.AudioValue);
            }
            _slider.value = value;
            IndicateValueOnSound();

        }

            private void ClickBackButton(ClickEvent evt)
            {
                PlayerPrefs.SetFloat(SaveKey.AudioValue, _slider.value);
                int isAudio = _isAudio == true ? 1 : 0;
                
                PlayerPrefs.SetInt(SaveKey.IsAudio, isAudio);
                
                _profilePlayer.CurrentState.Value = GameState.MainMenu;
            }
    }
}