using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float healthPoints;
    Vector3 ammoSpawnPos;
    Quaternion ammoSpawnRot;
    public EnenmyCount enmCount;
   
    public EnemyDrop enemyDrop;

    private void Start()
    {
        healthPoints = 100;
        enemyDrop = GameObject.Find("Manager").GetComponent<EnemyDrop>();
    }
    public void TakeDamage(float dmg)
    {
        healthPoints -= dmg;
        if (healthPoints <= 0)
        {
            Death();
        }
    }
     public void Death()
    {
        ammoSpawnPos = this.gameObject.transform.position;
        ammoSpawnRot = this.gameObject.transform.rotation;
        enemyDrop.SpawnAmmo(ammoSpawnPos, ammoSpawnRot);

        Destroy(this.gameObject);
    }

    // если противник милишный, то ударить игрока
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        DealDamage(other.GetComponent<Player>(), 10);
    //    }
    //}

    public void DealDamage(Player plr, float dmg)
    {
        plr.currHP = plr.currHP - dmg;
        Debug.Log(dmg + " damage dealt");
    }

}
