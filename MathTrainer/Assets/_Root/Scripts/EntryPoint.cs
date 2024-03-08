using System;
using _Root.Scripts.Controllers;
using _Root.Scripts.Interfaces;
using _Root.Scripts.Localizations;
using _Root.Scripts.Models;
using _Root.Scripts.ScriptableObjects;
using MB;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

internal class EntryPoint : MonoBehaviour
{

    [Header("Scene Objects")] 
    [SerializeField] private Transform _placeFor;

    [SerializeField] private SwipeDetection _swipeDetection;
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private UiManager _uiManager;
    
    [Header("Audio")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioEffectsManager _audioEffectsManager;
    [SerializeField] private AudioSource _audioEffects;

    private GameLevel _gameLevel;
    private MainController _mainController;
    private Records _records;
    private LocalizationModel _localizationText;


    private void Start()
    {
        _records = LoadRecords();
        _localizationText = LoadLocalizationText();

        AudioModel audioModel = new AudioModel(_audioMixer, _audioEffectsManager, _audioEffects);
        LoadVolumeAudio();

        _mainController = new MainController(_placeFor, _swipeDetection, _uiDocument, _uiManager, _records, audioModel,
            _localizationText);

    }

    private LocalizationModel LoadLocalizationText()
    {
        Localization<ILocalizationText> localization;
        int markerLocalizationText = 0;
        if (PlayerPrefs.HasKey(SaveKey.LocalizationKey))
        {
            markerLocalizationText = PlayerPrefs.GetInt(SaveKey.LocalizationKey);
            localization = markerLocalizationText == 0
                ? new EnglishLocalization(new EnglishText())
                : new RussianLocalization(new RussianText());
        }
        else
        {
            if (Application.systemLanguage == SystemLanguage.English)
            {
                localization = new EnglishLocalization(new EnglishText());
                markerLocalizationText = 0;
            }
            else
            {
                localization = new RussianLocalization(new RussianText());
                markerLocalizationText = 1;
            }
        }

        return new LocalizationModel(markerLocalizationText, localization);
    }

    private Records LoadRecords()
    {
        var records = new Records();
        
        if (PlayerPrefs.HasKey(SaveKey.RecordMultiplicationKey))
        {
            records.RecordMultiplication = PlayerPrefs.GetFloat(SaveKey.RecordMultiplicationKey);
        }
        if (PlayerPrefs.HasKey(SaveKey.RecordAdditionKey))
        {
            records.RecordAddition = PlayerPrefs.GetFloat(SaveKey.RecordAdditionKey);
        }
        if (PlayerPrefs.HasKey(SaveKey.RecordDivisionKey))
        {
            records.RecordDivision = PlayerPrefs.GetFloat(SaveKey.RecordDivisionKey);
        }
        if (PlayerPrefs.HasKey(SaveKey.RecordSubtractionKey))
        {
            records.RecordSubtraction = PlayerPrefs.GetFloat(SaveKey.RecordSubtractionKey);
        }

        return records;
    }

    private void LoadVolumeAudio()
    {
        if (PlayerPrefs.HasKey(SaveKey.IsAudio))
        {
            int isAudio = PlayerPrefs.GetInt(SaveKey.IsAudio);
            if (isAudio == 0)
            {
                float value = -100f;
                _audioMixer.SetFloat("Volume", value);
                return;
            }
        }
        
        float volume = 0;
        if (PlayerPrefs.HasKey(SaveKey.AudioValueKey))
        {
            volume = PlayerPrefs.GetFloat(SaveKey.AudioValueKey);
            float value = (float)(Math.Log10(volume / 100) * 20);
            _audioMixer.SetFloat("Volume", value);
        }
        else
        {
            float value = 0f;
            _audioMixer.SetFloat("Volume", value);
        }
    }

    private void Update()
    {
        _mainController?.Update();
    }

}
