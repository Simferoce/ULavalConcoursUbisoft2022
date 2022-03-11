using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnOptions : MonoBehaviour
{
    // Start is called before the first frame update
   public void OnMouseDown()
    {
        SceneManager.LoadScene("OptionsMenu");

    }
    public void SetFullscreen( bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

    }
}
