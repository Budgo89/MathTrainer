using _Root.Scripts.ScriptableObjects;
using Controllers;
using Profile;
using Tool;
using UnityEngine.UIElements;

namespace _Root.Scripts.Controllers
{
    public class TutorialUiController : BaseController
    {
        private readonly ProfilePlayers _profilePlayer;
        private readonly UIDocument _uiDocument;
        private readonly UiManager _uiManager;
        private VisualElement _root;

        private VisualElement _item2;

        public TutorialUiController(ProfilePlayers profilePlayer, UIDocument uiDocument, UiManager uiManager)
        {
            _profilePlayer = profilePlayer;
            _uiDocument = uiDocument;
            _uiManager = uiManager;
            
            _uiDocument.rootVisualElement.Clear();
            _uiDocument.visualTreeAsset = _uiManager.TutorialUI;
            _root = _uiDocument.rootVisualElement;
            
            AddElement();
            Subscribe();
        }

        private void AddElement()
        {
            _item2 = _root.Q<VisualElement>(TutorialUIKey.Item9);
            _item2.AddToClassList(TutorialUIKey.Item9Position2);
        }

        private void Subscribe()
        {
            
        }
    }
}