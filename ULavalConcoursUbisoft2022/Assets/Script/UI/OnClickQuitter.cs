using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickQuitter : MonoBehaviour
{
    public void OnMouseDown()
    {
        Application.Quit();
    }
}
