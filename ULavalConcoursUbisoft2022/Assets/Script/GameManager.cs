using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player player = null;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {
            Invoke("EndGame", 0.1f);
        }
    }

    void EndGame() {
        SceneManager.LoadScene("menu");
    }
}
