using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "Weapon Data", order = 51)]

public class WeaponData : ScriptableObject
{
    //Gun stats
    public string gunName;
    public int ammoType; // 0 - shotgun, 1 - smg, 2 - rpg
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public int bulletsLeft, bulletsShot;

    public GameObject gunModel;

    public bool isMelee;
}
