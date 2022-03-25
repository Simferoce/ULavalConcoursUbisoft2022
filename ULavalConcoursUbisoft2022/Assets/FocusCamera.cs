using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    public void Focus()
    {
        GameObject.FindObjectOfType<CameraControl>().Focus(_target);
    }

    public void UnFocus()
    {
        GameObject.FindObjectOfType<CameraControl>().ResetToOrigin();
    }
}
