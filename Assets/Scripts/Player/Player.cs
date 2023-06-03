using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float maxHealth = 100;
    [SerializeField] private float currHealth;
    public bool canRes;

    public AudioClip takeDamageAudio, healingAudio,  jumpAudio;
    public bool isWalkingAudio;
    public InterfaceManager interfaceManager;

    public float maxHP { get { return maxHealth; } set { maxHealth = value; } }
    public float currHP { get { return currHealth; } set { currHealth = value; } }

    private void Awake()
    {
        currHealth = maxHealth;
    }

    private void Start()
    {
        interfaceManager = GameObject.Find("Manager").GetComponent<InterfaceManager>();
    }

    private void FixedUpdate()
    {
        
        if(currHealth <= 0)
        {
            currHealth = 0;
            StartCoroutine(Death());
        }
    }
    IEnumerator Death()
    {
        Time.timeScale = 0;
        if (GetComponent<Inventory>().currUlt == 4 && GetComponent<UltSkill>().currUltCharge == 100) // 4 - воскрешение
        {
            canRes = true;
            interfaceManager.resurrectScreen.SetActive(true);
            yield return new WaitForSecondsRealtime(3);// время на воскрешение
            interfaceManager.resurrectScreen.SetActive(false);
            if (currHealth <= 0)
            {
                canRes = false;
                interfaceManager.DeathOn();
            }
        }
        else
        {
            interfaceManager.DeathOn();
        }
        
    }

    public void Heal(float heal)
    {
        GetComponent<AudioSource>().PlayOneShot(healingAudio);
        if (currHP < maxHP)
        {
            currHP += heal;
        }
        if (currHP > maxHP)
        {
            currHP = maxHP;
        }
    }

    public void TakeDamage(float damage)
    {
        GetComponent<AudioSource>().PlayOneShot(takeDamageAudio);
        currHP -= damage;
        
        if (currHP < 0)
        {
            currHP = 0;
        }
    }

}
