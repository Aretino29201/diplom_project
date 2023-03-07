using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Player player;
    private GunSystem gunSys;

    public WeaponData currWeapon;
    public List<WeaponData> myWeapons;
    public int pasSkill, actSkill, ult;

    private void Start()
    {
        player= this.GetComponent<Player>();
        gunSys = this.GetComponent<GunSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunSys.UpdateGun(myWeapons[0]);
            currWeapon = myWeapons[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunSys.UpdateGun(myWeapons[1]);
            currWeapon = myWeapons[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gunSys.UpdateGun(myWeapons[2]);
            currWeapon = myWeapons[2];
        }
    }
    public void PickupGun(WeaponData newGun)
    {
        myWeapons.Add(newGun);
        currWeapon = newGun;
        gunSys.UpdateGun(currWeapon);
    }

    public void PickupPasSkill(int newPasSkill)
    {
        pasSkill= newPasSkill;    
    }

    public void PickupActSkill(int newActSkill)
    {
        actSkill= newActSkill;
    }

    public void PickupUlt(int newUlt)
    {
        ult= newUlt;
    }


}
