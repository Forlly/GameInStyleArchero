using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    private List<EnemyController> poolObjects = new List<EnemyController>();
    [SerializeField] private int amountPool = 32;
    [SerializeField] private EnemyController _enemyGroundPrefab;
    [SerializeField] private EnemyController _enemyFlyingPrefab;
    
    private bool isFull = false;

    public void Init()
    {
        for (int i = 0; i < amountPool/2; i++)
        {
            EnemyController tmpObj = Instantiate(_enemyGroundPrefab);
            tmpObj.gameObject.SetActive(false);
            poolObjects.Add(tmpObj);
            
            tmpObj = Instantiate(_enemyFlyingPrefab);
            tmpObj.gameObject.SetActive(false);
            poolObjects.Add(tmpObj);
        }
    }


    public EnemyController GetPooledObject(EnemyType type)
    {
        for (int i = 0; i < amountPool; i++)
        {
            if (!poolObjects[i].gameObject.activeInHierarchy && poolObjects[i].EnemyType == type)
            {
                poolObjects[i].gameObject.SetActive(true);
                return poolObjects[i];
            }

            isFull = true;
        }

        return isFull ? CreateNewObject(type) : null;
    }
    
    public  void TurnOfObject( EnemyController enemy)
    {
        for (int i = 0; i < amountPool; i++)
        {
            if (enemy == poolObjects[i])
            {
                poolObjects[i].gameObject.SetActive(false);
            }
            
        }
    }

    private EnemyController CreateNewObject(EnemyType type)
    {
        EnemyController tmpObj;
        tmpObj = Instantiate(type == EnemyType.Flying ? _enemyFlyingPrefab : _enemyGroundPrefab);

        tmpObj.gameObject.SetActive(true);
        poolObjects.Add(tmpObj);
        return tmpObj;
    }
}
