using UnityEngine;

public class GlobalManager : MonoBehaviour  
{
    [SerializeField] private ViewManager _viewManager;
    [SerializeField] private Joystick _joystick;
    private GameModel _gameModel;
    
    public Transform SpawnPositionCharacter;

    private void Awake()
    {
        _gameModel = new GameModel();
        _gameModel.Init(_joystick);
        _viewManager.Init(_gameModel);
        
        _gameModel.StartSimulation();
    }

    private void OnDisable()
    {
        _gameModel.EndModel();
    }
}
