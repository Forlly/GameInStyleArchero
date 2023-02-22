using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    private List<EnemyController> poolObjects = new List<EnemyController>();
    [SerializeField] private int amountPool = 32;
    [SerializeField] private EnemyController _enemyPrefab;
    
    private bool isFull = false;

    public void Init()
    {
        for (int i = 0; i < amountPool; i++)
        {
            EnemyController tmpObj = Instantiate(_enemyPrefab);
            tmpObj.gameObject.SetActive(false);
            poolObjects.Add(tmpObj);
        }
    }


    public EnemyController GetPooledObject()
    {
        for (int i = 0; i < amountPool; i++)
        {
            if (!poolObjects[i].gameObject.activeInHierarchy)
            {
                poolObjects[i].gameObject.SetActive(true);
                return poolObjects[i];
            }

            isFull = true;
        }

        if (isFull)
        {
            return CreateNewObject();
        }
        return null;
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

    private EnemyController CreateNewObject()
    {
        EnemyController tmpObj = Instantiate(_enemyPrefab);
        tmpObj.gameObject.SetActive(true);
        poolObjects.Add(tmpObj);
        return tmpObj;
    }
}
