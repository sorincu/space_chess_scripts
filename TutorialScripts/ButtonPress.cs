using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour {
    static public int currentScene;

    void Start() {
        currentScene = 1;
        GameObject.Find("GridDisplay").GetComponent<Tutorial>().onValueChange(currentScene);
    }

    public void nextBtnPress() {
        if (currentScene < 33) {
            currentScene++;
            GameObject.Find("GridDisplay").GetComponent<Tutorial>().onValueChange(currentScene);
            Debug.Log(currentScene);
        }
    }

    public void prevBtnPress() {
        if (currentScene > 1) {
            currentScene--;
            GameObject.Find("GridDisplay").GetComponent<Tutorial>().onValueChange(currentScene);
            Debug.Log(currentScene);
        }
    }
}
