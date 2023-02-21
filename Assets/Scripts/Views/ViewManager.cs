using UnityEngine;
using UnityEngine.AI;

public class ViewManager : MonoBehaviour
{
    public Transform SpawnPositionCharacter;
    
    [SerializeField] private Collider _field;
    [SerializeField] private CharacterController _character;
    [SerializeField] private EnemyController _enemy;


    public void Init(GameModel gameModel)
    {
        _character.Init(gameModel);
        _enemy.Init(gameModel);
        Debug.Log("ViewManager starting");

    }

    public Bounds GetSpawnFieldBounds()
    {
        return _field.bounds;
    }
}
