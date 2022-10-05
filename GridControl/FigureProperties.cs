using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureProperties : MonoBehaviour {
    public bool isHovered;

    void Update() {
        if (isHovered) {
            this.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            this.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
        } else {
            this.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
