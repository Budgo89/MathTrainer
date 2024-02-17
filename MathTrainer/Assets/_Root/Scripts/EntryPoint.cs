﻿using System;
using _Root.Scripts.Controllers;
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


    private void Start()
    {
        _records = new Records();
        if (PlayerPrefs.HasKey(SaveKey.RecordMultiplicationKey))
        {
            _records.RecordMultiplication = PlayerPrefs.GetInt(SaveKey.RecordMultiplicationKey);
        }
        if (PlayerPrefs.HasKey(SaveKey.RecordAdditionKey))
        {
            _records.RecordAddition = PlayerPrefs.GetInt(SaveKey.RecordAdditionKey);
        }
        if (PlayerPrefs.HasKey(SaveKey.RecordDivisionKey))
        {
            _records.RecordDivision = PlayerPrefs.GetInt(SaveKey.RecordDivisionKey);
        }
        if (PlayerPrefs.HasKey(SaveKey.RecordSubtractionKey))
        {
            _records.RecordSubtraction = PlayerPrefs.GetInt(SaveKey.RecordSubtractionKey);
        }

        AudioModel audioModel = new AudioModel(_audioMixer, _audioEffectsManager, _audioEffects);
        LoadVolumeAudio();
        
        _mainController = new MainController(_placeFor, _swipeDetection, _uiDocument, _uiManager, _records, audioModel);

    }
    
    private void LoadVolumeAudio()
    {
        if (PlayerPrefs.HasKey(SaveKey.IsAudio))
        {
            int isAudio = PlayerPrefs.GetInt(SaveKey.IsAudio);
            if (isAudio == 0)
            {
                _audioMixer.SetFloat("Volume", -100);
                return;
            }
                
        }
        
        float volume = 1;
        if (PlayerPrefs.HasKey(SaveKey.AudioValue))
        {
            volume = PlayerPrefs.GetFloat(SaveKey.AudioValue);
        }
        var a = (float)(Math.Log10(volume) * 20);
        _audioMixer.SetFloat("Volume", (float)(Math.Log10(volume/100) * 20));
    }

    private void Update()
    {
        _mainController?.Update();

    }

}
