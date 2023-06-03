using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{// при подборе скиров менять ID и в UAS добавлять кейсы
    //public int actSkillID;
    private Player plr;
    private Inventory inv;
    public float cooldownTime;
    public bool isCooldown;
    public PlayerProjectile podstvolPref;

    private void Start()
    {
        inv = GetComponent<Inventory>();
        plr = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && !isCooldown && inv.currActSkill != 0)
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
                SelfHeal(plr.maxHP/2, 10);
                break;
            case 2:
                UnbreakableArmor(5, 15);
                break;
            case 3:
                SlowTime(5, 12);
                break;
            case 4:
                Podstvol(8);
                break;
            case 5:
                Invisibility(5, 20);
                break;
            default:
                Debug.Log("No active skills");
                break;
        }
    }
    private void SelfHeal(float addHP, float cdt)
    {
        cooldownTime = cdt;

        plr.Heal(addHP);

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

    private void SlowTime(float duration, float cdt)
    {
        cooldownTime = cdt;
        StartCoroutine(SlowT(duration));

    }

    private IEnumerator SlowT(float d)
    {
        //todo
        Time.timeScale = 0.5f;
        
        yield return new WaitForSeconds(d);
        Time.timeScale = 1f;

    }

    private void Podstvol(float cdt)
    {
        cooldownTime = cdt;
        Rigidbody rb = Instantiate(podstvolPref, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(this.GetComponent<GunSystem>().fpsCam.transform.forward * podstvolPref.pSpeed, ForceMode.Impulse);
    }




    private void Invisibility(float duration, float cdt)
    {
        cooldownTime = cdt;
        StartCoroutine(Invis(duration));

    }
    private IEnumerator Invis(float d)
    {
        //todo
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<SimpleEnemyAI>().isPlayerInvis = true;
        }

        yield return new WaitForSeconds(d);
        foreach (GameObject enemy in enemies)
        {
            if(enemy!= null)
            enemy.GetComponent<SimpleEnemyAI>().isPlayerInvis = false;
        }

    }
}
