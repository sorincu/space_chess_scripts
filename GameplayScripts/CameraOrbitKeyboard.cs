using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script uses the keyboard input to orbit the camera around the grid in the Game Scene

public class CameraOrbitKeyboard : MonoBehaviour {
    public float sensitivityHor = 1.0f;
    public float sensitivityVert = 1.0f;
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;
    private float _rotationX = 0;

    void Start() {
        transform.localEulerAngles = new Vector3(0, 90f, 0);
    }

    void Update() {
        _rotationX += Input.GetAxis("Vertical") * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
        float delta = Input.GetAxis("Horizontal") * sensitivityHor;
        float rotationY = transform.localEulerAngles.y - delta;
        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }
}
