using _Root.Scripts.Controllers;
using _Root.Scripts.ScriptableObjects;
using MB;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UIElements;

internal class EntryPoint : MonoBehaviour
{

    [Header("Scene Objects")] 
    [SerializeField] private Transform _placeFor;

    [SerializeField] private SwipeDetection _swipeDetection;
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private UiManager _uiManager;

    private GameLevel _gameLevel;
    private MainController _mainController;
    private int _record;


    private void Start()
    {
        if (PlayerPrefs.HasKey(SaveKey.RecordKey))
        {
            _record = PlayerPrefs.GetInt(SaveKey.RecordKey);
        }
        else
        {
            _record = 0;
        }
        _mainController = new MainController(_placeFor, _swipeDetection, _uiDocument, _uiManager, _record);

    }

    private void Update()
    {
       
    }

}
