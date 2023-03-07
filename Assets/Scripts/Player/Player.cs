using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float maxHealth = 100;
    [SerializeField] private float currHealth;

    [SerializeField] private float modDamage = 1;
    [SerializeField] private float modHealth = 1;
    [SerializeField] private float modSpeed = 1;

    public InterfaceManager interfaceManager;

    public TMP_Text HP;
    public float maxHP { get { return maxHealth; } set { maxHealth = value; } }
    public float currHP { get { return currHealth; } set { currHealth = value; } }

    private void Start()
    {
        currHealth = maxHealth * modHealth;
    }

    private void FixedUpdate()
    {
        HP.text = currHealth.ToString();
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
