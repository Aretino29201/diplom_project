using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    public GameObject deathScreen;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        deathScreen.SetActive(false);
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
