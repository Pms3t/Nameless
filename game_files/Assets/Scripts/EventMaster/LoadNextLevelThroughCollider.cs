using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevelThroughCollider : MonoBehaviour
{
    [SerializeField] private Loader _loader;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _loader.LoadNextLevel();
        }
    }
}
