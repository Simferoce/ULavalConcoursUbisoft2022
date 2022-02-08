using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class onClickStart : MonoBehaviour
{
    public void OnMouseDown()
    {
        SceneManager.LoadScene("ClassSelection");
    }
}
