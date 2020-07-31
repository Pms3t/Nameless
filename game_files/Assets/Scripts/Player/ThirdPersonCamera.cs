using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool _hideCursor = true;
    [SerializeField]
    private float _mouseSensitivity = 10;
    [SerializeField]
    private float _distanceFromTarget = 5;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Vector2 _yMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float x;
    float y;

    void Start(){
        if(_hideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Called after all other update methods.
    void LateUpdate()
    {
        // Get mouse input
        x += Input.GetAxis("Mouse X") * _mouseSensitivity;
        y -= Input.GetAxis("Mouse Y") * _mouseSensitivity;
        // Restrict camera movement
        y = Mathf.Clamp(y, _yMinMax.x, _yMinMax.y);

        // Calculate the rotation around the target
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(y, x), 
        ref rotationSmoothVelocity, rotationSmoothTime);

        // Declare rotation changes
        transform.eulerAngles = currentRotation;

        // Follow the target
        transform.position = _target.position - transform.forward * _distanceFromTarget;
    }
}
