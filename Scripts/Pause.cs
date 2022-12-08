using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pause;
    private bool isPaused = false;
    void Start()
    {
        pause.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
            isPaused = !isPaused;
            pause.SetActive(isPaused);
        }
        if (Input.GetKeyDown(KeyCode.Q) && isPaused)
        {
            ExitInMenu();
        }
    }

    public void PauseOff()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void ExitInMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
