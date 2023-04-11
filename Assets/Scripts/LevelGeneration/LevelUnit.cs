using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnit : MonoBehaviour
{
    public Transform levelEnd, playerSpawn;
    public List<GameObject> Enemies;
    public List<Transform> enemySpawnPoints;
    public GameObject startObj, endObj, closePath;
    public List<GameObject> enemies;

    private void Start()
    {
    }

   public void OnEnemyDestroyed(GameObject enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0) { closePath.SetActive(false); }
    }
}
