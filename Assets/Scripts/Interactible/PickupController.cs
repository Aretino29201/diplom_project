using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    
    public Inventory inventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // press e to pickup
            Debug.Log("Press E to pickup");
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("PickedUp");
                Pickup();
            }
        }
    }

    public void Pickup()
    {
        // code for actual pickup

        Destroy(gameObject);
    }
}
