using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour
{
    [SerializeField] private float damage, explosionRadius;
    [SerializeField] private bool isExploded;
    public GameObject explEffect;
    UltSkill ult;
    Inventory inv;

    private void Awake()
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        ult = GameObject.FindGameObjectWithTag("Player").GetComponent<UltSkill>();
        StartCoroutine(ExplosionEffect(1));
        Explode();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
        else if (other.CompareTag("Player"))
        {
            
        }
        
    }

    void Explode()
    {
        if (inv.currWeapon.isProjectile) damage = inv.currWeapon.damage; // берем значение урона из ракетницы

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if(collider.CompareTag("Player"))
                collider.gameObject.GetComponent<Player>().currHP -= damage / 2;

            EnemyController enemy = collider.GetComponent<EnemyController>();
            if (enemy != null)
            {
                // Вызываем функцию TakeDamage для объектов Enemy в радиусе взрыва
                enemy.TakeDamage(damage);
                if (ult.currUltCharge < ult.ultCharge)
                    ult.currUltCharge += (inv.currWeapon.damage / 10);
                if (ult.currUltCharge > ult.ultCharge)
                    ult.currUltCharge = ult.ultCharge;
            }
        }
    }

        private IEnumerator ExplosionEffect(float d)
    {
        GameObject explObj = Instantiate(explEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(d);
        Destroy(explObj);
    }

    private IEnumerator StopDamage(float d)
    {
        yield return new WaitForSeconds(d);
        Destroy(this.gameObject);
    }
}
