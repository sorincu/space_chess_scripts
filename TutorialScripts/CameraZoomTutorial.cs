using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomTutorial : MonoBehaviour {
    public float scrollSensitivity = 10f;
    public float scrollDampening = 6f;
    private float _cameraDistance = 2f;
    private Transform _cameraObj;
    private Transform _parentObj;

    void Start() {
        this._cameraObj = this.transform;
        this._parentObj = this.transform.parent;

        // Zooming camera effect at the start of the game
        this._cameraObj.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._cameraObj.localPosition.z, this._cameraDistance * -50f, Time.deltaTime * scrollDampening));
    }

    void LateUpdate() {
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        scrollAmount *= (this._cameraDistance * 0.3f);
        this._cameraDistance += scrollAmount * -1f;
        this._cameraDistance = Mathf.Clamp(this._cameraDistance, 1.5f, 20f);

        if (this._cameraObj.localPosition.z != this._cameraDistance * -1f) {
            this._cameraObj.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._cameraObj.localPosition.z, this._cameraDistance * -1f, Time.deltaTime * scrollDampening));
        }
    }
}
