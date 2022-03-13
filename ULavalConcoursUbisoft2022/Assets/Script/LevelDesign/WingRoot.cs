using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WingRoot : MonoBehaviour
{
    [System.Serializable]
    public class Channel
    {
        public string Name;
        public UnityEvent Event;
    }

    public enum Wing
    {
        Hub,
        Boss,
        Safe,
        Wrath,
        Depression,
        Exhaustion
    }


    [SerializeField] private string _sceneName = "";
    [SerializeField] private Wing _wing = Wing.Hub;
    [SerializeField] private GameObject _playerPrefab = null;

    [SerializeField] private List<Channel> _channel = new List<Channel>();
    [SerializeField] private UnityEvent OpenZoneEvent = null;
    [SerializeField] private UnityEvent CloseZoneEvent = null;

    public string SceneName { get => _sceneName; }
    public Wing WingType { get => _wing; set => _wing = value; }
    public bool SingleWingScene = true;

    private void Start()
    {
        if (GameObject.FindObjectOfType<LevelManager>() == null)
        {
            NavMeshSurface navMeshSurfaces = GameObject.FindObjectOfType<NavMeshSurface>();
            navMeshSurfaces.BuildNavMesh();
        }

        OpenZone();

        if (SingleWingScene)
        {
            PlayerSpawnPoint playerSpawnPoint = GameObject.FindObjectOfType<PlayerSpawnPoint>();
            Instantiate(_playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);
        }
    }

    public void OpenZone()
    {
        OpenZoneEvent?.Invoke();
    }

    public void CloseZone()
    {
        CloseZoneEvent?.Invoke();
    }

    public void OpenZone(string zoneName)
    {
        GameObject.FindObjectsOfType<WingRoot>().FirstOrDefault(x => x.SceneName == zoneName)?.OpenZone();
    }

    public void CloseZone(string zoneName)
    {
        GameObject.FindObjectsOfType<WingRoot>().FirstOrDefault(x => x.SceneName == zoneName)?.CloseZone();
    }

    public void Invoke(string channel)                      
    {
        _channel.FirstOrDefault(x => x.Name == channel)?.Event?.Invoke();
    }

    public void Signal(string channel)
    {
        foreach (WingRoot root in GameObject.FindObjectsOfType<WingRoot>())
        {
            root.Invoke(channel);
        }
    }
}
