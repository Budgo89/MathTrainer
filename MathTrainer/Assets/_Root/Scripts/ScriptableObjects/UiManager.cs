using UnityEngine;
using UnityEngine.UIElements;

namespace _Root.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(UiManager), menuName = "UiManager/" + nameof(UiManager), order = 0)]
    public class UiManager : ScriptableObject
    {
        [SerializeField] private VisualTreeAsset _gameUi;
        [SerializeField] private VisualTreeAsset _mainUi;
        [SerializeField] private VisualTreeAsset _gameOverUi;
        [SerializeField] private VisualTreeAsset _typeGameUi;
        [SerializeField] private VisualTreeAsset _complexityUi;

        public VisualTreeAsset GameUi => _gameUi;
        public VisualTreeAsset MainUi => _mainUi;
        public VisualTreeAsset GameOverUi => _gameOverUi;
        public VisualTreeAsset TypeGameUi => _typeGameUi;
        public VisualTreeAsset ComplexityUi => _complexityUi;
    }
}