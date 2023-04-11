using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltSkill : MonoBehaviour
{
    private Player plr;
    private Inventory inv;
    public float ultCharge, currUltCharge;
    bool isCharging;

    private void Start()
    {
        inv = GetComponent<Inventory>();
        plr = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) && currUltCharge >= ultCharge && inv.currUlt != 0)
        {
            UseUlt(inv.currUlt);
            currUltCharge = 0;
        }
    }


    public void UseUlt(int usID)
    {
        switch (usID)
        {
            case 1:
                BigBoom(20);
                Debug.Log("Boom");
                break;
            case 2:

                Debug.Log("");
                break;
            default:
                Debug.Log("No ult");
                break;
        }
    }
    private void BigBoom(float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Destroy(collider.gameObject);
            }
        }

    }















    //private void ResetCooldown()
    //{
    //    isCooldown = false;
    //    Debug.Log("Cooldown reseted");
    //}

    private void UnbreakableArmor(float duration, float cdt)
    {
        ultCharge = cdt;
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
