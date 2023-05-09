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
    [SerializeField] private Light pointLight;
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

        interfaceManager = GameObject.Find("Manager").GetComponent<InterfaceManager>();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        passID = Random.Range(1, interfaceManager.passDesc.skillName.Count);
        actID= Random.Range(1, interfaceManager.actDesc.skillName.Count);
        ultID= Random.Range(1, interfaceManager.ultDesc.skillName.Count);
        weapon = gunList.weapons[Random.Range(1, gunList.weapons.Count)];

        switch (type)
        {
            case 0:
                pointLight.color = Color.white;
                break;
            case 1:
                pointLight.color = Color.green;
                break;
            case 2:
                pointLight.color = Color.red;
                break;
            case 3:
                pointLight.color = Color.yellow;
                break;
        }
    }
}
