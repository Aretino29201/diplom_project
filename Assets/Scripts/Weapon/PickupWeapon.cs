using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
   [SerializeField] private WeaponData weapon;
    [SerializeField] private TMP_Text text;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.F))
        {
            other.GetComponent<Inventory>().PickupGun(weapon);
            Debug.Log("Weapon picked");
            Destroy(this.gameObject);
            text.text = "";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        text.text = "Press F to pickup "+ weapon.name;
    }
    private void OnTriggerExit(Collider other)
    {
        text.text = "";
    }
    private void Start()
    {
        text = GameObject.Find("MessageBox").GetComponent<TMP_Text>();
    }


}
