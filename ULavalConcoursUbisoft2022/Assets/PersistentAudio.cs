using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
    [SerializeField] private GameObject _prefab = null;

    public void Play()
    {
        Instantiate(_prefab, this.transform.position, Quaternion.identity);
    }
}
