using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{// ��� ������� ������ ������ ID � � UAS ��������� �����
    public int actSkillID;
    private Player plr;
    private Inventory inv;
    private float cooldownTime;
    bool isCooldown;

    private void Start()
    {
        inv = GetComponent<Inventory>();
        plr = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && !isCooldown)
        {
            UseActiveSkill(inv.actSkill);
        }
    }


    public void UseActiveSkill(int asID)
    {
        switch (actSkillID)
        {
            case 0:
                SelfHeal(50, 10);
                Debug.Log("Healed myself");
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
        isCooldown= true;
        Invoke("ResetCooldown", cooldownTime);
    }
    private void ResetCooldown()
    {
        isCooldown= false;
        Debug.Log("Cooldown reseted");
    }
}
