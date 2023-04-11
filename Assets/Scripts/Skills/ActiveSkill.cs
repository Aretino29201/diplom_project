using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{// при подборе скиров менять ID и в UAS добавлять кейсы
    //public int actSkillID;
    private Player plr;
    private Inventory inv;
    private float cooldownTime;
    public float cooldownResetingTime;
    public bool isCooldown;

    private void Start()
    {
        inv = GetComponent<Inventory>();
        plr = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && !isCooldown)
        {
            UseActiveSkill(inv.currActSkill);
            isCooldown = true;
            Invoke("ResetCooldown", cooldownTime);

        }
    }


    public void UseActiveSkill(int asID)
    {
        switch (asID)
        {
            case 1:
                SelfHeal(50, 10);
                Debug.Log("Healed myself");
                break;
            case 2:
                UnbreakableArmor(3, 10);
                Debug.Log("ARMOR");
                break;
            default:
                Debug.Log("No active skills");
                break;
        }
    }
    private void SelfHeal(float addHP, float cdt)
    {
        cooldownTime = cdt;

        if (plr.currHP < plr.maxHP) 
        {
            plr.currHP += addHP;
            if (plr.currHP + addHP > plr.maxHP)
            {
                plr.currHP = plr.maxHP;
            }
        }

    }
    private void ResetCooldown()
    {
        isCooldown= false;
        Debug.Log("Cooldown reseted");
    }

    private void UnbreakableArmor(float duration, float cdt)
    {
        cooldownTime = cdt;
        StartCoroutine(Shield(duration));
        
    }

    private IEnumerator Shield(float d)
    {
        //todo
        float tempHP = plr.currHP;
        plr.currHP = 999999999999;
         yield return new WaitForSeconds(d);
        plr.currHP = tempHP;
        //StopCoroutine(Shield(d));

    }
}
