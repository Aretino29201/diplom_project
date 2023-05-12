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
     

    public GameObject deathScreen, pauseScreen, resurrectScreen;
    public TMP_Text ammoText, hpText;
    public Slider ultSlide;
    public TMP_Text ultText;

    public Image actSkillCdtImage, actCgtBG;


    public SkillsDescription passDesc, actDesc, ultDesc;
    public TMP_Text passName, actName, ultName, messageBox, descriptionBox, actSkillCdText;

    private bool notPaused = true, setTempCD = true;
    float cooldownTimeStart = 0, cooldownTemp;

    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gun = plr.GetComponent<GunSystem>();
        inv = plr.GetComponent<Inventory>();
        pass = plr.GetComponent<PassiveSkill>();
        act = plr.GetComponent<ActiveSkill>();
        ult = plr.GetComponent<UltSkill>();
        messageBox.text = "";
        descriptionBox.text = "";
        Time.timeScale = 1;
        deathScreen.SetActive(false);
    }

    private void FixedUpdate()
    {
        //вывод на экран здоровья и патронов
        hpText.SetText(((int)plr.currHP).ToString());
        if (gun.isWeapon && !inv.currWeapon.isMelee)
            ammoText.SetText(inv.currWeapon.bulletsLeft / inv.currWeapon.bulletsPerTap + " / " + inv.currWeapon.magazineSize / inv.currWeapon.bulletsPerTap);
        else ammoText.SetText("");
        
        
        passName.text = passDesc.skillName[inv.currPasSkill];

        //кулдаун активной способности
        actName.text = actDesc.skillName[inv.currActSkill];
        if (act.isCooldown)
        {
            if (setTempCD)
            {
                cooldownTemp = act.cooldownTime;
                setTempCD = false;
            }

            cooldownTimeStart += Time.fixedDeltaTime;
            cooldownTemp -= Time.fixedDeltaTime;
            actSkillCdText.text = Mathf.RoundToInt(cooldownTemp).ToString();
            actSkillCdtImage.fillAmount = cooldownTimeStart / act.cooldownTime;
            actCgtBG.color = Color.red;
        }
        else if(inv.currActSkill == 0)
        {
            actSkillCdText.text = "";
            actCgtBG.color = Color.grey;
        }
        else
        {
            actSkillCdText.text = "";
            cooldownTimeStart = 0;
            actCgtBG.color = Color.green;
            setTempCD = true;
        }

        //заряд ультимейта
        ultName.text = ultDesc.skillName[inv.currUlt];
        ultSlide.value = ult.currUltCharge/ult.ultCharge;
        ultText.text = ((int)(ult.currUltCharge / ult.ultCharge * 100)).ToString() + "%";


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOn(notPaused);
        }
        
        
    }
    public void DeathOn()
    {
        deathScreen.SetActive(true); 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void PauseOn(bool pause)
    {
        
        if (pause)
        {
            pauseScreen.SetActive(true);
            Time.timeScale= 0;
            notPaused = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            notPaused= true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void RestartGame()
    {
        Debug.Log("Restarting...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReturnToMenu()
    {
        Debug.Log("To Menu!");
        SceneManager.LoadScene(0);
    }
}
