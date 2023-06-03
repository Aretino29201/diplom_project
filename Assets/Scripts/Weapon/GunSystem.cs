using UnityEngine;
using TMPro;
using EZCameraShake;

public class GunSystem : MonoBehaviour
{

    //Gun stats
    public Inventory inv;
    public UltSkill ult;
    //private int damage;
    //private float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    //private int magazineSize, bulletsPerTap;
    //private bool allowButtonHold;
    //public int bulletsLeft, bulletsShot;
    //private int ammoType; // 0 - shotgun, 1 - smg, 2 - rpg

    //bools 
    public bool isWeapon;
    bool  shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public CameraShaker camShake;
    public float camShakeMagnitude, camShakeDuration;
    public Transform renderPoint;
    private GameObject gunModel;

    private AudioSource audioSrc;

   


    private void Start()
    {
        inv = GetComponent<Inventory>();
        ult= GetComponent<UltSkill>();
        audioSrc = GetComponent<AudioSource>();
    }

    public void UpdateGun(WeaponData newWeapon)
    {       
        if (gunModel != null)
        {
            Destroy(gunModel);
        }

       gunModel = GameObject.Instantiate(newWeapon.gunModel, renderPoint);

        readyToShoot = true;
        isWeapon= true;
    }

    private void Update()
    {
        if(isWeapon)
        MyInput();
        
    }
    private void MyInput()
    {
        if (inv.currWeapon.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

      //  if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && inv.currWeapon.bulletsLeft > 0)
        {
            inv.currWeapon.bulletsShot = inv.currWeapon.bulletsPerTap;
            Shoot();
            // audio
            audioSrc.pitch = 1;
            audioSrc.clip = inv.currWeapon.shootSound;
            audioSrc.pitch = 1 + (float)Random.Range(-7, 7) / 100;
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-inv.currWeapon.spread, inv.currWeapon.spread);
        float y = Random.Range(-inv.currWeapon.spread, inv.currWeapon.spread);

        
            //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);


        if (!inv.currWeapon.isProjectile)
        {
            //RayCast
            if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, inv.currWeapon.range, whatIsEnemy))
            {
                //Debug.Log(rayHit.collider.name);
                //--------------------------------------------------------ENEMY HIT------------------------------------
                if (rayHit.collider.CompareTag("Enemy"))
                {
                    rayHit.collider.GetComponent<EnemyController>().TakeDamage(inv.currWeapon.damage);
                    if (ult.currUltCharge < ult.ultCharge)
                        ult.currUltCharge += (inv.currWeapon.damage / 10);
                    if (ult.currUltCharge > ult.ultCharge)
                        ult.currUltCharge = ult.ultCharge;

                }
            }


            //ShakeCamera
            // camShake.ShakeOnce(camShakeMagnitude, 0.5f, 0f, 0.5f);
            
            

            //Graphics
           GameObject bullethole = Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
            bullethole.transform.SetParent(rayHit.transform);
        }
        else // isProjectile
        {
            Rigidbody rb = Instantiate(inv.currWeapon.projectile, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(direction * inv.currWeapon.projectile.pSpeed, ForceMode.Impulse);

        }
        // Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        if (!inv.currWeapon.isMelee)
        {
            inv.currWeapon.bulletsLeft--;
            inv.currWeapon.bulletsShot--;
        }
       

        Invoke("ResetShot", inv.currWeapon.timeBetweenShooting);

        if (inv.currWeapon.bulletsShot > 0 && inv.currWeapon.bulletsLeft > 0)
            Invoke("Shoot", inv.currWeapon.timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", inv.currWeapon.reloadTime);
    }
    private void ReloadFinished()
    {
        inv.currWeapon.bulletsLeft = inv.currWeapon.magazineSize;
        reloading = false;
    }
}
