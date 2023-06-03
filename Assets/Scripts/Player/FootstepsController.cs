using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsController : MonoBehaviour
{
    public AudioSource audioSrc;
    public bool isPlaying;

    public KinematicCharacterMotor Motor;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D))
        {
            // ���� ���� �� �������������, ��������� ���
            if (!isPlaying)
            {
                audioSrc.Play();
                isPlaying = true;
            }
        }
        else
        {
            if(isPlaying)
            {
                audioSrc.Stop();
                isPlaying = false;
            }
        }

    }
}
