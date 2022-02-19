using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rebound : MonoBehaviour
{
    [SerializeField] private LayerMask mask;

    private void OnTriggerEnter(Collider other)
    {
        if (mask == (mask | (1 << other.gameObject.layer)))
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 100, mask);
            if (hits.Length != 0)
            {
                RaycastHit hit = hits.FirstOrDefault(x => x.collider.gameObject == other.gameObject);
                if(!hit.Equals(default(RaycastHit)))
                {
                    Vector3 newForward = Vector3.Reflect(transform.forward, hit.normal);
                    this.transform.rotation = Quaternion.LookRotation(newForward, Vector3.up);
                }
               
            }
        }
    }
}
