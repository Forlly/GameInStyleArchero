using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    public static BulletsPool Instance;
    private List<Bullet> poolObjects = new List<Bullet>();
    [SerializeField] private int amountPool = 128;
    [SerializeField] private Bullet bullet;
    
    private bool isFull = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        for (int i = 0; i < amountPool; i++)
        {
            Bullet tmpObj = Instantiate(bullet);
            tmpObj.gameObject.SetActive(false);
            tmpObj.InPull = true;
            tmpObj.transform.position = new Vector3(-1000, -1000, -1000);
            poolObjects.Add(tmpObj);
        }
    }


    public Bullet GetPooledObject()
    {
        for (int i = 0; i < amountPool; i++)
        {
            if (!poolObjects[i].gameObject.activeInHierarchy)
            {
                poolObjects[i].InPull = false;
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
    
    public  void TurnOfObject(Bullet _platform)
    {
        for (int i = 0; i < amountPool; i++)
        {
            if (_platform == poolObjects[i])
            {
                poolObjects[i].InPull = true;
                poolObjects[i].gameObject.SetActive(false);
                poolObjects[i].transform.position = new Vector3(-1000, -1000, -1000);
            }
            
        }
    }

    private Bullet CreateNewObject()
    {
        Bullet tmpObj = Instantiate(bullet);
        tmpObj.gameObject.SetActive(true);
        tmpObj.InPull = false;
        tmpObj.transform.position = new Vector3(-1000, -1000, -1000);
        poolObjects.Add(tmpObj);
        return tmpObj;
    }
}
