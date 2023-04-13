using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableMenu : MonoBehaviour
{
    public GameObject menu1;
    public GameObject menu2;
    public GameObject menu3;
    public GameObject menu4;
    public GameObject menu5;
    public GameObject menu6;
    public GameObject menu7;

    public void setDisable()
    {
        menu7.SetActive(false);
    }

    public void setDisableAll()
    {
        menu1.SetActive(false);
        menu2.SetActive(false);
        menu3.SetActive(false);
        menu4.SetActive(false);
        menu5.SetActive(false);
        menu6.SetActive(false);
    }

    public void setEnable()
    {
        menu7.SetActive(true);
    }

    public void setEnableAll()
    {
        menu1.SetActive(true);
        menu2.SetActive(true);
        menu3.SetActive(true);
        menu4.SetActive(true);
        menu5.SetActive(true);
        menu6.SetActive(true);
    }
}
