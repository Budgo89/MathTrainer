using _Root.Scripts.Controllers;
using _Root.Scripts.Models;
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
        
        _mainController = new MainController(_placeFor, _swipeDetection, _uiDocument, _uiManager, _records);

    }

    private void Update()
    {
       
    }

}
