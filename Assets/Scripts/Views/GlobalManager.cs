using UnityEngine;

public class GlobalManager : MonoBehaviour  
{
    [SerializeField] private ViewManager _viewManager;
    [SerializeField] private ObjectsPool _objectsPool;
    [SerializeField] private Joystick _joystick;
    private GameModel _gameModel;
    
    public Transform SpawnPositionCharacter;

    private void Awake()
    {
        _gameModel = new GameModel();
        _viewManager.Init(_gameModel);
        _gameModel.Init(_joystick, _objectsPool);

        Debug.Log(_viewManager.GetSpawnFieldBounds());
        _gameModel.SetSpawnFieldBorders(_viewManager.GetSpawnFieldBounds());
        _gameModel.StartSimulation();
    }

    private void OnDisable()
    {
        _gameModel.EndModel();
    }
}
