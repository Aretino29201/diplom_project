using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    
    public Player plr;
    public GunSystem gun;
    public Inventory inv;
    public PassiveSkill pass;
    public ActiveSkill act;
    public UltSkill ult;

    public GameObject deathScreen;
    public TMP_Text ammoText, hpText;
    public Slider ultSlide;
    public TMP_Text ultText;

    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gun = plr.GetComponent<GunSystem>();
        inv = plr.GetComponent<Inventory>();
        pass = plr.GetComponent<PassiveSkill>();
        act = plr.GetComponent<ActiveSkill>();
        ult = plr.GetComponent<UltSkill>();

        Time.timeScale = 1;
        deathScreen.SetActive(false);
    }

    private void FixedUpdate()
    {
        hpText.SetText(plr.currHP.ToString());

        if (gun.isWeapon && !inv.currWeapon.isMelee)
            ammoText.SetText(inv.currWeapon.bulletsLeft / inv.currWeapon.bulletsPerTap + " / " + inv.currWeapon.magazineSize / inv.currWeapon.bulletsPerTap);
        else ammoText.SetText("");

        ultSlide.value = ult.currUltCharge/ult.ultCharge;
        ultText.text = (ult.currUltCharge / ult.ultCharge * 100).ToString() + "%";
    }
    public void DeathOn()
    {
        deathScreen.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
