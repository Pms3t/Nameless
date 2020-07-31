using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectWhenButtonIsPressed : MonoBehaviour
{
    public GameObject spawnableObject;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Spawn();
            // ONLY FOR TESTING
            //var rp = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
            //rp.DecreaseLightAttack(10);
        }
    }

    void Spawn()
    {
        // I want you to spawn an object over here
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Throw the object over there where I pointed
        Instantiate(spawnableObject, position, Quaternion.identity);
    }
}
