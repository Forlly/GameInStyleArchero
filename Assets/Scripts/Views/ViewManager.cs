using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public Transform SpawnPositionCharacter;
    
    [SerializeField] private CharacterController _character;


    public void Init(GameModel gameModel)
    {
        _character.Init(gameModel);
        Debug.Log("ViewManager starting");

    }
}
