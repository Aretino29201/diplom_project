using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : MonoBehaviour
{
    Player plr;
    ExampleCharacterController ctrl;
    UltSkill ult;
    float sMaxHP, sCurrHP, sMoveSpeed, sAirSpeed;
    [SerializeField] List<float> sMagSize = new List<float>(), sWeapDmg = new List<float>(), sWeapSkor = new List<float>();
    public WeaponList weapons;
    public int id;
    private void Start()
    {
        plr = GetComponent<Player>();
        ctrl = GetComponent<ExampleCharacterController>();
        ult = GetComponent<UltSkill>();

        //Start Stats
        sMaxHP = plr.maxHP;
        sCurrHP = plr.currHP;
        sMoveSpeed = ctrl.MaxStableMoveSpeed;
        sAirSpeed = ctrl.MaxAirMoveSpeed;
        foreach (WeaponData weapon in weapons.weapons)
        {
            sMagSize.Add(weapon.magazineSize);
            sWeapDmg.Add(weapon.damage);
            sWeapSkor.Add(weapon.timeBetweenShooting);
        }
    }

    public void UsePassiveSkill(int psID)
    {
        ResetStats();
        switch (psID)
        {
            case 1:
                DoubleHPHalfSpeed();
                break;
            case 2:
                DoubleSpeedHalfHp();
                break;
            case 3:
                DoubleAmmo();
                break;
            case 4:
                FastShoot();
                break;
            case 5:
                SlowShoot();
                break;
            case 6:
                FasterUlt();
                break;
        }
    }

    private void ResetStats()
    {
        //player stats
        plr.currHP = sCurrHP;
        plr.maxHP = sMaxHP;
        //gun stats

        //controller stats
        ctrl.MaxAirMoveSpeed = sAirSpeed;
        ctrl.MaxStableMoveSpeed = sMoveSpeed;

        ult.ultCharge = 100;
        for (int i = 0; i < sMagSize.Count; i++)
        {
            weapons.weapons[i].magazineSize = ((int)sMagSize[i]);
            weapons.weapons[i].damage = sWeapDmg[i];
            weapons.weapons[i].timeBetweenShooting = sWeapSkor[i];
            if (weapons.weapons[i].magazineSize < weapons.weapons[i].bulletsLeft)
            {
                weapons.weapons[i].bulletsLeft = weapons.weapons[i].magazineSize;
            }
        }
    }

    private void DoubleHPHalfSpeed()
    {
        plr.maxHP*=1.5f;
        plr.currHP*=1.5f;
        ctrl.MaxStableMoveSpeed/= 2f;
        ctrl.MaxAirMoveSpeed /= 2f;
    }

    private void DoubleSpeedHalfHp()
    {
        plr.maxHP /= 1.5f;
        plr.currHP /= 1.5f;
        ctrl.MaxStableMoveSpeed *= 1.5f;
        ctrl.MaxAirMoveSpeed *= 1.5f;
    }

    private void DoubleAmmo()
    {
        foreach(WeaponData weapon in weapons.weapons) 
        {
            weapon.magazineSize *= 2;
        }
    }

    private void FastShoot()
    {
        foreach (WeaponData weapon in weapons.weapons)
        {
            weapon.damage /= 1.5f;
            weapon.timeBetweenShooting /= 1.5f;
        }
    }

    private void SlowShoot()
    {
        foreach (WeaponData weapon in weapons.weapons)
        {
            weapon.damage *= 1.5f;
            weapon.timeBetweenShooting *= 1.5f;
        }
    }

    private void FasterUlt()
    {
        ult.ultCharge *= 0.75f;
        ult.currUltCharge *= 0.75f;

    }
    private void OnDestroy()
    {
        ResetStats();
    }
}
