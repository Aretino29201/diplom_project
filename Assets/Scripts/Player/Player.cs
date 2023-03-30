using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float maxHealth = 100;
    [SerializeField] private float currHealth;


    public InterfaceManager interfaceManager;

    public float maxHP { get { return maxHealth; } set { maxHealth = value; } }
    public float currHP { get { return currHealth; } set { currHealth = value; } }

    private void Awake()
    {
        currHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        
        if(currHealth <= 0)
        {
            currHealth = 0;
            Death();
        }
    }
    public void Death()
    {
        Time.timeScale = 0;
        interfaceManager.DeathOn();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

}
