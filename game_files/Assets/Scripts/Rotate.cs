using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] 
    private bool _rotateOn = false;
    [SerializeField] private float _speed;

    // Update is called once per frame
    void Update()
    {
        if (_rotateOn)
            transform.Rotate(Vector3.up * _speed * Time.deltaTime);
    }

    public void ToggleRotateOn()
    {
        if (_rotateOn)
            _rotateOn = false;

        else if (!_rotateOn)
            _rotateOn = true;
    }
}
