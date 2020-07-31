using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontally : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime, 0,0, Space.Self);
    }
}
