using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    public Collider _field;

    [SerializeField] private Text _countOfCoinTxt;
    [SerializeField] private TransitionBetweenLevels _transitionBetweenLevels;
    [SerializeField] private Material _transitionBetweenLevelsMaterial;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private PausePanel _pausePanel;
    [SerializeField] private CharacterController prefab;
    [SerializeField] private GameObject _gameOverPanel;
    
    private CharacterController _character;
    private int _countOfCoin;
    
    private GameModel _gameModel;


    public void Init(GameModel gameModel)
    {
        _gameModel = gameModel;
        _transitionBetweenLevels.SetTransitionState(false);
        _gameModel.SpawnCharacterEvent += SpawnCharacter;
        _gameModel.DieUnitEvent += IncreaseCountOfCoin;
        _gameModel.AllEnemiesKilled += OpenTransitionBetweenLevels;
        _countOfCoin = 0;
        _transitionBetweenLevelsMaterial.color = new Color(1f, 0.36f, 0.32f);

        _pauseButton.onClick.AddListener(PauseGame);
        _pausePanel.SetGameModel(gameModel);
        SetOrthographicSizeCamera();
        
        Debug.Log("ViewManager starting");
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        _gameModel.EndModel();
        _pauseButton.gameObject.SetActive(false);
        _pausePanel.gameObject.SetActive(true);
    }

    private void OpenTransitionBetweenLevels()
    {
     _transitionBetweenLevelsMaterial.color = new Color(0.3f, 1f, 0.51f);
     _transitionBetweenLevels.SetTransitionState(true);
    }

    private void SetOrthographicSizeCamera()
    {
        Camera _camera = Camera.main;
        if (_camera.pixelHeight > _camera.pixelWidth)
        {
            _camera.orthographicSize =
                (_field.bounds.size.x + 1) * _camera.pixelHeight / _camera.pixelWidth * 0.5f;
        }
        else
        {
            _camera.orthographicSize =
                (_field.bounds.size.z + 1) * _camera.pixelWidth / _camera.pixelHeight * 0.5f;
        }
    }

    public Bounds GetSpawnFieldBounds()
    {
        return _field.bounds;
    }

    private void SpawnCharacter()
    {
        CharacterController character = Instantiate(prefab, 
            new Vector3((_field.bounds.min.x + _field.bounds.max.x) / 2, 2f, _field.bounds.min.z + 0.5f), 
            Quaternion.identity);
        character.Init(_gameModel);
        _character = character;
        _character.DieCharacterEvent += ShowGameOverPanel;
    }

    private void IncreaseCountOfCoin()
    {
        _countOfCoin += 10;
        _countOfCoinTxt.text = _countOfCoin.ToString();
    }

    private void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
        _gameModel.EndModel();
    }
        
}
