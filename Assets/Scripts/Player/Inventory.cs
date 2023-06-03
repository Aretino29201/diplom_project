using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Player player;
    private GunSystem gunSys;

    public WeaponData currWeapon;
    public List<WeaponData> myWeapons;
    public int currPasSkill, currActSkill, currUlt;
    public ActiveSkill actSkill;
    public PassiveSkill passSkill;
    [SerializeField] WeaponData startWeapon;
    int lastWeaponID = 0;

    private void Start()
    {
        player= this.GetComponent<Player>();
        gunSys = this.GetComponent<GunSystem>();
        passSkill = this.GetComponent<PassiveSkill>();
        for (int i = 0; i < 10; i++) myWeapons.Add(null);
        PickupGun(startWeapon);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateGunInHands(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateGunInHands(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateGunInHands(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateGunInHands(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UpdateGunInHands(4);
        }
    }

    //    if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
    //    {
    //        try
    //        {
    //            ScrollUpGun();
    //        }
    //        catch
    //        {
    //            while (myWeapons[lastWeaponID] == null)
    //            {
    //                lastWeaponID++;
    //            }
                
    //        }
    //    }
    //    if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
    //    {
    //        try
    //        {
    //            ScrollDownGun();
    //        }
    //        catch
    //        {
    //            while (myWeapons[lastWeaponID] == null)
    //            {
    //                lastWeaponID--;
    //            }
    //        }
    //    }
    //}

    //private void ScrollUpGun()
    //{
        
    //        lastWeaponID++;

    //    if (lastWeaponID >= myWeapons.Count)
    //    {
    //        lastWeaponID = 0;
    //    }

    //    UpdateGunInHands(lastWeaponID);
    //}

    //private void ScrollDownGun()
    //{
    //        lastWeaponID--;

    //    if (lastWeaponID < 0)
    //    {
    //        lastWeaponID = myWeapons.Count - 1;
    //    }

    //    UpdateGunInHands(lastWeaponID);
    //}


    public void PickupGun(WeaponData newGun)
    {
        myWeapons[newGun.ammoType] = newGun;
        currWeapon = newGun;
        if (currWeapon.bulletsLeft + currWeapon.magazineSize/2 <= currWeapon.magazineSize)
        {
            currWeapon.bulletsLeft += currWeapon.magazineSize/2;
        }
        else
        {
            currWeapon.bulletsLeft = currWeapon.magazineSize;
        }
        gunSys.UpdateGun(currWeapon);
    }

    public void PickupPasSkill(int newPasSkill)
    {
        currPasSkill= newPasSkill;
        passSkill.UsePassiveSkill(currPasSkill);
    }

    public void PickupActSkill(int newActSkill)
    {
        currActSkill= newActSkill;
    }

    public void PickupUlt(int newUlt)
    {
        currUlt= newUlt;
    }

    public void UpdateGunInHands(int key)
    {
        if (myWeapons[key] != null)
        {
            gunSys.UpdateGun(myWeapons[key]);
            currWeapon = myWeapons[key];
        }
        
    }


}
