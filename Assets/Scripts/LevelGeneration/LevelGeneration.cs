using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI; //"Editor" not "Engine"

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private LevelUnitsContainer levelList;
    [SerializeField] private GameObject plr;
    [SerializeField] private int unitCount;
    bool first = true;
    // Start is called before the first frame update
    void Awake()
    {
        int rand = 0;
        LevelUnit newUnit = null, prevUnit = null;
        Transform lvlEnd = null;
        for (int i = 0; i<unitCount; i++)
        {
            rand = Random.Range(0, levelList.levelUnits.Count);
            if (first)
            {
                first = false;
                newUnit = GameObject.Instantiate(levelList.levelUnits[rand]);
                GameObject.Instantiate(plr, newUnit.playerSpawn.position, newUnit.playerSpawn.rotation);
            }
            else
            {
                newUnit = GameObject.Instantiate(levelList.levelUnits[rand], lvlEnd.position, lvlEnd.rotation);
            }
            lvlEnd = newUnit.levelEnd;
        }
    }

    private void Start()
    {
        

        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMesh();
    }


}
