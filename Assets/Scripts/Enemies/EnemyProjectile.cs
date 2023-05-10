using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Player plr;
    public float damage = 10;
    void Start()
    {
        Destroy(this.gameObject, 3);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            plr = other.gameObject.GetComponent<Player>();
            plr.currHP -= damage;
            Debug.Log("Ouch!");
            Destroy(gameObject);
        }
        else if (!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    
}
