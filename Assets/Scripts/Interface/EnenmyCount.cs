using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnenmyCount : MonoBehaviour
{
    private GameObject[] enemyObjects;
    public List<GameObject> enemyList;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemyList.Add(enemyObjects[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
            if (enemyList.Count <= 0)
            {
                Debug.Log("YOU WON");
            }
    }
}
