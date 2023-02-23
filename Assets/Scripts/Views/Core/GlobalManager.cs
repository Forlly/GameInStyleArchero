using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour  
{
    [SerializeField] private ViewManager _viewManager;
    [SerializeField] private TransitionBetweenLevels _transitionBetweenLevels;
    [SerializeField] private StartingLoadingPanel _startingLoadingPanel;
    [SerializeField] private List<GameObject> _levelFields;
    [SerializeField] private GameObject _currentLevel;
    [SerializeField] private ObjectsPool _objectsPool;
    [SerializeField] private Joystick _joystick;
    private GameModel _gameModel;

    private void Awake()
    {
        _gameModel = new GameModel();
        _viewManager.Init(_gameModel);
        _gameModel.Init(_joystick, _objectsPool);
        _objectsPool.Init();
        
        _gameModel.SetSpawnFieldBorders(_viewManager.GetSpawnFieldBounds());
        StartCoroutine(_startingLoadingPanel.StartCountdown());
        
        _startingLoadingPanel.CountdownIsOverEvent += _gameModel.StartSimulation;
    }
    

        private void OnDisable()
    {
        _gameModel.EndModel();
    }
}
