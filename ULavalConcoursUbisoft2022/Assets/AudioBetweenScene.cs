using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBetweenScene : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        transform.parent = null;
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }
}
