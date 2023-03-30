using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    public GameObject MenuOffOn;
    public GameObject PlaneCredits;
    public GameObject PlaneOptions;
    public GameObject ButtonMute;
    public AudioSource MenuMusic;
    public AudioSource ChoiceMusic;

    private bool muted = false;

    public void Credits()
    {
        PlaneCredits.SetActive(true);
        MenuOffOn.SetActive(false);
    }

    public void Options()
    {
        PlaneOptions.SetActive(true);
        MenuOffOn.SetActive(false);
    }

    public void ReturnMenu()
    {
        if (PlaneCredits == true)
        {
            PlaneCredits.SetActive(false);
            MenuOffOn.SetActive(true);
        }
        if (PlaneOptions == true)
        {
            PlaneOptions.SetActive(false);
            MenuOffOn.SetActive(true);
        }
    }

    public void TurnOnOff()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
            ButtonMute.SetActive(true);
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
            ButtonMute.SetActive(false);
        }
    }
}
