using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlaySound : MonoBehaviour
{
    public void Execute(string soundName)
    {
        Channel.Signal(soundName);
    }
}
