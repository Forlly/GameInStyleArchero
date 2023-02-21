using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public Transform SpawnPositionCharacter;
    
    [SerializeField] private GameObject _spawnCharacterUnitView;


    public void Init(GameModel gameModel)
    {

        Debug.Log("ViewManager starting");

    }
}
