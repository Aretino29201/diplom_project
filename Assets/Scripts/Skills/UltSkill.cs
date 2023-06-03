using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltSkill : MonoBehaviour
{
    private Player plr;
    private Inventory inv;
    ExampleCharacterController ctrl;
    public float ultCharge, currUltCharge;
    bool isCharging;
    public PlayerProjectile rocketPref;
    public WeaponList weapons;
    InterfaceManager intManager;
    public LayerMask whatIsEnemy;

    private void Start()
    {
        inv = GetComponent<Inventory>();
        plr = GetComponent<Player>();
        ctrl = GetComponent<ExampleCharacterController>();
        intManager = GameObject.Find("Manager").GetComponent<InterfaceManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) && currUltCharge >= ultCharge && inv.currUlt != 0)
        {
            if (inv.currUlt != 4)
            {
                UseUlt(inv.currUlt);
                currUltCharge = 0;
            }
            else if(inv.currUlt == 4 && plr.currHP <=0 && plr.canRes)
            {
                UseUlt(inv.currUlt);
                currUltCharge = 0;
            }
        }
    }


    public void UseUlt(int usID)
    {
        switch (usID)
        {
            case 1:
                BigBoom(10);
                break;
            case 2:
                StartCoroutine(RocketBarrage(0.2f, 20));
                break;
            case 3:
                StartCoroutine(Berserker(7.5f));
                break;
            case 4:
                Resurrect();
                break;
            case 5:
               StartCoroutine(Vampire(0.2f,10,2));
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

    private IEnumerator RocketBarrage(float durat, int rAmount)
    {

        ctrl.MaxStableMoveSpeed /= 3f;
        ctrl.MaxAirMoveSpeed /= 3f;
        ctrl.JumpUpSpeed = 0;
        GetComponent<GunSystem>().renderPoint.gameObject.SetActive(false);
        for (int i = 0; i <= rAmount; i++) {
            float x = Random.Range(-0.5f, 0.5f);
            float y = Random.Range(-0.5f, 0.5f);
            Rigidbody rb = Instantiate(rocketPref, new Vector3(transform.position.x + x, transform.position.y + 1.5f + y, transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(this.GetComponent<GunSystem>().fpsCam.transform.forward * rocketPref.pSpeed, ForceMode.Impulse);

            yield return new WaitForSeconds(durat); 
        }
        GetComponent<GunSystem>().renderPoint.gameObject.SetActive(true);
        ctrl.MaxStableMoveSpeed *= 3f;
        ctrl.MaxAirMoveSpeed *= 3f;
        ctrl.JumpUpSpeed = 10;
    }

    private IEnumerator Berserker(float durat)
    {
        int[] tempAmmo = new int[weapons.weapons.Count];
        for (int i = 0; i<weapons.weapons.Count; i++)
        {
            if (weapons.weapons[i] != null)
            {
                tempAmmo[i] = weapons.weapons[i].bulletsLeft;

                weapons.weapons[i].bulletsLeft = 9999999;
                weapons.weapons[i].timeBetweenShooting /= 2;
            }
        }
        ctrl.MaxStableMoveSpeed *= 1.5f;
        ctrl.MaxAirMoveSpeed *= 1.5f;
        


            yield return new WaitForSeconds(durat);

        for (int i = 0; i < weapons.weapons.Count; i++)
        {
            if (weapons.weapons[i] != null)
            {
                weapons.weapons[i].bulletsLeft = tempAmmo[i];

                weapons.weapons[i].timeBetweenShooting *= 2;
            }
        }

        ctrl.MaxStableMoveSpeed /= 1.5f;
        ctrl.MaxAirMoveSpeed /= 1.5f;
    }

    private void Resurrect()
    {
        plr.currHP = plr.maxHP/2;
        Time.timeScale = 1;
        intManager.resurrectScreen.SetActive(false);
    }

    private IEnumerator Vampire(float durat, float radius, float damage)
    {
        Collider[] colliders;
        bool isEnemyInRange = Physics.CheckSphere(this.transform.position, radius, whatIsEnemy);
        while (isEnemyInRange)
        {
            colliders = Physics.OverlapSphere(this.transform.position, radius, whatIsEnemy);
            foreach(Collider collider in colliders)
            {
                collider.GetComponent<EnemyController>().TakeDamage(damage);
                plr.Heal(damage / 2);
            }
            isEnemyInRange = Physics.CheckSphere(this.transform.position, radius, whatIsEnemy);
            yield return new WaitForSeconds(durat);
        }
        
    }
}
