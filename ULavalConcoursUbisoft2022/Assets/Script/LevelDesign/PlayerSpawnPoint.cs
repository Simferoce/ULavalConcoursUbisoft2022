using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    [SerializeField] public bool MainSpawnPoint = false;

    [ContextMenu("Teleport")]
    public void Teleport()
    {
        FindObjectOfType<Player>().GetComponentInChildren<Entity>().Teleport(this.transform.position, this.transform.forward);
    }
}
