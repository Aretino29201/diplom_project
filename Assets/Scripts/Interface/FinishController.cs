using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You Won!!!!!!!!");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.UnloadScene(SceneManager.GetActiveScene());
            SceneManager.LoadScene(0);
        }
    }
}
