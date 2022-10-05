using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script positions the FocusObject in the center of the grid in the Game Scene
public class CalculateGridCenterGame : MonoBehaviour {
    private float _cX, _cY, _cZ;

    void Start() {
        _cX = GridSizeController.xSize.value;
        _cY = GridSizeController.ySize.value;
        _cZ = GridSizeController.zSize.value;

        transform.localPosition = new Vector3(_cX / 2, _cY / 2, _cZ / 2);
    }
}
