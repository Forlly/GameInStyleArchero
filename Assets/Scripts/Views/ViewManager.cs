using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    public Transform SpawnPositionCharacter;

    [SerializeField] private Text _countOfCoinTxt;
    [SerializeField] private Collider _field;
    [SerializeField] private CharacterController _characterPrefab;
    private CharacterController _character;
    private int _countOfCoin;
    
    private GameModel _gameModel;


    public void Init(GameModel gameModel)
    {
        _gameModel = gameModel;
        _gameModel.SpawnCharacterEvent += SpawnCharacter;
        _gameModel.DieUnitEvent += IncreaseCountOfCoin;
        _countOfCoin = 0;
       
        SetOrthographicSizeCamera();
        
        Debug.Log("ViewManager starting");
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
        CharacterController character = Instantiate(_characterPrefab, 
            new Vector3((_field.bounds.min.x + _field.bounds.max.x) / 2, 1f, _field.bounds.min.z + 0.5f), 
            Quaternion.identity);
        character.Init(_gameModel);
        _character = character;
    }

    private void IncreaseCountOfCoin()
    {
        _countOfCoin += 10;
        _countOfCoinTxt.text = _countOfCoin.ToString();
    }
        
}
