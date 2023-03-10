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
    public Action<int> EnemyAttackEvent;
    public Action<List<EnemyController>, int> StartAttackUnitEvent;
    public Action SpawnCharacterEvent;
    public Action DieUnitEvent;
    public Action AllEnemiesKilled;
    
    private bool _onSimulation;
    private bool _allEnemiesIsKilled;
    private int _countOfEnemiesGround;
    private int _countOfEnemiesFlying;
    private ObjectsPool _objectsPool;
    private List<EnemyController> _enemies = new List<EnemyController>();

    public void Init(Joystick joystick, ObjectsPool objectsPool)
    {
        TickTime = 10;
        _countOfEnemiesGround = 2;
        _countOfEnemiesFlying = 2;
        _objectsPool = objectsPool;
        _allEnemiesIsKilled = false;

        Joystick = joystick;
    }

    public void SetSpawnFieldBorders(Bounds fieldBounds)
    {
        float minPointZ = fieldBounds.size.z / 3 + fieldBounds.min.z;
        SpawnEnemies(fieldBounds.min.x, fieldBounds.max.x, minPointZ, fieldBounds.max.z);
    }

    private void SpawnEnemies(float minX, float maxX, float minZ, float maxZ)
    {
        for (int i = 0; i < _countOfEnemiesGround; i++)
        {
            EnemyController enemyGround = _objectsPool.GetPooledObject(EnemyType.Ground);
            enemyGround.transform.position = new Vector3(Random.Range(maxX, minX), 0.25f, Random.Range(maxZ, minZ));
            enemyGround.EnemyMoveable.SpawnPoint = enemyGround.transform.position;
            enemyGround.Init(this);
            enemyGround.DieUnitEvent += ReturnToPoolUnit;
            _enemies.Add(enemyGround);
        }
        for (int i = 0; i < _countOfEnemiesFlying; i++)
        {
            EnemyController enemyFlying = _objectsPool.GetPooledObject(EnemyType.Flying);
            enemyFlying.transform.position = new Vector3(Random.Range(maxX, minX), 0.25f, Random.Range(maxZ, minZ));
            enemyFlying.EnemyMoveable.SpawnPoint = enemyFlying.transform.position;
            enemyFlying.Init(this);
            enemyFlying.DieUnitEvent += ReturnToPoolUnit;
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
            if (_enemies.Count == 0 && _allEnemiesIsKilled == false)
            {
                _allEnemiesIsKilled = true;
                AllEnemiesKilled?.Invoke();
            }
            if (Joystick.Horizontal != 0)
            {
                CharacterMoveEvent?.Invoke(Joystick.Direction);
            }
            else
            {
                StartAttackUnitEvent?.Invoke(_enemies, msec);
            }
            
            EnemyMoveEvent?.Invoke(msec);
            EnemyAttackEvent?.Invoke(msec);
            
            await Task.Delay(msec);
        }
            
        EndModel();
    }

    private void ReturnToPoolUnit(EnemyController unit)
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i] == unit)
            {
                _enemies[i].StopMoving();
                _objectsPool.TurnOfObject(_enemies[i]);
                _enemies.Remove(_enemies[i]);
            }
        }
        DieUnitEvent?.Invoke();
    }
    
    public void EndModel()
    {
        _onSimulation = false;
    }
}
