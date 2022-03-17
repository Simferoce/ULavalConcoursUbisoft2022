using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameManager _gameManager = GameObject.FindObjectOfType<GameManager>();
                if(_gameManager != null)
                {
                    _instance = _gameManager;
                }
                else
                {
                    GameObject gameManagerPrefab = Resources.Load<GameObject>("GameManager");
                    _instance = Instantiate(gameManagerPrefab).GetComponent<GameManager>();
                }

                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    [SerializeField] private ClassData _class = null;
    public ClassData Class { get => _class; set => _class = value; }
}
