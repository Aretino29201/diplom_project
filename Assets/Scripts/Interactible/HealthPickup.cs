using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int pickedHP = 25;
    [SerializeField] private int ammoType;
    private Player plr;
    float tmpHP;


    private void Start()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        plr = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (other.CompareTag("Player"))

            if (plr.currHP < plr.maxHP)
            {
                if (plr.currHP + pickedHP > plr.maxHP)
                {
                    plr.currHP = plr.maxHP;
                } else plr.currHP += pickedHP;

                Debug.Log("Added 25 HP");
                Destroy(gameObject);
            }
            else Debug.Log("HP is full");
        }

        
    }

