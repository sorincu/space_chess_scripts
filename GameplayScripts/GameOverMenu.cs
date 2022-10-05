using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {
    public void goToMenu() {
        SceneManager.LoadScene("Scenes/Menu");
    }

    public void quitApp() {
        Application.Quit();
    }
}
