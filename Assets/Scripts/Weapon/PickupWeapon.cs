using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
   [SerializeField] private int type;
   [SerializeField] private WeaponData weapon;
    [SerializeField] private int passID, actID, ultID;
    [SerializeField] private TMP_Text pickupText;
    [SerializeField] private WeaponList gunList;
    [SerializeField] private PassiveSkill passSkill;
    [SerializeField]private Inventory inv;
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
                    passSkill.UsePassiveSkill(passID);
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
            pickupText.text = "";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //inv = other.GetComponent<Inventory>();
            switch (type)
            {
                case 0:
                    pickupText.text = "Press F to pickup " + weapon.name;
                    break;
                case 1:
                    pickupText.text = "Press F to pickup Passive skill " +passID;
                    break;
                case 2:
                    pickupText.text = "Press F to pickup Active skill "+actID;
                    break;
                case 3:
                    pickupText.text = "Press F to pickup Ultimate "+ultID;
                    break;
            }
        }
        //pickupText.text = "Press F to pickup "; // + weapon.name;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupText.text = "";
        }
    }
    private void Start()
    {
        type = Random.Range(0, 4);
        passID = Random.Range(0, 2);
        actID= Random.Range(1, 3);
        ultID= Random.Range(1, 2);
        weapon = gunList.weapons[Random.Range(0, 3)];
        pickupText = GameObject.Find("MessageBox").GetComponent<TMP_Text>();
        passSkill = GameObject.Find("Player").GetComponent<PassiveSkill>();
        inv = GameObject.Find("Player").GetComponent<Inventory>();
    }
}
