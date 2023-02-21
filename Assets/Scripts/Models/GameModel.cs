using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameModel
{
    public int TickTime;
    public Vector3 SpawnPositionCharacter;
    public Joystick Joystick;
    
    public Action<Vector3> CharacterMoveEvent;
    public Action<int> EnemyMoveEvent;
    public Action StartAttackEvent;
    
    private bool _onSimulation;
    
    public void Init(Joystick joystick)
    {
        TickTime = 20;

        Joystick = joystick;
        Debug.Log("GameModel starting" + Joystick);
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
