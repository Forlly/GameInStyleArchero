using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameModel
{
    public int TickTime;
    public Joystick Joystick;
    
    public Action<Vector3> CharacterMoveEvent;
    public Action<int> EnemyMoveEvent;
    public Action StartAttackEvent;
    public Action SpawnCharacterEvent;
    
    private bool _onSimulation;
    private int _countOfEnemiesGround;
    private int _countOfEnemiesFlying;
    private ObjectsPool _objectsPool;
    private List<EnemyController> _enemies = new List<EnemyController>();

    public void Init(Joystick joystick, ObjectsPool objectsPool)
    {
        TickTime = 10;
        _countOfEnemiesGround = 3;
        _countOfEnemiesFlying = 1;
        _objectsPool = objectsPool;

        Joystick = joystick;
        Debug.Log("GameModel starting" + Joystick);
    }

    public void SetSpawnFieldBorders(Bounds fieldBounds)
    {
        float minPointZ = fieldBounds.size.z / 3 + fieldBounds.min.z;
        SpawnEnemies(fieldBounds.min.x, fieldBounds.max.x, minPointZ, fieldBounds.max.z);
    }

    private void SpawnEnemies(float minX, float maxX, float minZ, float maxZ)
    {
        Debug.Log("SPAWN");
        for (int i = 0; i < _countOfEnemiesGround; i++)
        {
            Debug.Log(_countOfEnemiesGround);
            EnemyController enemyGround = _objectsPool.GetPooledObject(EnemyType.Ground);
            enemyGround.transform.position = new Vector3(Random.Range(maxX, minX), 0.25f, Random.Range(maxZ, minZ));
            enemyGround.EnemyMoveable.SpawnPoint = enemyGround.transform.position;
            enemyGround.Init(this);
            _enemies.Add(enemyGround);
        }
        for (int i = 0; i < _countOfEnemiesFlying; i++)
        {
            Debug.Log(_countOfEnemiesFlying);
            EnemyController enemyFlying = _objectsPool.GetPooledObject(EnemyType.Flying);
            enemyFlying.transform.position = new Vector3(Random.Range(maxX, minX), 0.25f, Random.Range(maxZ, minZ));
            enemyFlying.EnemyMoveable.SpawnPoint = enemyFlying.transform.position;
            enemyFlying.Init(this);
            _enemies.Add(enemyFlying);
        }
        
        SpawnCharacterEvent?.Invoke();
    }
    
    public async void StartSimulation()
    {
        await Tick(TickTime);
    }
    public async Task Tick(int msec)
    {
        _onSimulation = true;
       
        while (_onSimulation)
        {
            if (Joystick.Horizontal != 0)
            {
                CharacterMoveEvent?.Invoke(Joystick.Direction);
            }
            else
            {
                StartAttackEvent?.Invoke();
            }
            
            EnemyMoveEvent?.Invoke(msec);
            
            await Task.Delay(msec);
        }
            
        EndModel();
    }

    public void EndModel()
    {
        _onSimulation = false;
    }
}
