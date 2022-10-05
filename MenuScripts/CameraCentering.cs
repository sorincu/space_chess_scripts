using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script positions the camera, so it points to
// the grid on the right side of the Menu screen

public class CameraCentering : MonoBehaviour {
    public TMPro.TMP_Text xSize;

    private int _cX;

    void Update() {
        _cX = int.Parse(xSize.text);

        transform.localPosition = new Vector3(-2.2f - (0.25f * _cX), -1f, -3f + _cX * (-1.5f));
    }
}
