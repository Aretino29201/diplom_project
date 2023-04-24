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
    int lastWeaponID;

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

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            try
            {
                UpdateGunInHands(lastWeaponID + 1);
            }
            catch
            {
                UpdateGunInHands(0);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            try
            {
                UpdateGunInHands(lastWeaponID - 1);
            }
            catch
            {
                UpdateGunInHands(2);//ÏÎÌÅÍßÒÜ ÍÀ ÌÅÑÈÌÀËÜÍÎÅ ÇÍÀ×ÅÍÈÅ ÏÓØÅÊ
            }
        }
    }
    public void PickupGun(WeaponData newGun)
    {
        myWeapons[newGun.ammoType] = newGun;
        currWeapon = newGun;
        if (currWeapon.bulletsLeft + currWeapon.magazineSize/4 <= currWeapon.magazineSize)
        {
            currWeapon.bulletsLeft += currWeapon.magazineSize/4;
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
            lastWeaponID = key;
            gunSys.UpdateGun(myWeapons[key]);
            currWeapon = myWeapons[key];
        }
        
    }


}
