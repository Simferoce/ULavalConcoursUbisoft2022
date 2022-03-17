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

    public static void Signal(string channel)
    {
        foreach (Channel root in GameObject.FindObjectsOfType<Channel>(true))
        {
            root.Invoke(channel);
        }
    }

    public void Invoke(string channel)
    {

        ChannelObject channelObject = _channel.FirstOrDefault(x => x.Name == channel);
        
        if (channelObject!= null)
        {
            channelObject?.Event?.Invoke();
        }
    }
    
    [System.Serializable]
    public class ChannelObjectEvent : UnityEvent<object> { }

    [System.Serializable]
    public class ChannelObjectWithParameters
    {
        public string Name;
        public ChannelObjectEvent Event;
    }

    [SerializeField] private List<ChannelObjectWithParameters> _channelWithObjectParameters = new List<ChannelObjectWithParameters>();

    public static void Signal(string channel, object parameters)
    {
        foreach (Channel root in GameObject.FindObjectsOfType<Channel>(true))
        {
            root.Invoke(channel, parameters);
        }
    }

    public void Invoke(string channel, object parameters)
    {
        _channelWithObjectParameters.FirstOrDefault(x => x.Name == channel)?.Event?.Invoke(parameters);
    }
}
