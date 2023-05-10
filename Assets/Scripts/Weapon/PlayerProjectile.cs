using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float pSpeed;
    public PlayerExplosion plrExplos;
    bool enemyInExplos;
    public LayerMask playerLayer;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("GoesThru")) ;
        else
        {
            Instantiate(plrExplos,new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);   
        }
    }

        private void OnCollisionEnter(Collision collision)
    {
        
    }
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        collision.gameObject.GetComponent<EnemyController>().TakeDamage(1.5f*damage);
    //        ProjectileExplosion();
    //    }
    //    else
    //    {
    //        ProjectileExplosion();
    //    }
    //}
    //public void ProjectileExplosion()
    //{
    //    // партиклы взрыва
    //    enemyInExplos = Physics.CheckSphere(transform.position, explosRange, enemyLayer);        
    //    if (enemyInExplos) 
    //    {            
    //    }
    //    Debug.Log("Boom!!!!!!!!!!!!!!!!");
    
}
