using System.Collections;
using System.Collections.Generic;
using _Root.Scripts.Enums;
using _Root.Scripts.Interfaces;
using _Root.Scripts.Models;
using _Root.Scripts.ScriptableObjects;
using Controllers;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class GameUIController : BaseController
    {
        private readonly ResourcePath _resourcePathFieldView = new ResourcePath("Ui/GameUI");

        private readonly ProfilePlayers _profilePlayers;
        private readonly IWorldGenerator _worldGenerator;
        private readonly GameController _gameController;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private readonly GameSettings _gameSettings;
        private readonly AudioModel _audioModel;

        private VisualElement _root;
        private Button _backButton;
        private Label _points;
        private Label _attempts;
        private Label _answer;
        private Label _timer;

        private float _pointCount = 100;
        private int _attemptsCount = 5;
        private int _timerCountBase = 7;
        private int _timerCount = 7;

        private Coroutine _timerCoroutine;
        private Coroutine _pauseCoroutine;
        private Coroutine _nextCoroutine;


        public GameUIController(ProfilePlayers profilePlayers, IWorldGenerator worldGenerator,
            GameController gameController, UIDocument uiDocument, UiManager uiManager, GameSettings gameSettings,
            AudioModel audioModel)
        {
            _profilePlayers = profilePlayers;
            _worldGenerator = worldGenerator;
            _gameController = gameController;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _gameSettings = gameSettings;
            _audioModel = audioModel;

            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.GameUi;
            _root = _uiDocument.rootVisualElement;
            
            AddElement();
            Subscribe();
            GetTimerCount();
            _timerCoroutine = CoroutineController.StartRoutine(Timer());
        }

        private void GetTimerCount()
        {
            if (_gameSettings.ComplexityEnum == ComplexityEnum.Easy)
                _timerCountBase = 7;
            if (_gameSettings.ComplexityEnum == ComplexityEnum.Normal)
                _timerCountBase = 14;
            if (_gameSettings.ComplexityEnum == ComplexityEnum.Hard)
                _timerCountBase = 20;
        }

        public float GetPointCount()
        {
            return _pointCount;
        }
        
        private void AddElement()
        {
            _backButton = _root.Q<Button>(GameUIKey.BackButton);
            
            _points = _root.Q<Label>(GameUIKey.PointsCount);
            _points.text = _pointCount.ToString();
            
            _attempts = _root.Q<Label>(GameUIKey.Attempts);
            _attempts.text = _attemptsCount.ToString();
            
            _answer = _root.Q<Label>(GameUIKey.Answer);
            _answer.text = _gameController._victoryCondition.ToString();
            
            _timer = _root.Q<Label>(GameUIKey.Timer);
            _timer.text = _timerCountBase.ToString();

        }

        private void Subscribe()
        {
            _backButton.RegisterCallback<ClickEvent>(ClickNextButton);
            _backButton.RegisterCallback<ClickEvent>(AudioPlay);
            _gameController.VictoryAction += VictoryAction;
            _gameController.DefeatAction += DefeatAction;
            _worldGenerator.RestartAction += UpdateResult;
        }
        
        private void AudioPlay(ClickEvent evt)
        {
            _audioModel.AudioEffects.clip = _audioModel.AudioEffectsManager.ButtonClick;
            _audioModel.AudioEffects.Play();
        }

        private void UpdateResult(List<PointModel> obj)
        {
            _answer.text = _gameController._victoryCondition.ToString();
        }

        private void VictoryAction(float point)
        {
            _pointCount += point;
            AddPoint(_pointCount);
            if (_timerCoroutine != null)
                CoroutineController.StopRoutine(_timerCoroutine);
            _worldGenerator.RestartGenerator();
            _timerCoroutine = CoroutineController.StartRoutine(Timer());
        }

        public void RemoveStylePoint()
        {
            _points.RemoveFromClassList(GameUIKey.PointStyle);
            _points.RemoveFromClassList(GameUIKey.PointStyle1000);
            _points.RemoveFromClassList(GameUIKey.PointStyle10000);
        }
        private void AddPoint(float pointCount)
        {
            RemoveStylePoint();
            if (pointCount >= 1000)
            {
                _points.AddToClassList(GameUIKey.PointStyle1000);
            }
            else if (pointCount <= -100)
            {
                _points.AddToClassList(GameUIKey.PointStyle1000);
            }
            else if (pointCount >= 10000)
            {
                _points.AddToClassList(GameUIKey.PointStyle10000);
            }
            else if (pointCount <= -1000)
            {
                _points.AddToClassList(GameUIKey.PointStyle10000);
            }
            else
            {
                _points.AddToClassList(GameUIKey.PointStyle);
            }
            
            _points.text = pointCount.ToString();
        }
        
        private void DefeatAction(float point)
        {
            _pointCount -= point;
            AddPoint(_pointCount);
            
            _attemptsCount -= 1;
            _attempts.text = _attemptsCount.ToString();
            if (_timerCoroutine != null)
                CoroutineController.StopRoutine(_timerCoroutine);
            if (_attemptsCount <= 0)
            {
                GameOver();
            }
            else
            {
                _worldGenerator.RestartGenerator();
                _timerCoroutine = CoroutineController.StartRoutine(Timer());
            }
        }

        private void GameOver()
        {
            _pauseCoroutine = CoroutineController.StartRoutine(Pause());
        }


        private void ClickNextButton(ClickEvent evt)
        {
            _worldGenerator.StopGenerator();
            if (_timerCoroutine != null)
                CoroutineController.StopRoutine(_timerCoroutine);
            _nextCoroutine = CoroutineController.StartRoutine(Next());
        }


        private void UnsubscribeButton()
        {
            _gameController.VictoryAction -= VictoryAction;
            _gameController.DefeatAction -= DefeatAction;
        }
        
        protected override void OnDispose()
        {
            UnsubscribeButton();
        }

        private IEnumerator Timer()
        {
            _timerCount = _timerCountBase;
            for (int i = _timerCountBase; i > 0; i--)
            {
                yield return new WaitForSeconds(1f);
                _timerCount--;
                _timer.text = _timerCount.ToString();
            }
            if (_timerCoroutine != null)
                CoroutineController.StopRoutine(_timerCoroutine);
            DefeatAction(_gameController._victoryCondition);

        }
        
        private IEnumerator Pause()
        {
            yield return new WaitForSeconds(1f);
            _profilePlayers.CurrentState.Value = GameState.GameOver;
            CoroutineController.StopRoutine(_pauseCoroutine);
        }
        
        private IEnumerator Next()
        {
            yield return new WaitForSeconds(1f);
            _profilePlayers.CurrentState.Value = GameState.MainMenu;
            CoroutineController.StopRoutine(_nextCoroutine);
        }

    }
}