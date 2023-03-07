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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            plr = collision.gameObject.GetComponent<Player>();
            plr.currHP-= damage;
            Debug.Log("Ouch!");
            Destroy(gameObject);
        }

    }
}
