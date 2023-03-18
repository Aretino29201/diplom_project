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
    private Inventory inv;
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
            pickupText.text = "";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        inv = other.GetComponent<Inventory>();
        pickupText.text = "Press F to pickup "; // + weapon.name;
    }
    private void OnTriggerExit(Collider other)
    {
        pickupText.text = "";
    }
    private void Start()
    {
        pickupText = GameObject.Find("MessageBox").GetComponent<TMP_Text>();
    }
}
