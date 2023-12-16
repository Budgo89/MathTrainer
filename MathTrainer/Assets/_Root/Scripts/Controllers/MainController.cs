using _Root.Scripts.Interfaces;
using _Root.Scripts.ScriptableObjects;
using Controllers;
using MB;
using Profile;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

namespace _Root.Scripts.Controllers
{
    public class MainController : BaseController
    {
        private readonly Transform _placeFor;
        private readonly SwipeDetection _swipeDetection;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private readonly int _record;
        
        private ProfilePlayers _profilePlayer;

        private IWorldGenerator _worldGenerator;
        private GameController _gameController;
        private GameUIController _gameUIController;

        public MainController(Transform placeFor, SwipeDetection swipeDetection, UIDocument uiDocument,
            UiManager uiManager, int record)
        {
            _placeFor = placeFor;
            _swipeDetection = swipeDetection;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            _record = record;
            _profilePlayer = new ProfilePlayers(GameState.Game);
            _profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);
        }

        private void OnChangeGameState(GameState state)
        {
            DisposeControllers();
            switch (state)
            {
                case GameState.MainMenu:
                    break;
                case GameState.Game:
                    _worldGenerator = new WorldGeneratorController(_placeFor);
                    _worldGenerator.StartGenerator();
                    _gameController = new GameController(_profilePlayer, _worldGenerator, _swipeDetection);
                    _gameUIController = new GameUIController(_profilePlayer, _worldGenerator, _gameController, _uiDocument,_uiManager);
                    break;
            }
        }

        private void DisposeControllers()
        {
            _worldGenerator?.Dispose();
        }
    }
}