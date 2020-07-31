using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherInputs : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField] 
    private ThirdPersonCamera _cam; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseMenu.activeSelf)
            {
                _pauseMenu.SetActive(false);
                GetComponent<PlayerMovement>().enabled = true;
                GetComponent<PlayerCombat>().enabled = true;
                Cursor.visible = false;
                _cam.enabled = true;
                Time.timeScale = 1;

                if (Cursor.lockState == CursorLockMode.None)
                    Cursor.lockState = CursorLockMode.Locked;
            }

            else
            {
                _pauseMenu.SetActive(true);
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerCombat>().enabled = false;
                Cursor.visible = true;
                _cam.enabled = false;
                Time.timeScale = 0;

                if(Cursor.lockState == CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
