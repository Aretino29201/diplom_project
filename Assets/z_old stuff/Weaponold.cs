using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponold: MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private float bulletSpeed;
    private GameObject lastBullet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shoot()
    {
        Debug.Log("shoot");
        lastBullet = Instantiate(bulletPrefab, bulletPosition.position, transform.rotation);
        lastBullet.GetComponent<Rigidbody>().AddForce(bulletPosition.transform.forward * bulletSpeed);
        Destroy(lastBullet, 10);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
