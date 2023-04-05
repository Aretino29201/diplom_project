using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnit : MonoBehaviour
{
    public Transform levelEnd, playerSpawn;
    public List<GameObject> Enemies;
    public List<Transform> enemySpawnPoints;
    public GameObject startObj, endObj;
}
