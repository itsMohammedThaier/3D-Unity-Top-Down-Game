using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunsManager : MonoBehaviour
{
    public GameObject[] guns;
    private int currentGunIndex = 0, lastGunIndex;
    
    void  Start()
    {
        lastGunIndex = guns.Length;
    }    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ChangeGun();

    }
    void ChangeGun()
    {
        currentGunIndex++;
        if (currentGunIndex < lastGunIndex)
        {
            guns[currentGunIndex - 1].SetActive(false);
            guns[currentGunIndex].SetActive(true);
            return;
        }

        guns[currentGunIndex - 1].SetActive(false);
        currentGunIndex = 0;
        guns[currentGunIndex].SetActive(true);
    }
}
