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

    private void Start()
    {
        inv = GetComponent<Inventory>();
        plr = GetComponent<Player>();
        ctrl = GetComponent<ExampleCharacterController>();
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
                break;
            case 2:
                StartCoroutine(RocketBarrage(0.2f, 20));
                break;
            case 3:
                StartCoroutine(Berserker(5f));
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

    //rocket barrage!!!!!!!!




    //private void ResetCooldown()
    //{
    //    isCooldown = false;
    //    Debug.Log("Cooldown reseted");
    //}

    
        
    

    private IEnumerator RocketBarrage(float durat, int rAmount)
    {

        ctrl.MaxStableMoveSpeed /= 3f;
        ctrl.MaxAirMoveSpeed /= 3f;
        ctrl.JumpUpSpeed = 0;
        for (int i = 0; i <= rAmount; i++) {
            float x = Random.Range(-0.5f, 0.5f);
            float y = Random.Range(-0.5f, 0.5f);
            Rigidbody rb = Instantiate(rocketPref, new Vector3(transform.position.x + x, transform.position.y + 1.5f + y, transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(this.GetComponent<GunSystem>().fpsCam.transform.forward * rocketPref.pSpeed, ForceMode.Impulse);

            yield return new WaitForSeconds(durat); 
        }
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
}
