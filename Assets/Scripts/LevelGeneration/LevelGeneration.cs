using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private LevelUnitsContainer levelList;
    [SerializeField] private GameObject plr;
    [SerializeField] private int unitCount;
    [SerializeField] private NavMeshSurface navMeshSurf;
    bool first = true;
    // Start is called before the first frame update
    void Awake()
    {
        int rand = 0, enemyRand = 0;
        LevelUnit newUnit = null;
        Transform lvlEnd = null;
        for (int i = 0; i<levelList.unitGenerationSize; i++)
        {
            rand = Random.Range(0, levelList.levelUnits.Count);
            if (first)
            {
                first = false;
                newUnit = GameObject.Instantiate(levelList.levelUnits[rand]);
                newUnit.startObj.SetActive(true);
                GameObject.Instantiate(plr, newUnit.playerSpawn.position, newUnit.playerSpawn.rotation);
            }
            else
            {
                newUnit = GameObject.Instantiate(levelList.levelUnits[rand], lvlEnd.position, lvlEnd.rotation);
            }

            for(int j = 0; j< levelList.levelUnits[rand].enemySpawnPoints.Count; j++)
            {
                enemyRand = Random.Range(0, levelList.levelUnits[rand].Enemies.Count);
                Instantiate(levelList.levelUnits[rand].Enemies[enemyRand], newUnit.enemySpawnPoints[j].position, newUnit.enemySpawnPoints[j].rotation);
            }
            lvlEnd = newUnit.levelEnd;
        }
        newUnit.endObj.SetActive(true);
    }

    private void Start()
    {
        navMeshSurf.BuildNavMesh();
    }


}
