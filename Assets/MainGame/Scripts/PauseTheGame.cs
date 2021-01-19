using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTheGame : MonoBehaviour
{
    public GameObject pausePanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPausePanel();
        }
    }

    public int ShowPausePanel()
    {
        if (pausePanel.active)
        {
            pausePanel.active = false;
            return 0;
        }

        else
        {
            pausePanel.active = true;
        }
        return 0;
    }
}
