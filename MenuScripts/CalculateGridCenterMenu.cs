using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script positions the FocusObject in the center of the grid in the Menu Scene
public class CalculateGridCenterMenu : MonoBehaviour {
    public TMPro.TMP_Text xSize, ySize, zSize;

    void Update() {
        transform.localPosition = new Vector3(float.Parse(xSize.text) / 2, float.Parse(ySize.text) / 2, float.Parse(zSize.text) / 2);
    }
}
