using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuOption : MonoBehaviour
{

    public AudioMixer audioMixer;



   public void setVolume (float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SettingRetour()
    {
        SceneManager.LoadScene("menu");


    }
}
