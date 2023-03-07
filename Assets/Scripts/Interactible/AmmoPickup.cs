using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int pickedAmmo = 12;
    [SerializeField] private int ammoType;
    private List<WeaponData> weaponData;
    private GunSystem gunSys;


    private void Start()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        weaponData = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().myWeapons;
        gunSys = GameObject.FindGameObjectWithTag("Player").GetComponent<GunSystem>();

        if (other.CompareTag("Player"))
        { for(int i = 0; i< weaponData.Count; i++)
            if (ammoType == weaponData[i].ammoType && weaponData[i].bulletsLeft != weaponData[i].magazineSize)
            {
                if (weaponData[i].bulletsLeft + pickedAmmo <= weaponData[i].magazineSize)
                {
                    weaponData[i].bulletsLeft += pickedAmmo;
                }
                else
                {
                    weaponData[i].bulletsLeft = weaponData[i].magazineSize;
                }
                Destroy(gameObject);
            }
        }

        
    }
}
