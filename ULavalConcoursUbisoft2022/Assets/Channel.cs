using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Channel : MonoBehaviour
{
    [System.Serializable]
    public class ChannelObject
    {
        public string Name;
        public UnityEvent Event;
    }

    [SerializeField] private List<ChannelObject> _channel = new List<ChannelObject>();

    public void Signal(string channel)
    {
        foreach (Channel root in GameObject.FindObjectsOfType<Channel>(true))
        {
            root.Invoke(channel);
        }
    }

    public void Invoke(string channel)
    {
        _channel.FirstOrDefault(x => x.Name == channel)?.Event?.Invoke();
    }
}
