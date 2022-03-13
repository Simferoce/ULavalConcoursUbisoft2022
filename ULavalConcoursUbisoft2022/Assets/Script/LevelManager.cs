using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<string> _wingsName = null;
    [SerializeField] private string _bossWingName = "";
    [SerializeField] private string _safeZoneName = "";
    [SerializeField] private GameObject _playerPrefab = null;
    [SerializeField] private NavMeshSurface _navMeshSurfaces = null;

    private LowerWall[] _walls = null;

    private void Awake()
    {
        SceneManager.LoadScene(_bossWingName, LoadSceneMode.Additive);
        SceneManager.LoadScene(_safeZoneName, LoadSceneMode.Additive);

        List<int> randomWings = GenerateRandomsWithoutRepeat(2, _wingsName.Count);

        SceneManager.LoadScene(_wingsName[randomWings[0]], LoadSceneMode.Additive);
        SceneManager.LoadScene(_wingsName[randomWings[1]], LoadSceneMode.Additive);
    }

    private void Start()
    {
        List<Vector3> direction = new List<Vector3>() { Vector3.right, Vector3.forward, Vector3.left, Vector3.back };
        List<int> randomOrder = GenerateRandomsWithoutRepeat(4, 4);
        
        WingRoot[] wingRoots = GameObject.FindObjectsOfType<WingRoot>().Where(x => x.SceneName != "hub").ToArray();
        wingRoots[0].transform.rotation = Quaternion.LookRotation(direction[randomOrder[0]], Vector3.up);
        wingRoots[1].transform.rotation = Quaternion.LookRotation(direction[randomOrder[1]], Vector3.up);
        wingRoots[2].transform.rotation = Quaternion.LookRotation(direction[randomOrder[2]], Vector3.up);
        wingRoots[3].transform.rotation = Quaternion.LookRotation(direction[randomOrder[3]], Vector3.up);

        foreach(WingRoot wing in GameObject.FindObjectsOfType<WingRoot>())
        {
            wing.SingleWingScene = false;
        }

        PlayerSpawnPoint playerSpawnPoint = GameObject.FindObjectsOfType<PlayerSpawnPoint>().FirstOrDefault(x => x.MainSpawnPoint);
        Instantiate(_playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);

        _navMeshSurfaces.BuildNavMesh();
    }

    private List<int> GenerateRandomsWithoutRepeat(int numberOfChoosen, int size)
    {
        List<int> choosenIndex = new List<int>();
        if (size < numberOfChoosen)
        {
            Debug.LogError("There is less choice than desired choosen");
            return choosenIndex;
        }

        List<int> index = new List<int>();
        for(int i = 0; i < size; ++i)
        {
            index.Add(i);
        }

        for(int i = 0; i < numberOfChoosen; ++i)
        {
            int random = Random.Range(0, index.Count);
            choosenIndex.Add(index[random]);
            index.RemoveAt(random);
        }

        return choosenIndex;
    }
}
