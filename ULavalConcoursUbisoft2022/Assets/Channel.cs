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
        foreach (WingRoot root in GameObject.FindObjectsOfType<WingRoot>())
        {
            Invoke(channel);
        }
    }

    private void Invoke(string channel)
    {
        _channel.FirstOrDefault(x => x.Name == channel)?.Event?.Invoke();
    }
}
