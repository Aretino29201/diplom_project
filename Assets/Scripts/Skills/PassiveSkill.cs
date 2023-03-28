using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : MonoBehaviour
{
    Player plr;
    GunSystem gun;
    ExampleCharacterController ctrl;
    float sMaxHP, sCurrHP, sMoveSpeed, sAirSpeed;
    public int id;
    private void Start()
    {
        plr = GetComponent<Player>();
        gun = GetComponent<GunSystem>();   
        ctrl= GetComponent<ExampleCharacterController>();

        //Start Stats
        sMaxHP = plr.maxHP;
        sCurrHP = plr.currHP;
        sMoveSpeed = ctrl.MaxStableMoveSpeed;
        sAirSpeed = ctrl.MaxAirMoveSpeed;
    }

    public void UsePassiveSkill(int psID)
    {
        ResetStats();
        switch (psID)
        {
            case 0:
                DoubleHPHalfSpeed();
                Debug.LogWarning("Slow, but strong");
                break;
            case 1:
                DoubleSpeedHalfHp();
                Debug.LogWarning("Fast, but weak");
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
    }

    private void DoubleHPHalfSpeed()
    {
        plr.maxHP*=2;
        plr.currHP*=2;
        ctrl.MaxStableMoveSpeed/=2;
        ctrl.MaxAirMoveSpeed /= 2;
    }

    private void DoubleSpeedHalfHp()
    {
        plr.maxHP /= 2;
        plr.currHP /= 2;
        ctrl.MaxStableMoveSpeed *= 2;
        ctrl.MaxAirMoveSpeed *= 2;
    }
}
