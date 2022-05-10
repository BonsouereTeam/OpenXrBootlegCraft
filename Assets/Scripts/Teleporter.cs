using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = destination.position;
    }
}
