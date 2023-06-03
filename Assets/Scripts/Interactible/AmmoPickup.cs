using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int pickedAmmo;
    [SerializeField] private int ammoType;
    private List<WeaponData> weaponData;
    private GunSystem gunSys;
    public GameObject render;


    private void Start()
    {
        weaponData = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().myWeapons;
        gunSys = GameObject.FindGameObjectWithTag("Player").GetComponent<GunSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.CompareTag("Player"))
        { for(int i = 0; i< weaponData.Count; i++)
            if (weaponData[i] != null && ammoType == weaponData[i].ammoType && weaponData[i].bulletsLeft != weaponData[i].magazineSize)
            {
                if (weaponData[i].bulletsLeft + pickedAmmo <= weaponData[i].magazineSize)
                {
                    weaponData[i].bulletsLeft += pickedAmmo;
                }
                else
                {
                    weaponData[i].bulletsLeft = weaponData[i].magazineSize;
                }
                    GetComponent<AudioSource>().Play();
                    GetComponent<SphereCollider>().enabled = false;
                    render.SetActive(false); 
                    Invoke("Destr()",3);
            }
        }

        
    }
    private void Destr()
    {
        Destroy(this.gameObject);
    }
}
