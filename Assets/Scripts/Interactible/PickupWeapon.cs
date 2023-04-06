using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
   [SerializeField] private int type; // 0 - w, 1 - p, 2 - a, 3 - u
   [SerializeField] private WeaponData weapon;
    [SerializeField] private int passID, actID, ultID;
    [SerializeField] private InterfaceManager interfaceManager;
    [SerializeField] private WeaponList gunList;
    [SerializeField]private Inventory inv;
    [SerializeField] private bool isWeapon = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.F))
        {
            switch (type)
            {
                case 0:
                    inv.PickupGun(weapon);
                    Debug.Log("Weapon picked");
                    break;
                case 1:
                    
                    inv.PickupPasSkill(passID);
                    Debug.Log("Passive picked");
                    break;
                case 2:
                    inv.PickupActSkill(actID);
                    Debug.Log("Active picked");
                    break;
                case 3:
                    inv.PickupUlt(ultID);
                    Debug.Log("Ultimate picked");
                    break;
            }

            Destroy(this.gameObject);
            interfaceManager.messageBox.text = "";
            interfaceManager.descriptionBox.text = "";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case 0:
                    interfaceManager.messageBox.text = "Press F to pickup " + weapon.gunName;
                    break;
                case 1:
                    interfaceManager.messageBox.text = "Press F to pickup Passive skill " + interfaceManager.passDesc.skillName[passID];
                    interfaceManager.descriptionBox.text = interfaceManager.passDesc.skillDesc[passID];
                    break;
                case 2:
                    interfaceManager.messageBox.text = "Press F to pickup Active skill "+ interfaceManager.actDesc.skillName[actID];
                    interfaceManager.descriptionBox.text = interfaceManager.actDesc.skillDesc[actID];
                    break;
                case 3:
                    interfaceManager.messageBox.text = "Press F to pickup Ultimate "+ interfaceManager.ultDesc.skillName[ultID];
                    interfaceManager.descriptionBox.text = interfaceManager.ultDesc.skillDesc[ultID];
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interfaceManager.messageBox.text = "";
            interfaceManager.descriptionBox.text = "";
        }
    }
    private void Start()
    {
        type = Random.Range(0, 4);
        if (isWeapon) type = 0;

        passID = Random.Range(1, 3);
        actID= Random.Range(1, 3);
        ultID= Random.Range(1, 2);
        weapon = gunList.weapons[Random.Range(1, 3)];
        interfaceManager = GameObject.Find("Manager").GetComponent<InterfaceManager>();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
}
