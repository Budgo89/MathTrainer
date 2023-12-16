using System;
using System.Collections;
using System.Collections.Generic;
using _Root.Scripts.Interfaces;
using _Root.Scripts.Models;
using _Root.Scripts.ScriptableObjects;
using _Root.Scripts.View;
using Controllers;
using Profile;
using TMPro;
using Tool;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class GameUIController : BaseController
    {
        private readonly ResourcePath _resourcePathFieldView = new ResourcePath("Ui/GameUI");
        
        private readonly ProfilePlayers _profilePlayer;
        private readonly IWorldGenerator _worldGenerator;
        private readonly GameController _gameController;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        
        private VisualElement _root;
        private Button _nextButton;
        private Label _points;
        private Label _attempts;
        private Label _answer;
        private Label _timer;

        private int _attemptsCount = 5;
        private int _timerCountBase = 7;
        private int _timerCount = 7;
        private int _pontsCount = 100;

        private Coroutine _timerCoroutine;


        public GameUIController(ProfilePlayers profilePlayer, IWorldGenerator worldGenerator,
            GameController gameController, UIDocument uiDocument, UiManager uiManager)
        {
            _profilePlayer = profilePlayer;
            _worldGenerator = worldGenerator;
            _gameController = gameController;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            
            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.GameUi;
            _root = _uiDocument.rootVisualElement;
            
            AddElement();
            Subscribe();
            _timerCoroutine = CoroutineController.StartRoutine(Timer());
        }
        
        private void AddElement()
        {
            _nextButton = _root.Q<Button>(GameUIKey.NextButton);
            
            _points = _root.Q<Label>(GameUIKey.PointsCount);
            _points.text = _pontsCount.ToString();
            
            _attempts = _root.Q<Label>(GameUIKey.Attempts);
            _attempts.text = _attemptsCount.ToString();
            
            _answer = _root.Q<Label>(GameUIKey.Answer);
            _answer.text = _gameController._victoryCondition.ToString();
            
            _timer = _root.Q<Label>(GameUIKey.Timer);
            _timer.text = _timerCountBase.ToString();

        }

        private void Subscribe()
        {
            _nextButton.RegisterCallback<ClickEvent>(ClickNextButton);
            _gameController.VictoryAction += VictoryAction;
            _gameController.DefeatAction += DefeatAction;
            _worldGenerator.RestartAction += UpdateResult;
        }

        private void UpdateResult(List<PointModel> obj)
        {
            _answer.text = _gameController._victoryCondition.ToString();
        }

        private void VictoryAction(int point)
        {
            _pontsCount += point;
            _points.text = _pontsCount.ToString();
            if (_timerCoroutine != null)
                CoroutineController.StopRoutine(_timerCoroutine);
            _worldGenerator.RestartGenerator();
            _timerCoroutine = CoroutineController.StartRoutine(Timer());
        }

        private void DefeatAction(int point)
        {
            _pontsCount -= point;
            _points.text = _pontsCount.ToString();
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
            Debug.Log("Просрал");
        }

        private void ClickNextButton(ClickEvent evt)
        {
            Debug.Log("Есть клик");
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

    }
}