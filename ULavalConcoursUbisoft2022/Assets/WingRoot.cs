using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WingRoot : MonoBehaviour
{
    [SerializeField] private string _sceneName = "";

    [SerializeField] private UnityEvent OpenZoneEvent = null;
    [SerializeField] private UnityEvent CloseZoneEvent = null;

    public string SceneName { get => _sceneName; }

    private void Start()
    {
        if (GameObject.FindObjectOfType<LevelManager>() == null)
        {
            NavMeshSurface navMeshSurfaces = GameObject.FindObjectOfType<NavMeshSurface>();
            navMeshSurfaces.BuildNavMesh();
        }

        OpenZone();
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
}
