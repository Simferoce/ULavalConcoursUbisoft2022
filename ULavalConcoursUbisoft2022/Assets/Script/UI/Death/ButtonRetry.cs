using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonRetry : MonoBehaviour
{
    // Start is called before the first frame update

   







    public void  RetryButton()
    {
        SceneManager.UnloadScene("Level");
        SceneManager.LoadScene("Level");
        Time.timeScale = 1f;


    }
}
