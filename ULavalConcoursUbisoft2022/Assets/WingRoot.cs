using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class WingRoot : MonoBehaviour
{
    [SerializeField] private string _sceneName = "";

    private void Start()
    {
        if (GameObject.FindObjectOfType<LevelManager>() == null)
        {
            NavMeshSurface navMeshSurfaces = GameObject.FindObjectOfType<NavMeshSurface>();
            navMeshSurfaces.BuildNavMesh();
        }
    }
}
