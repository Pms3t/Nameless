using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDown : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 
            transform.position.y - _speed * Time.deltaTime, transform.position.z);
    }
}
