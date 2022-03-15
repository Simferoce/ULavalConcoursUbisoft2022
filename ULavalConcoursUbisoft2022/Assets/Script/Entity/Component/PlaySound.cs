using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlaySound : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        [SerializeField] public string Name = "";
        [SerializeField] public AudioSource AudioSource = null;
    }

    [SerializeField] private List<Sound> _sounds = new List<Sound>();

    public void Execute(string soundName)
    {
        Sound sound = _sounds.FirstOrDefault(x => x.Name == soundName);

        if(sound != null)
        {
            sound.AudioSource.Play();
        }
        else
        {
            Debug.LogError("Not found sound");
        }
    }
}
