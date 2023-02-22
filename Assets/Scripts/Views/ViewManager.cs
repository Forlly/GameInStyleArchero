using UnityEngine;
using UnityEngine.AI;

public class ViewManager : MonoBehaviour
{
    public Transform SpawnPositionCharacter;
    
    [SerializeField] private Collider _field;
    [SerializeField] private CharacterController _characterPrefab;
    private GameModel _gameModel;


    public void Init(GameModel gameModel)
    {
        _gameModel = gameModel;
        _gameModel.SpawnCharacterEvent += SpawnCharacter;
        Debug.Log("ViewManager starting");

    }

    public Bounds GetSpawnFieldBounds()
    {
        return _field.bounds;
    }

    private void SpawnCharacter()
    {
        CharacterController character = Instantiate(_characterPrefab, 
            new Vector3((_field.bounds.min.x + _field.bounds.max.x) / 2, 2f, _field.bounds.min.z + 0.5f), 
            Quaternion.identity);
        character.Init(_gameModel);
    }
}
