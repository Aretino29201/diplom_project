using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject shotgunAmmoPack, smgAmmoPack, medpack;
    public List<GameObject> dropItems;
    private int spawnID;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SpawnAmmo(Vector3 pos,Quaternion rot)
    {
        spawnID  = Random.Range(0, 5);

        switch (spawnID)
        {
            case 0:
                Instantiate(dropItems[0], pos, rot);
                break;
            case 1:
                Instantiate(dropItems[1], pos, rot);
                break;
            case 2:
                Instantiate(dropItems[2], pos, rot);
                break;
            case 3:
                Instantiate(dropItems[3], pos, rot);
                break;
            case 4:
                Instantiate(dropItems[4], pos, rot);
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
