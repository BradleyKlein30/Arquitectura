using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    #region Variables
    public static BulletPool current;
    public GameObject pooledBullet;
    public int pooledAmount;
    public bool willGrow;

    private List<GameObject> pooledBullets;
    #endregion

    //singleton
    private void Awake()
    {
        current = current ? current : this;
    }

    void Start()
    {
        pooledBullets = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledBullet);
            obj.SetActive(false);
            pooledBullets.Add(obj);
        }
    }

    public GameObject GetPooledBullet()
    {
        for (int i = 0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = Instantiate(pooledBullet);
            pooledBullets.Add(obj);
            return obj;
        }


        return null;
    }
}
