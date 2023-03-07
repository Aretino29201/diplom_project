using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject shotgunAmmoPack, smgAmmoPack, medpack;
    private int spawnID;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SpawnAmmo(Vector3 pos,Quaternion rot)
    {
        spawnID  = Random.Range(0, 4);

        switch (spawnID)
        {
            case 0:
                Instantiate(shotgunAmmoPack, pos, rot);
                Debug.Log("Shotgun ammo spawned");
                break;
            case 1:
                Instantiate(smgAmmoPack, pos, rot);
                Debug.Log("SMG ammo spawned");
                break;
            case 2:
                Instantiate(medpack, pos, rot);
                Debug.Log("Medpack spawned");
                break;
            default:
                Debug.Log("No drop for you");
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
