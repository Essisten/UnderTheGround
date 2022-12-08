using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject exitTable;
    public TMPro.TMP_Dropdown ResolutionsList;
    public bool isFullScreen = true;

    private void Start()
    {
        exitTable.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SaveGame()
    {
        return;
    }

    public void LoadGame()
    {
        return;
    }

    public void USureToExit()
    {
        if (!exitTable.active)
            exitTable.SetActive(true);
    }

    public void YesImSure()
    {
        Application.Quit();
    }

    public void NoImNotSure()
    {
        exitTable.SetActive(false);
    }

    public void ChangeResolution()
    {
        if(ResolutionsList.value == 0)
            Screen.SetResolution(640, 360, isFullScreen);
        if (ResolutionsList.value == 1)
            Screen.SetResolution(854, 480, isFullScreen);
        if (ResolutionsList.value == 2)
            Screen.SetResolution(960, 540, isFullScreen);
        if (ResolutionsList.value == 3)
            Screen.SetResolution(1280, 720, isFullScreen);
        if (ResolutionsList.value == 4)
            Screen.SetResolution(1600, 900, isFullScreen);
        if (ResolutionsList.value == 5)
            Screen.SetResolution(1920, 1080, isFullScreen);
        if (ResolutionsList.value == 6)
            Screen.SetResolution(2048, 1152, isFullScreen);
        if (ResolutionsList.value == 7)
            Screen.SetResolution(2560, 1440, isFullScreen);
        if (ResolutionsList.value == 8)
            Screen.SetResolution(3200, 1800, isFullScreen);
        if (ResolutionsList.value == 9)
            Screen.SetResolution(3840, 2160, isFullScreen);
        if (ResolutionsList.value == 10)
            Screen.SetResolution(7680, 4320, isFullScreen);
    }

    public void ChangeFullScreen()
    {
        if (!isFullScreen)
            isFullScreen = true;
        else
            isFullScreen = false;
        ChangeResolution();
    }
}
