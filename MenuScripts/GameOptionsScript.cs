using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOptionsScript : MonoBehaviour {
    public void PlayHuman() {
        GridSizeController.xSize = GameObject.Find("X-Slider").GetComponent<Slider>();
        GridSizeController.ySize = GameObject.Find("Y-Slider").GetComponent<Slider>();
        GridSizeController.zSize = GameObject.Find("Z-Slider").GetComponent<Slider>();
        SceneManager.LoadScene("Scenes/Gameplay");
    }

    public void Tutorial() {
        SceneManager.LoadScene("Scenes/Tutorial");
    }

    public void GoToMenu() {
        SceneManager.LoadScene("Scenes/Menu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
