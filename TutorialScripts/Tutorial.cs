using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public GameObject P, R, B, N, Q, K, b, q;
    public Material cellMatWhite, cellMatBlack, availableCellMat, currentCellMat, potentialCaptureCellMat, indicatorMat, cubeFaceMat, cubeCornerMat, attackedKingMat;
    public TMPro.TMP_Text tutorialText;
    private float _positionScale;

    void Start() {
        _positionScale = GridInitializationGame.positionScale;
    }

    public void onValueChange(int sceneNumber) {
        GameObject[] objToDelete = GameObject.FindGameObjectsWithTag("Deletable");
        foreach (GameObject obj in objToDelete)
            GameObject.Destroy(obj);

        switch (sceneNumber) {
            case 1:
                var fig1 = Instantiate(P, new Vector3(0, 0, 0), Quaternion.identity);
                fig1.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);
                createCell(1, 0, 0, availableCellMat);

                tutorialText.text = "A PAWN IN REGULAR CHESS CAN MOVE ONLY FORWARD...";
                break;
            case 2:
                var fig2 = Instantiate(P, new Vector3(0, 0, 0), Quaternion.identity);
                fig2.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);
                createCell(1, 0, 0, availableCellMat);
                createCell(1, 0, 1, potentialCaptureCellMat);
                createCell(1, 0, -1, potentialCaptureCellMat);

                tutorialText.text = "...AND CAPTURE DIAGONALLY.";
                break;
            case 3:
                var fig3 = Instantiate(P, new Vector3(0, 0, 0), Quaternion.identity);
                fig3.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                createCell(1, 0, 0, availableCellMat);
                createCell(1, 1, 0, availableCellMat);
                createCell(1, -1, 0, availableCellMat);

                createCell(1, 0, 1, potentialCaptureCellMat);
                createCell(1, 1, 1, potentialCaptureCellMat);
                createCell(1, -1, 1, potentialCaptureCellMat);

                createCell(1, 0, -1, potentialCaptureCellMat);
                createCell(1, 1, -1, potentialCaptureCellMat);
                createCell(1, -1, -1, potentialCaptureCellMat);

                tutorialText.text = "IN SPACE CHESS YOU CAN ALSO MOVE AND CAPTURE FORWARD-UP AND FORWARD-DOWN.";
                break;
            case 4:
                var fig4 = Instantiate(N, new Vector3(0, 0, 0), Quaternion.identity);
                fig4.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                tutorialText.text = "THE KNIGHT";
                break;
            case 5:
                var fig5 = Instantiate(N, new Vector3(0, 0, 0), Quaternion.identity);
                fig5.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);

                tutorialText.text = "THE KNIGHT IN CHESS MOVES 2 CELLS IN A DIMENSION...";
                break;
            case 6:
                var fig6 = Instantiate(N, new Vector3(0, 0, 0), Quaternion.identity);
                fig6.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(2, 0, 1, potentialCaptureCellMat);
                createCell(2, 0, -1, potentialCaptureCellMat);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-2, 0, 1, potentialCaptureCellMat);
                createCell(-2, 0, -1, potentialCaptureCellMat);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(1, 0, 2, potentialCaptureCellMat);
                createCell(-1, 0, 2, potentialCaptureCellMat);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(1, 0, -2, potentialCaptureCellMat);
                createCell(-1, 0, -2, potentialCaptureCellMat);

                tutorialText.text = "...AND THEN 1 CELL IN THE OTHER DIMENSION.";
                break;
            case 7:
                var fig7 = Instantiate(N, new Vector3(0, 0, 0), Quaternion.identity);
                fig7.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);

                createCell(0, 1, 0, availableCellMat);
                createCell(0, 2, 0, availableCellMat);

                createCell(0, -1, 0, availableCellMat);
                createCell(0, -2, 0, availableCellMat);

                tutorialText.text = "IN SPACE CHESS, THE KNIGHT ALSO MOVES 2 CELLS IN A DIMENSION...";
                break;
            case 8:
                var fig8 = Instantiate(N, new Vector3(0, 0, 0), Quaternion.identity);
                fig8.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(2, 1, 0, potentialCaptureCellMat);
                createCell(2, -1, 0, potentialCaptureCellMat);
                createCell(2, 0, 1, potentialCaptureCellMat);
                createCell(2, 0, -1, potentialCaptureCellMat);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-2, 1, 0, potentialCaptureCellMat);
                createCell(-2, -1, 0, potentialCaptureCellMat);
                createCell(-2, 0, 1, potentialCaptureCellMat);
                createCell(-2, 0, -1, potentialCaptureCellMat);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(0, 1, 2, potentialCaptureCellMat);
                createCell(0, -1, 2, potentialCaptureCellMat);
                createCell(1, 0, 2, potentialCaptureCellMat);
                createCell(-1, 0, 2, potentialCaptureCellMat);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(0, 1, -2, potentialCaptureCellMat);
                createCell(0, -1, -2, potentialCaptureCellMat);
                createCell(1, 0, -2, potentialCaptureCellMat);
                createCell(-1, 0, -2, potentialCaptureCellMat);

                createCell(0, 1, 0, availableCellMat);
                createCell(0, 2, 0, availableCellMat);
                createCell(0, 2, 1, potentialCaptureCellMat);
                createCell(0, 2, -1, potentialCaptureCellMat);
                createCell(1, 2, 0, potentialCaptureCellMat);
                createCell(-1, 2, 0, potentialCaptureCellMat);

                createCell(0, -1, 0, availableCellMat);
                createCell(0, -2, 0, availableCellMat);
                createCell(0, -2, 1, potentialCaptureCellMat);
                createCell(0, -2, -1, potentialCaptureCellMat);
                createCell(1, -2, 0, potentialCaptureCellMat);
                createCell(-1, -2, 0, potentialCaptureCellMat);

                tutorialText.text = "...AND THEN 1 CELL IN THE OTHER TWO DIMENSIONS.";
                break;
            case 9:
                var fig9 = Instantiate(N, new Vector3(0, 0, 0), Quaternion.identity);
                fig9.transform.tag = "Deletable";

                createCell(0, 0, 0, cellMatWhite);

                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(2, 0, 1, cellMatBlack);
                createCell(2, 0, -1, cellMatBlack);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-2, 0, 1, cellMatBlack);
                createCell(-2, 0, -1, cellMatBlack);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(1, 0, 2, cellMatBlack);
                createCell(-1, 0, 2, cellMatBlack);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(1, 0, -2, cellMatBlack);
                createCell(-1, 0, -2, cellMatBlack);

                tutorialText.text = "ALSO, THE KNIGHT RETAINS ITS TRADITIONAL PROPERTIES...";
                break;
            case 10:
                var fig10 = Instantiate(N, new Vector3(0, 0, 0), Quaternion.identity);
                fig10.transform.tag = "Deletable";

                createCell(0, 0, 0, cellMatWhite);

                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(2, 1, 0, cellMatBlack);
                createCell(2, -1, 0, cellMatBlack);
                createCell(2, 0, 1, cellMatBlack);
                createCell(2, 0, -1, cellMatBlack);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-2, 1, 0, cellMatBlack);
                createCell(-2, -1, 0, cellMatBlack);
                createCell(-2, 0, 1, cellMatBlack);
                createCell(-2, 0, -1, cellMatBlack);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(0, 1, 2, cellMatBlack);
                createCell(0, -1, 2, cellMatBlack);
                createCell(1, 0, 2, cellMatBlack);
                createCell(-1, 0, 2, cellMatBlack);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(0, 1, -2, cellMatBlack);
                createCell(0, -1, -2, cellMatBlack);
                createCell(1, 0, -2, cellMatBlack);
                createCell(-1, 0, -2, cellMatBlack);

                createCell(0, 1, 0, availableCellMat);
                createCell(0, 2, 0, availableCellMat);
                createCell(0, 2, 1, cellMatBlack);
                createCell(0, 2, -1, cellMatBlack);
                createCell(1, 2, 0, cellMatBlack);
                createCell(-1, 2, 0, cellMatBlack);

                createCell(0, -1, 0, availableCellMat);
                createCell(0, -2, 0, availableCellMat);
                createCell(0, -2, 1, cellMatBlack);
                createCell(0, -2, -1, cellMatBlack);
                createCell(1, -2, 0, cellMatBlack);
                createCell(-1, -2, 0, cellMatBlack);

                tutorialText.text = "...IT WILL ALWAYS MOVE TO A CELL OF A DIFFERENT COLOUR THAN THE CELL IT IS CURRENTLY ON.";
                break;
            case 11:
                var fig11 = Instantiate(N, new Vector3(0, 0, 0), Quaternion.identity);
                var fig11_2 = Instantiate(Q, new Vector3(2 * _positionScale, 0, 1 * _positionScale), Quaternion.identity);
                fig11.transform.tag = "Deletable";
                fig11_2.transform.tag = "Deletable";

                createCell(0, 0, 0, cellMatWhite);

                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(2, 0, 1, potentialCaptureCellMat);
                createCell(2, 0, -1, cellMatBlack);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-2, 0, 1, cellMatBlack);
                createCell(-2, 0, -1, cellMatBlack);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(1, 0, 2, cellMatBlack);
                createCell(-1, 0, 2, cellMatBlack);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(1, 0, -2, cellMatBlack);
                createCell(-1, 0, -2, cellMatBlack);

                tutorialText.text = "THE KNIGHT IS KNOWN AS QUEEN'S WORST ENEMY.";
                break;
            case 12:
                var fig12 = Instantiate(N, new Vector3(0, 0, 0), Quaternion.identity);
                var fig12_2 = Instantiate(Q, new Vector3(2 * _positionScale, 0, 1 * _positionScale), Quaternion.identity);
                fig12.transform.tag = "Deletable";
                fig12_2.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(2, 0, 1, potentialCaptureCellMat);
                createCell(2, 0, -1, availableCellMat);
                createCell(2, 0, -2, availableCellMat);
                createCell(2, 0, -3, availableCellMat);

                createCell(1, 0, 0, availableCellMat);
                createCell(0, 0, -1, availableCellMat);
                createCell(-1, 0, -2, availableCellMat);
                createCell(-2, 0, -3, availableCellMat);

                createCell(1, 0, 1, availableCellMat);
                createCell(0, 0, 1, availableCellMat);
                createCell(-1, 0, 1, availableCellMat);
                createCell(-2, 0, 1, availableCellMat);

                createCell(3, 0, 0, availableCellMat);
                createCell(3, 0, 1, availableCellMat);
                createCell(3, 0, 2, availableCellMat);

                createCell(2, 0, 2, availableCellMat);
                createCell(1, 0, 2, availableCellMat);

                tutorialText.text = "BECAUSE THE KNIGHT IS THE ONLY FIGURE WHICH CAN ATTACK, WITHOUT BEING ATTACKED BACK.";
                break;
            case 13:
                var fig13 = Instantiate(N, new Vector3(0, 0, 0), Quaternion.identity);
                var fig13_2 = Instantiate(Q, new Vector3(2 * _positionScale, 1 * _positionScale, 0), Quaternion.identity);
                fig13.transform.tag = "Deletable";
                fig13_2.transform.tag = "Deletable";

                createCell(0, 0, 0, cellMatWhite);

                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(2, 1, 0, potentialCaptureCellMat);
                createCell(2, -1, 0, cellMatBlack);
                createCell(2, 0, 1, cellMatBlack);
                createCell(2, 0, -1, cellMatBlack);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-2, 1, 0, cellMatBlack);
                createCell(-2, -1, 0, cellMatBlack);
                createCell(-2, 0, 1, cellMatBlack);
                createCell(-2, 0, -1, cellMatBlack);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(0, 1, 2, cellMatBlack);
                createCell(0, -1, 2, cellMatBlack);
                createCell(1, 0, 2, cellMatBlack);
                createCell(-1, 0, 2, cellMatBlack);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(0, 1, -2, cellMatBlack);
                createCell(0, -1, -2, cellMatBlack);
                createCell(1, 0, -2, cellMatBlack);
                createCell(-1, 0, -2, cellMatBlack);

                createCell(0, 1, 0, availableCellMat);
                createCell(0, 2, 0, availableCellMat);
                createCell(0, 2, 1, cellMatBlack);
                createCell(0, 2, -1, cellMatBlack);
                createCell(1, 2, 0, cellMatBlack);
                createCell(-1, 2, 0, cellMatBlack);

                createCell(0, -1, 0, availableCellMat);
                createCell(0, -2, 0, availableCellMat);
                createCell(0, -2, 1, cellMatBlack);
                createCell(0, -2, -1, cellMatBlack);
                createCell(1, -2, 0, cellMatBlack);
                createCell(-1, -2, 0, cellMatBlack);

                tutorialText.text = "BECAUSE THE KNIGHT IS THE ONLY FIGURE WHICH CAN ATTACK, WITHOUT BEING ATTACKED BACK.";
                break;
            case 14:
                var fig14 = Instantiate(N, new Vector3(0, 0, 0), Quaternion.identity);
                var fig14_2 = Instantiate(Q, new Vector3(2 * _positionScale, 1 * _positionScale, 0), Quaternion.identity);
                fig14.transform.tag = "Deletable";
                fig14_2.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                createCell(2, 1, 0, potentialCaptureCellMat);
                createCell(2, 1, 1, availableCellMat);
                createCell(2, 1, -1, availableCellMat);
                createCell(3, 1, 0, availableCellMat);
                createCell(2, 1, -1, availableCellMat);
                createCell(3, 1, 1, availableCellMat);
                createCell(3, 1, -1, availableCellMat);
                createCell(1, 1, 0, availableCellMat);
                createCell(1, 1, 1, availableCellMat);
                createCell(1, 1, -1, availableCellMat);
                createCell(2, 0, 1, availableCellMat);
                createCell(2, 0, -1, availableCellMat);
                createCell(3, 0, 0, availableCellMat);
                createCell(3, 0, 1, availableCellMat);
                createCell(3, 0, -1, availableCellMat);
                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(1, 0, 1, availableCellMat);
                createCell(1, 0, -1, availableCellMat);
                createCell(2, 2, 1, availableCellMat);
                createCell(2, 2, -1, availableCellMat);
                createCell(3, 2, 0, availableCellMat);
                createCell(3, 2, 1, availableCellMat);
                createCell(3, 2, -1, availableCellMat);
                createCell(1, 2, 0, availableCellMat);
                createCell(2, 2, 0, availableCellMat);
                createCell(1, 2, 1, availableCellMat);
                createCell(1, 2, -1, availableCellMat);

                createCell(0, 1, 0, availableCellMat);
                createCell(-1, 1, 0, availableCellMat);
                createCell(-2, 1, 0, availableCellMat);

                createCell(0, 1, 2, availableCellMat);
                createCell(-1, 1, 3, availableCellMat);
                createCell(-2, 1, 4, availableCellMat);

                createCell(0, 1, -2, availableCellMat);
                createCell(-1, 1, -3, availableCellMat);
                createCell(-2, 1, -4, availableCellMat);

                createCell(0, 3, 0, availableCellMat);
                createCell(-1, 4, 0, availableCellMat);
                createCell(-2, 5, 0, availableCellMat);

                createCell(0, 3, 2, availableCellMat);
                createCell(-1, 4, 3, availableCellMat);
                createCell(-2, 5, 4, availableCellMat);

                createCell(0, 3, -2, availableCellMat);
                createCell(-1, 4, -3, availableCellMat);
                createCell(-2, 5, -4, availableCellMat);

                createCell(0, -1, 0, availableCellMat);
                createCell(-1, -2, 0, availableCellMat);
                createCell(-2, -3, 0, availableCellMat);

                createCell(0, -1, 2, availableCellMat);
                createCell(-1, -2, 3, availableCellMat);
                createCell(-2, -3, 4, availableCellMat);

                createCell(0, -1, -2, availableCellMat);
                createCell(-1, -2, -3, availableCellMat);
                createCell(-2, -3, -4, availableCellMat);

                tutorialText.text = "THINGS REMAIN THE SAME IN SPACE CHESS.";
                break;
            case 15:
                var fig15 = Instantiate(B, new Vector3(0, 0, 0), Quaternion.identity);
                fig15.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);
                
                tutorialText.text = "THE BISHOP";
                break;
            case 16:
                var fig16 = Instantiate(B, new Vector3(0, 0, 0), Quaternion.identity);

                fig16.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                createCell(1, 0, 1, availableCellMat);
                createCell(1, 0, -1, availableCellMat);
                createCell(-1, 0, 1, availableCellMat);
                createCell(-1, 0, -1, availableCellMat);

                createCell(2, 0, 2, availableCellMat);
                createCell(2, 0, -2, availableCellMat);
                createCell(-2, 0, 2, availableCellMat);
                createCell(-2, 0, -2, availableCellMat);

                createCell(3, 0, 3, availableCellMat);
                createCell(3, 0, -3, availableCellMat);
                createCell(-3, 0, 3, availableCellMat);
                createCell(-3, 0, -3, availableCellMat);

                tutorialText.text = "THE BISHOP IN CHESS MOVES IN DIAGONAL...";
                break;
            case 17:
                var fig17 = Instantiate(B, new Vector3(0, 0, 0), Quaternion.identity);

                var fig17_1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig17_1.transform.position = new Vector3(0.55f, 0, 0.55f);
                fig17_1.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig17_1.GetComponent<Renderer>().material = indicatorMat;
                fig17_1.gameObject.tag = "Deletable";

                var fig17_2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig17_2.transform.position = new Vector3(0.55f, 0, -0.55f);
                fig17_2.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig17_2.GetComponent<Renderer>().material = indicatorMat;
                fig17_2.gameObject.tag = "Deletable";

                var fig17_3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig17_3.transform.position = new Vector3(-0.55f, 0, 0.55f);
                fig17_3.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig17_3.GetComponent<Renderer>().material = indicatorMat;
                fig17_3.gameObject.tag = "Deletable";

                var fig17_4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig17_4.transform.position = new Vector3(-0.55f, 0, -0.55f);
                fig17_4.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig17_4.GetComponent<Renderer>().material = indicatorMat;
                fig17_4.gameObject.tag = "Deletable";

                fig17.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                createCell(1, 0, 1, availableCellMat);
                createCell(1, 0, -1, availableCellMat);
                createCell(-1, 0, 1, availableCellMat);
                createCell(-1, 0, -1, availableCellMat);

                createCell(2, 0, 2, availableCellMat);
                createCell(2, 0, -2, availableCellMat);
                createCell(-2, 0, 2, availableCellMat);
                createCell(-2, 0, -2, availableCellMat);

                createCell(3, 0, 3, availableCellMat);
                createCell(3, 0, -3, availableCellMat);
                createCell(-3, 0, 3, availableCellMat);
                createCell(-3, 0, -3, availableCellMat);

                tutorialText.text = "...OR THROUGH THE CORNERS OF ITS SQUARE.";
                break;
            case 18:
                var fig18 = Instantiate(B, new Vector3(0, 0, 0), Quaternion.identity);
                fig18.transform.tag = "Deletable";

                var fig18_2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig18_2.transform.position = new Vector3(0, 0.5f, 0);
                fig18_2.transform.localScale = new Vector3(1f, 1f, 1f);
                fig18_2.GetComponent<Renderer>().material = currentCellMat;
                fig18_2.gameObject.tag = "Deletable";

                createCell(1, 0, 1, availableCellMat);
                createCell(1, 0, -1, availableCellMat);
                createCell(-1, 0, 1, availableCellMat);
                createCell(-1, 0, -1, availableCellMat);

                createCell(2, 0, 2, availableCellMat);
                createCell(2, 0, -2, availableCellMat);
                createCell(-2, 0, 2, availableCellMat);
                createCell(-2, 0, -2, availableCellMat);

                createCell(3, 0, 3, availableCellMat);
                createCell(3, 0, -3, availableCellMat);
                createCell(-3, 0, 3, availableCellMat);
                createCell(-3, 0, -3, availableCellMat);

                tutorialText.text = "IN SPACE CHESS, IF WE VISUALIZE THE BISHOP INSIDE A CUBE...";
                break;
            case 19:
                var fig19 = Instantiate(B, new Vector3(0, 0, 0), Quaternion.identity);
                fig19.transform.tag = "Deletable";

                var fig19_cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_cell.transform.position = new Vector3(0, 0.5f, 0);
                fig19_cell.transform.localScale = new Vector3(1f, 1f, 1f);
                fig19_cell.GetComponent<Renderer>().material = currentCellMat;
                fig19_cell.gameObject.tag = "Deletable";

                // Edges X
                var fig19_1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_1.transform.position = new Vector3(0, 0, 0.55f);
                fig19_1.transform.localScale = new Vector3(1f, 0.05f, 0.05f);
                fig19_1.GetComponent<Renderer>().material = indicatorMat;
                fig19_1.gameObject.tag = "Deletable";

                var fig19_2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_2.transform.position = new Vector3(0, 0, -0.55f);
                fig19_2.transform.localScale = new Vector3(1f, 0.05f, 0.05f);
                fig19_2.GetComponent<Renderer>().material = indicatorMat;
                fig19_2.gameObject.tag = "Deletable";

                var fig19_3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_3.transform.position = new Vector3(0, 1f, 0.55f);
                fig19_3.transform.localScale = new Vector3(1f, 0.05f, 0.05f);
                fig19_3.GetComponent<Renderer>().material = indicatorMat;
                fig19_3.gameObject.tag = "Deletable";

                var fig19_4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_4.transform.position = new Vector3(0, 1f, -0.55f);
                fig19_4.transform.localScale = new Vector3(1f, 0.05f, 0.05f);
                fig19_4.GetComponent<Renderer>().material = indicatorMat;
                fig19_4.gameObject.tag = "Deletable";

                // Edges Z
                var fig19_5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_5.transform.position = new Vector3(0.55f, 0, 0f);
                fig19_5.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
                fig19_5.GetComponent<Renderer>().material = indicatorMat;
                fig19_5.gameObject.tag = "Deletable";

                var fig19_6 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_6.transform.position = new Vector3(-0.55f, 0, 0);
                fig19_6.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
                fig19_6.GetComponent<Renderer>().material = indicatorMat;
                fig19_6.gameObject.tag = "Deletable";

                var fig19_7 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_7.transform.position = new Vector3(0.55f, 1f, 0);
                fig19_7.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
                fig19_7.GetComponent<Renderer>().material = indicatorMat;
                fig19_7.gameObject.tag = "Deletable";

                var fig19_8 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_8.transform.position = new Vector3(-0.55f, 1f, 0);
                fig19_8.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
                fig19_8.GetComponent<Renderer>().material = indicatorMat;
                fig19_8.gameObject.tag = "Deletable";

                // Edges Y
                var fig19_9 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_9.transform.position = new Vector3(0.55f, 0.5f, 0.55f);
                fig19_9.transform.localScale = new Vector3(0.05f, 1f, 0.05f);
                fig19_9.GetComponent<Renderer>().material = indicatorMat;
                fig19_9.gameObject.tag = "Deletable";

                var fig19_a = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_a.transform.position = new Vector3(0.55f, 0.5f, -0.55f);
                fig19_a.transform.localScale = new Vector3(0.05f, 1f, 0.05f);
                fig19_a.GetComponent<Renderer>().material = indicatorMat;
                fig19_a.gameObject.tag = "Deletable";

                var fig19_b = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_b.transform.position = new Vector3(-0.55f, 0.5f, 0.55f);
                fig19_b.transform.localScale = new Vector3(0.05f, 1f, 0.05f);
                fig19_b.GetComponent<Renderer>().material = indicatorMat;
                fig19_b.gameObject.tag = "Deletable";

                var fig19_c = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig19_c.transform.position = new Vector3(-0.55f, 0.5f, -0.55f);
                fig19_c.transform.localScale = new Vector3(0.05f, 1f, 0.05f);
                fig19_c.GetComponent<Renderer>().material = indicatorMat;
                fig19_c.gameObject.tag = "Deletable";

                // Y0
                createCell(1, 0, 1, availableCellMat);
                createCell(1, 0, -1, availableCellMat);
                createCell(-1, 0, 1, availableCellMat);
                createCell(-1, 0, -1, availableCellMat);

                createCell(2, 0, 2, availableCellMat);
                createCell(2, 0, -2, availableCellMat);
                createCell(-2, 0, 2, availableCellMat);
                createCell(-2, 0, -2, availableCellMat);

                createCell(3, 0, 3, availableCellMat);
                createCell(3, 0, -3, availableCellMat);
                createCell(-3, 0, 3, availableCellMat);
                createCell(-3, 0, -3, availableCellMat);

                //Y+
                createCell(1, 1, 0, availableCellMat);
                createCell(-1, 1, 0, availableCellMat);
                createCell(0, 1, -1, availableCellMat);
                createCell(0, 1, 1, availableCellMat);

                createCell(2, 2, 0, availableCellMat);
                createCell(-2, 2, 0, availableCellMat);
                createCell(0, 2, -2, availableCellMat);
                createCell(0, 2, 2, availableCellMat);

                createCell(3, 3, 0, availableCellMat);
                createCell(-3, 3, 0, availableCellMat);
                createCell(0, 3, -3, availableCellMat);
                createCell(0, 3, 3, availableCellMat);

                //Y-
                createCell(1, -1, 0, availableCellMat);
                createCell(-1, -1, 0, availableCellMat);
                createCell(0, -1, -1, availableCellMat);
                createCell(0, -1, 1, availableCellMat);

                createCell(2, -2, 0, availableCellMat);
                createCell(-2, -2, 0, availableCellMat);
                createCell(0, -2, -2, availableCellMat);
                createCell(0, -2, 2, availableCellMat);

                createCell(3, -3, 0, availableCellMat);
                createCell(-3, -3, 0, availableCellMat);
                createCell(0, -3, -3, availableCellMat);
                createCell(0, -3, 3, availableCellMat);

                tutorialText.text = "...IT WILL MOVE THROUGH THE CUBE'S EDGES.";
                break;
            case 20:
                var fig20 = Instantiate(B, new Vector3(0, 0, 0), Quaternion.identity);
                fig20.transform.tag = "Deletable";

                createCell(0, 0, 0, cellMatWhite);

                // Y0
                createCell(1, 0, 1, cellMatWhite);
                createCell(1, 0, -1, cellMatWhite);
                createCell(-1, 0, 1, cellMatWhite);
                createCell(-1, 0, -1, cellMatWhite);

                createCell(2, 0, 2, cellMatWhite);
                createCell(2, 0, -2, cellMatWhite);
                createCell(-2, 0, 2, cellMatWhite);
                createCell(-2, 0, -2, cellMatWhite);

                createCell(3, 0, 3, cellMatWhite);
                createCell(3, 0, -3, cellMatWhite);
                createCell(-3, 0, 3, cellMatWhite);
                createCell(-3, 0, -3, cellMatWhite);

                //Y+
                createCell(1, 1, 0, cellMatWhite);
                createCell(-1, 1, 0, cellMatWhite);
                createCell(0, 1, -1, cellMatWhite);
                createCell(0, 1, 1, cellMatWhite);

                createCell(2, 2, 0, cellMatWhite);
                createCell(-2, 2, 0, cellMatWhite);
                createCell(0, 2, -2, cellMatWhite);
                createCell(0, 2, 2, cellMatWhite);

                createCell(3, 3, 0, cellMatWhite);
                createCell(-3, 3, 0, cellMatWhite);
                createCell(0, 3, -3, cellMatWhite);
                createCell(0, 3, 3, cellMatWhite);

                //Y-
                createCell(1, -1, 0, cellMatWhite);
                createCell(-1, -1, 0, cellMatWhite);
                createCell(0, -1, -1, cellMatWhite);
                createCell(0, -1, 1, cellMatWhite);

                createCell(2, -2, 0, cellMatWhite);
                createCell(-2, -2, 0, cellMatWhite);
                createCell(0, -2, -2, cellMatWhite);
                createCell(0, -2, 2, cellMatWhite);

                createCell(3, -3, 0, cellMatWhite);
                createCell(-3, -3, 0, cellMatWhite);
                createCell(0, -3, -3, cellMatWhite);
                createCell(0, -3, 3, cellMatWhite);

                tutorialText.text = "ALSO, THE BISHOP DOESN'T CHANGE ITS CELL COLOR.";
                break;
            case 21:
                var fig21 = Instantiate(R, new Vector3(0, 0, 0), Quaternion.identity);
                fig21.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                tutorialText.text = "THE ROOK";
                break;
            case 22:
                var fig22 = Instantiate(R, new Vector3(0, 0, 0), Quaternion.identity);
                fig22.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                // EDGES
                var fig22_1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig22_1.transform.position = new Vector3(0, 0, 0.55f);
                fig22_1.transform.localScale = new Vector3(1f, 0.05f, 0.05f);
                fig22_1.GetComponent<Renderer>().material = indicatorMat;
                fig22_1.gameObject.tag = "Deletable";

                var fig22_2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig22_2.transform.position = new Vector3(0, 0, -0.55f);
                fig22_2.transform.localScale = new Vector3(1f, 0.05f, 0.05f);
                fig22_2.GetComponent<Renderer>().material = indicatorMat;
                fig22_2.gameObject.tag = "Deletable";

                var fig22_3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig22_3.transform.position = new Vector3(0.55f, 0, 0f);
                fig22_3.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
                fig22_3.GetComponent<Renderer>().material = indicatorMat;
                fig22_3.gameObject.tag = "Deletable";

                var fig22_4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig22_4.transform.position = new Vector3(-0.55f, 0, 0);
                fig22_4.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
                fig22_4.GetComponent<Renderer>().material = indicatorMat;
                fig22_4.gameObject.tag = "Deletable";

                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(3, 0, 0, availableCellMat);
                createCell(4, 0, 0, availableCellMat);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-3, 0, 0, availableCellMat);
                createCell(-4, 0, 0, availableCellMat);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(0, 0, 3, availableCellMat);
                createCell(0, 0, 4, availableCellMat);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(0, 0, -3, availableCellMat);
                createCell(0, 0, -4, availableCellMat);

                tutorialText.text = "IN CHESS, THE ROOK MOVES THROUGH THE EDGES OF ITS SQUARE.";
                break;
            case 23:
                var fig23 = Instantiate(R, new Vector3(0, 0, 0), Quaternion.identity);
                fig23.transform.tag = "Deletable";

                var fig23_cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig23_cell.transform.position = new Vector3(0, 0.5f, 0);
                fig23_cell.transform.localScale = new Vector3(1f, 1f, 1f);
                fig23_cell.GetComponent<Renderer>().material = currentCellMat;
                fig23_cell.gameObject.tag = "Deletable";

                // FACES
                var fig23_1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig23_1.transform.position = new Vector3(0, 0.5f, 0.55f);
                fig23_1.transform.localScale = new Vector3(0.4f, 0.4f, 0.01f);
                fig23_1.GetComponent<Renderer>().material = cubeFaceMat;
                fig23_1.gameObject.tag = "Deletable";

                var fig23_2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig23_2.transform.position = new Vector3(0, 0.5f, -0.55f);
                fig23_2.transform.localScale = new Vector3(0.4f, 0.4f, 0.01f);
                fig23_2.GetComponent<Renderer>().material = cubeFaceMat;
                fig23_2.gameObject.tag = "Deletable";

                var fig23_3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig23_3.transform.position = new Vector3(0.55f, 0.5f, 0);
                fig23_3.transform.localScale = new Vector3(0.01f, 0.4f, 0.4f);
                fig23_3.GetComponent<Renderer>().material = cubeFaceMat;
                fig23_3.gameObject.tag = "Deletable";

                var fig23_4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig23_4.transform.position = new Vector3(-0.55f, 0.5f, 0);
                fig23_4.transform.localScale = new Vector3(0.01f, 0.4f, 0.4f);
                fig23_4.GetComponent<Renderer>().material = cubeFaceMat;
                fig23_4.gameObject.tag = "Deletable";

                var fig23_5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig23_5.transform.position = new Vector3(0, 1.05f, 0);
                fig23_5.transform.localScale = new Vector3(0.4f, 0.01f, 0.4f);
                fig23_5.GetComponent<Renderer>().material = cubeFaceMat;
                fig23_5.gameObject.tag = "Deletable";

                var fig23_6 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig23_6.transform.position = new Vector3(0, -0.05f, 0);
                fig23_6.transform.localScale = new Vector3(0.4f, 0.01f, 0.4f);
                fig23_6.GetComponent<Renderer>().material = cubeFaceMat;
                fig23_6.gameObject.tag = "Deletable";

                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(3, 0, 0, availableCellMat);
                createCell(4, 0, 0, availableCellMat);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-3, 0, 0, availableCellMat);
                createCell(-4, 0, 0, availableCellMat);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(0, 0, 3, availableCellMat);
                createCell(0, 0, 4, availableCellMat);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(0, 0, -3, availableCellMat);
                createCell(0, 0, -4, availableCellMat);

                createCell(0, 1, 0, availableCellMat);
                createCell(0, 2, 0, availableCellMat);
                createCell(0, 3, 0, availableCellMat);
                createCell(0, 4, 0, availableCellMat);

                createCell(0, -1, 0, availableCellMat);
                createCell(0, -2, 0, availableCellMat);
                createCell(0, -3, 0, availableCellMat);
                createCell(0, -4, 0, availableCellMat);

                tutorialText.text = "IN SPACE CHESS, THE ROOK MOVES THROUGH THE FACES OF ITS CUBE.";
                break;
            case 24:
                var fig24 = Instantiate(Q, new Vector3(0, 0, 0), Quaternion.identity);
                fig24.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                tutorialText.text = "THE QUEEN";
                break;
            case 25:
                var fig25 = Instantiate(Q, new Vector3(0, 0, 0), Quaternion.identity);
                fig25.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                // ROOK
                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(3, 0, 0, availableCellMat);
                createCell(4, 0, 0, availableCellMat);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-3, 0, 0, availableCellMat);
                createCell(-4, 0, 0, availableCellMat);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(0, 0, 3, availableCellMat);
                createCell(0, 0, 4, availableCellMat);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(0, 0, -3, availableCellMat);
                createCell(0, 0, -4, availableCellMat);

                // BISHOP
                createCell(1, 0, 1, potentialCaptureCellMat);
                createCell(1, 0, -1, potentialCaptureCellMat);
                createCell(-1, 0, 1, potentialCaptureCellMat);
                createCell(-1, 0, -1, potentialCaptureCellMat);

                createCell(2, 0, 2, potentialCaptureCellMat);
                createCell(2, 0, -2, potentialCaptureCellMat);
                createCell(-2, 0, 2, potentialCaptureCellMat);
                createCell(-2, 0, -2, potentialCaptureCellMat);

                createCell(3, 0, 3, potentialCaptureCellMat);
                createCell(3, 0, -3, potentialCaptureCellMat);
                createCell(-3, 0, 3, potentialCaptureCellMat);
                createCell(-3, 0, -3, potentialCaptureCellMat);

                createCell(4, 0, 4, potentialCaptureCellMat);
                createCell(4, 0, -4, potentialCaptureCellMat);
                createCell(-4, 0, 4, potentialCaptureCellMat);
                createCell(-4, 0, -4, potentialCaptureCellMat);

                tutorialText.text = "IN CHESS, THE QUEEN COMBINES THE MOVES OF THE BISHOP AND THE ROOK.";
                break;
            case 26:
                var fig26 = Instantiate(Q, new Vector3(0, 0, 0), Quaternion.identity);
                fig26.transform.tag = "Deletable";

                var fig26_cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig26_cell.transform.position = new Vector3(0, 0.5f, 0);
                fig26_cell.transform.localScale = new Vector3(1f, 1f, 1f);
                fig26_cell.GetComponent<Renderer>().material = currentCellMat;
                fig26_cell.gameObject.tag = "Deletable";

                // ROOK
                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(3, 0, 0, availableCellMat);
                createCell(4, 0, 0, availableCellMat);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-3, 0, 0, availableCellMat);
                createCell(-4, 0, 0, availableCellMat);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(0, 0, 3, availableCellMat);
                createCell(0, 0, 4, availableCellMat);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(0, 0, -3, availableCellMat);
                createCell(0, 0, -4, availableCellMat);

                createCell(0, 1, 0, availableCellMat);
                createCell(0, 2, 0, availableCellMat);
                createCell(0, 3, 0, availableCellMat);
                createCell(0, 4, 0, availableCellMat);

                createCell(0, -1, 0, availableCellMat);
                createCell(0, -2, 0, availableCellMat);
                createCell(0, -3, 0, availableCellMat);
                createCell(0, -4, 0, availableCellMat);

                // BISHOP
                createCell(1, 0, 1, potentialCaptureCellMat);
                createCell(1, 0, -1, potentialCaptureCellMat);
                createCell(-1, 0, 1, potentialCaptureCellMat);
                createCell(-1, 0, -1, potentialCaptureCellMat);

                createCell(2, 0, 2, potentialCaptureCellMat);
                createCell(2, 0, -2, potentialCaptureCellMat);
                createCell(-2, 0, 2, potentialCaptureCellMat);
                createCell(-2, 0, -2, potentialCaptureCellMat);

                createCell(3, 0, 3, potentialCaptureCellMat);
                createCell(3, 0, -3, potentialCaptureCellMat);
                createCell(-3, 0, 3, potentialCaptureCellMat);
                createCell(-3, 0, -3, potentialCaptureCellMat);

                createCell(4, 0, 4, potentialCaptureCellMat);
                createCell(4, 0, -4, potentialCaptureCellMat);
                createCell(-4, 0, 4, potentialCaptureCellMat);
                createCell(-4, 0, -4, potentialCaptureCellMat);

                //Y+
                createCell(1, 1, 0, potentialCaptureCellMat);
                createCell(-1, 1, 0, potentialCaptureCellMat);
                createCell(0, 1, -1, potentialCaptureCellMat);
                createCell(0, 1, 1, potentialCaptureCellMat);

                createCell(2, 2, 0, potentialCaptureCellMat);
                createCell(-2, 2, 0, potentialCaptureCellMat);
                createCell(0, 2, -2, potentialCaptureCellMat);
                createCell(0, 2, 2, potentialCaptureCellMat);

                createCell(3, 3, 0, potentialCaptureCellMat);
                createCell(-3, 3, 0, potentialCaptureCellMat);
                createCell(0, 3, -3, potentialCaptureCellMat);
                createCell(0, 3, 3, potentialCaptureCellMat);

                createCell(4, 4, 0, potentialCaptureCellMat);
                createCell(-4, 4, 0, potentialCaptureCellMat);
                createCell(0, 4, -4, potentialCaptureCellMat);
                createCell(0, 4, 4, potentialCaptureCellMat);

                //Y-
                createCell(1, -1, 0, potentialCaptureCellMat);
                createCell(-1, -1, 0, potentialCaptureCellMat);
                createCell(0, -1, -1, potentialCaptureCellMat);
                createCell(0, -1, 1, potentialCaptureCellMat);

                createCell(2, -2, 0, potentialCaptureCellMat);
                createCell(-2, -2, 0, potentialCaptureCellMat);
                createCell(0, -2, -2, potentialCaptureCellMat);
                createCell(0, -2, 2, potentialCaptureCellMat);

                createCell(3, -3, 0, potentialCaptureCellMat);
                createCell(-3, -3, 0, potentialCaptureCellMat);
                createCell(0, -3, -3, potentialCaptureCellMat);
                createCell(0, -3, 3, potentialCaptureCellMat);

                createCell(4, -4, 0, potentialCaptureCellMat);
                createCell(-4, -4, 0, potentialCaptureCellMat);
                createCell(0, -4, -4, potentialCaptureCellMat);
                createCell(0, -4, 4, potentialCaptureCellMat);

                tutorialText.text = "IN SPACE CHESS IT IS DOING THE SAME...";
                break;
            case 27:
                var fig27 = Instantiate(Q, new Vector3(0, 0, 0), Quaternion.identity);
                fig27.transform.tag = "Deletable";

                var fig27_cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig27_cell.transform.position = new Vector3(0, 0.5f, 0);
                fig27_cell.transform.localScale = new Vector3(1f, 1f, 1f);
                fig27_cell.GetComponent<Renderer>().material = currentCellMat;
                fig27_cell.gameObject.tag = "Deletable";

                var fig27_1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig27_1.transform.position = new Vector3(0.55f, 0, 0.55f);
                fig27_1.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig27_1.GetComponent<Renderer>().material = cubeCornerMat;
                fig27_1.gameObject.tag = "Deletable";

                var fig27_2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig27_2.transform.position = new Vector3(0.55f, 0, -0.55f);
                fig27_2.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig27_2.GetComponent<Renderer>().material = cubeCornerMat;
                fig27_2.gameObject.tag = "Deletable";

                var fig27_3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig27_3.transform.position = new Vector3(-0.55f, 0, 0.55f);
                fig27_3.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig27_3.GetComponent<Renderer>().material = cubeCornerMat;
                fig27_3.gameObject.tag = "Deletable";

                var fig27_4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig27_4.transform.position = new Vector3(-0.55f, 0, -0.55f);
                fig27_4.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig27_4.GetComponent<Renderer>().material = cubeCornerMat;
                fig27_4.gameObject.tag = "Deletable";

                var fig27_5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig27_5.transform.position = new Vector3(0.55f, 1, 0.55f);
                fig27_5.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig27_5.GetComponent<Renderer>().material = cubeCornerMat;
                fig27_5.gameObject.tag = "Deletable";

                var fig27_6 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig27_6.transform.position = new Vector3(0.55f, 1, -0.55f);
                fig27_6.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig27_6.GetComponent<Renderer>().material = cubeCornerMat;
                fig27_6.gameObject.tag = "Deletable";

                var fig27_7 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig27_7.transform.position = new Vector3(-0.55f, 1, 0.55f);
                fig27_7.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig27_7.GetComponent<Renderer>().material = cubeCornerMat;
                fig27_7.gameObject.tag = "Deletable";

                var fig27_8 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fig27_8.transform.position = new Vector3(-0.55f, 1, -0.55f);
                fig27_8.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
                fig27_8.GetComponent<Renderer>().material = cubeCornerMat;
                fig27_8.gameObject.tag = "Deletable";

                // ROOK
                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(3, 0, 0, availableCellMat);
                createCell(4, 0, 0, availableCellMat);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-3, 0, 0, availableCellMat);
                createCell(-4, 0, 0, availableCellMat);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(0, 0, 3, availableCellMat);
                createCell(0, 0, 4, availableCellMat);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(0, 0, -3, availableCellMat);
                createCell(0, 0, -4, availableCellMat);

                createCell(0, 1, 0, availableCellMat);
                createCell(0, 2, 0, availableCellMat);
                createCell(0, 3, 0, availableCellMat);
                createCell(0, 4, 0, availableCellMat);

                createCell(0, -1, 0, availableCellMat);
                createCell(0, -2, 0, availableCellMat);
                createCell(0, -3, 0, availableCellMat);
                createCell(0, -4, 0, availableCellMat);

                // BISHOP
                createCell(1, 0, 1, availableCellMat);
                createCell(1, 0, -1, availableCellMat);
                createCell(-1, 0, 1, availableCellMat);
                createCell(-1, 0, -1, availableCellMat);

                createCell(2, 0, 2, availableCellMat);
                createCell(2, 0, -2, availableCellMat);
                createCell(-2, 0, 2, availableCellMat);
                createCell(-2, 0, -2, availableCellMat);

                createCell(3, 0, 3, availableCellMat);
                createCell(3, 0, -3, availableCellMat);
                createCell(-3, 0, 3, availableCellMat);
                createCell(-3, 0, -3, availableCellMat);

                createCell(4, 0, 4, availableCellMat);
                createCell(4, 0, -4, availableCellMat);
                createCell(-4, 0, 4, availableCellMat);
                createCell(-4, 0, -4, availableCellMat);

                //Y+
                createCell(1, 1, 0, availableCellMat);
                createCell(-1, 1, 0, availableCellMat);
                createCell(0, 1, -1, availableCellMat);
                createCell(0, 1, 1, availableCellMat);

                createCell(2, 2, 0, availableCellMat);
                createCell(-2, 2, 0, availableCellMat);
                createCell(0, 2, -2, availableCellMat);
                createCell(0, 2, 2, availableCellMat);

                createCell(3, 3, 0, availableCellMat);
                createCell(-3, 3, 0, availableCellMat);
                createCell(0, 3, -3, availableCellMat);
                createCell(0, 3, 3, availableCellMat);

                createCell(4, 4, 0, availableCellMat);
                createCell(-4, 4, 0, availableCellMat);
                createCell(0, 4, -4, availableCellMat);
                createCell(0, 4, 4, availableCellMat);

                //Y-
                createCell(1, -1, 0, availableCellMat);
                createCell(-1, -1, 0, availableCellMat);
                createCell(0, -1, -1, availableCellMat);
                createCell(0, -1, 1, availableCellMat);

                createCell(2, -2, 0, availableCellMat);
                createCell(-2, -2, 0, availableCellMat);
                createCell(0, -2, -2, availableCellMat);
                createCell(0, -2, 2, availableCellMat);

                createCell(3, -3, 0, availableCellMat);
                createCell(-3, -3, 0, availableCellMat);
                createCell(0, -3, -3, availableCellMat);
                createCell(0, -3, 3, availableCellMat);

                createCell(4, -4, 0, availableCellMat);
                createCell(-4, -4, 0, availableCellMat);
                createCell(0, -4, -4, availableCellMat);
                createCell(0, -4, 4, availableCellMat);

                // CORNERS
                //Y+
                createCell(1, 1, 1, potentialCaptureCellMat);
                createCell(-1, 1, 1, potentialCaptureCellMat);
                createCell(1, 1, -1, potentialCaptureCellMat);
                createCell(-1, 1, -1, potentialCaptureCellMat);

                createCell(2, 2, 2, potentialCaptureCellMat);
                createCell(-2, 2, 2, potentialCaptureCellMat);
                createCell(2, 2, -2, potentialCaptureCellMat);
                createCell(-2, 2, -2, potentialCaptureCellMat);

                createCell(3, 3, 3, potentialCaptureCellMat);
                createCell(-3, 3, 3, potentialCaptureCellMat);
                createCell(3, 3, -3, potentialCaptureCellMat);
                createCell(-3, 3, -3, potentialCaptureCellMat);

                createCell(4, 4, 4, potentialCaptureCellMat);
                createCell(-4, 4, 4, potentialCaptureCellMat);
                createCell(4, 4, -4, potentialCaptureCellMat);
                createCell(-4, 4, -4, potentialCaptureCellMat);

                //Y-
                createCell(1, -1, 1, potentialCaptureCellMat);
                createCell(-1, -1, 1, potentialCaptureCellMat);
                createCell(1, -1, -1, potentialCaptureCellMat);
                createCell(-1, -1, -1, potentialCaptureCellMat);

                createCell(2, -2, 2, potentialCaptureCellMat);
                createCell(-2, -2, 2, potentialCaptureCellMat);
                createCell(2, -2, -2, potentialCaptureCellMat);
                createCell(-2, -2, -2, potentialCaptureCellMat);

                createCell(3, -3, 3, potentialCaptureCellMat);
                createCell(-3, -3, 3, potentialCaptureCellMat);
                createCell(3, -3, -3, potentialCaptureCellMat);
                createCell(-3, -3, -3, potentialCaptureCellMat);

                createCell(4, -4, 4, potentialCaptureCellMat);
                createCell(-4, -4, 4, potentialCaptureCellMat);
                createCell(4, -4, -4, potentialCaptureCellMat);
                createCell(-4, -4, -4, potentialCaptureCellMat);

                tutorialText.text = "...AND ON TOP OF THAT, IT CAN MOVE THROUGH ITS CUBE'S CORNERS.";
                break;
            case 28:
                var fig28 = Instantiate(Q, new Vector3(0, 0, 0), Quaternion.identity);
                fig28.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                // ROOK
                createCell(1, 0, 0, availableCellMat);
                createCell(2, 0, 0, availableCellMat);
                createCell(3, 0, 0, availableCellMat);
                createCell(4, 0, 0, availableCellMat);

                createCell(-1, 0, 0, availableCellMat);
                createCell(-2, 0, 0, availableCellMat);
                createCell(-3, 0, 0, availableCellMat);
                createCell(-4, 0, 0, availableCellMat);

                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, 2, availableCellMat);
                createCell(0, 0, 3, availableCellMat);
                createCell(0, 0, 4, availableCellMat);

                createCell(0, 0, -1, availableCellMat);
                createCell(0, 0, -2, availableCellMat);
                createCell(0, 0, -3, availableCellMat);
                createCell(0, 0, -4, availableCellMat);

                createCell(0, 1, 0, availableCellMat);
                createCell(0, 2, 0, availableCellMat);
                createCell(0, 3, 0, availableCellMat);
                createCell(0, 4, 0, availableCellMat);

                createCell(0, -1, 0, availableCellMat);
                createCell(0, -2, 0, availableCellMat);
                createCell(0, -3, 0, availableCellMat);
                createCell(0, -4, 0, availableCellMat);

                // BISHOP
                createCell(1, 0, 1, availableCellMat);
                createCell(1, 0, -1, availableCellMat);
                createCell(-1, 0, 1, availableCellMat);
                createCell(-1, 0, -1, availableCellMat);

                createCell(2, 0, 2, availableCellMat);
                createCell(2, 0, -2, availableCellMat);
                createCell(-2, 0, 2, availableCellMat);
                createCell(-2, 0, -2, availableCellMat);

                createCell(3, 0, 3, availableCellMat);
                createCell(3, 0, -3, availableCellMat);
                createCell(-3, 0, 3, availableCellMat);
                createCell(-3, 0, -3, availableCellMat);

                createCell(4, 0, 4, availableCellMat);
                createCell(4, 0, -4, availableCellMat);
                createCell(-4, 0, 4, availableCellMat);
                createCell(-4, 0, -4, availableCellMat);

                //Y+
                createCell(1, 1, 0, availableCellMat);
                createCell(-1, 1, 0, availableCellMat);
                createCell(0, 1, -1, availableCellMat);
                createCell(0, 1, 1, availableCellMat);

                createCell(2, 2, 0, availableCellMat);
                createCell(-2, 2, 0, availableCellMat);
                createCell(0, 2, -2, availableCellMat);
                createCell(0, 2, 2, availableCellMat);

                createCell(3, 3, 0, availableCellMat);
                createCell(-3, 3, 0, availableCellMat);
                createCell(0, 3, -3, availableCellMat);
                createCell(0, 3, 3, availableCellMat);

                createCell(4, 4, 0, availableCellMat);
                createCell(-4, 4, 0, availableCellMat);
                createCell(0, 4, -4, availableCellMat);
                createCell(0, 4, 4, availableCellMat);

                //Y-
                createCell(1, -1, 0, availableCellMat);
                createCell(-1, -1, 0, availableCellMat);
                createCell(0, -1, -1, availableCellMat);
                createCell(0, -1, 1, availableCellMat);

                createCell(2, -2, 0, availableCellMat);
                createCell(-2, -2, 0, availableCellMat);
                createCell(0, -2, -2, availableCellMat);
                createCell(0, -2, 2, availableCellMat);

                createCell(3, -3, 0, availableCellMat);
                createCell(-3, -3, 0, availableCellMat);
                createCell(0, -3, -3, availableCellMat);
                createCell(0, -3, 3, availableCellMat);

                createCell(4, -4, 0, availableCellMat);
                createCell(-4, -4, 0, availableCellMat);
                createCell(0, -4, -4, availableCellMat);
                createCell(0, -4, 4, availableCellMat);

                // CORNERS
                //Y+
                createCell(1, 1, 1, availableCellMat);
                createCell(-1, 1, 1, availableCellMat);
                createCell(1, 1, -1, availableCellMat);
                createCell(-1, 1, -1, availableCellMat);

                createCell(2, 2, 2, availableCellMat);
                createCell(-2, 2, 2, availableCellMat);
                createCell(2, 2, -2, availableCellMat);
                createCell(-2, 2, -2, availableCellMat);

                createCell(3, 3, 3, availableCellMat);
                createCell(-3, 3, 3, availableCellMat);
                createCell(3, 3, -3, availableCellMat);
                createCell(-3, 3, -3, availableCellMat);

                createCell(4, 4, 4, availableCellMat);
                createCell(-4, 4, 4, availableCellMat);
                createCell(4, 4, -4, availableCellMat);
                createCell(-4, 4, -4, availableCellMat);

                //Y-
                createCell(1, -1, 1, availableCellMat);
                createCell(-1, -1, 1, availableCellMat);
                createCell(1, -1, -1, availableCellMat);
                createCell(-1, -1, -1, availableCellMat);

                createCell(2, -2, 2, availableCellMat);
                createCell(-2, -2, 2, availableCellMat);
                createCell(2, -2, -2, availableCellMat);
                createCell(-2, -2, -2, availableCellMat);

                createCell(3, -3, 3, availableCellMat);
                createCell(-3, -3, 3, availableCellMat);
                createCell(3, -3, -3, availableCellMat);
                createCell(-3, -3, -3, availableCellMat);

                createCell(4, -4, 4, availableCellMat);
                createCell(-4, -4, 4, availableCellMat);
                createCell(4, -4, -4, availableCellMat);
                createCell(-4, -4, -4, availableCellMat);

                tutorialText.text = "TAKE A MOMENT AND LOOK AT QUEEN'S MOVE PATTERN. IT IS THE MOST COMPLEX ONE.";
                break;
            case 29:
                var fig29 = Instantiate(K, new Vector3(0, 0, 0), Quaternion.identity);
                fig29.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);
                
                tutorialText.text = "THE KING";
                break;
            case 30:
                var fig30 = Instantiate(K, new Vector3(0, 0, 0), Quaternion.identity);
                fig30.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);

                // ROOK
                createCell(1, 0, 0, availableCellMat);
                createCell(-1, 0, 0, availableCellMat);
                createCell(0, 0, 1, availableCellMat);   
                createCell(0, 0, -1, availableCellMat);
                createCell(0, 1, 0, availableCellMat);
                createCell(0, -1, 0, availableCellMat);


                // BISHOP
                createCell(1, 0, 1, availableCellMat);
                createCell(1, 0, -1, availableCellMat);
                createCell(-1, 0, 1, availableCellMat);
                createCell(-1, 0, -1, availableCellMat);

                //Y+
                createCell(1, 1, 0, availableCellMat);
                createCell(-1, 1, 0, availableCellMat);
                createCell(0, 1, -1, availableCellMat);
                createCell(0, 1, 1, availableCellMat);

                //Y-
                createCell(1, -1, 0, availableCellMat);
                createCell(-1, -1, 0, availableCellMat);
                createCell(0, -1, -1, availableCellMat);
                createCell(0, -1, 1, availableCellMat);

                // CORNERS
                //Y+
                createCell(1, 1, 1, availableCellMat);
                createCell(-1, 1, 1, availableCellMat);
                createCell(1, 1, -1, availableCellMat);
                createCell(-1, 1, -1, availableCellMat);

                //Y-
                createCell(1, -1, 1, availableCellMat);
                createCell(-1, -1, 1, availableCellMat);
                createCell(1, -1, -1, availableCellMat);
                createCell(-1, -1, -1, availableCellMat);

                tutorialText.text = "THE KING MOVES LIKE THE QUEEN, JUST ONE CELL AT A TIME.";
                break;
            case 31:
                var fig31_K = Instantiate(K, new Vector3(0, 0, 0), Quaternion.identity);
                var fig31_b = Instantiate(b, new Vector3(2 * _positionScale, 0, 1 * _positionScale), Quaternion.identity);
                fig31_K.transform.tag = "Deletable";
                fig31_b.transform.tag = "Deletable";

                createCell(0, 0, 0, currentCellMat);
                createCell(2, 0, 1, cellMatWhite);

                // ROOK
                createCell(1, 0, 0, potentialCaptureCellMat);
                createCell(-1, 0, 0, availableCellMat);
                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, -1, potentialCaptureCellMat);
                createCell(0, 1, 0, availableCellMat);
                createCell(0, -1, 0, availableCellMat);

                // BISHOP
                createCell(1, 0, 1, availableCellMat);
                createCell(1, 0, -1, availableCellMat);
                createCell(-1, 0, 1, availableCellMat);
                createCell(-1, 0, -1, availableCellMat);

                //Y+
                createCell(1, 1, 0, availableCellMat);
                createCell(-1, 1, 0, availableCellMat);
                createCell(0, 1, -1, availableCellMat);
                createCell(0, 1, 1, availableCellMat);

                //Y-
                createCell(1, -1, 0, availableCellMat);
                createCell(-1, -1, 0, availableCellMat);
                createCell(0, -1, -1, availableCellMat);
                createCell(0, -1, 1, availableCellMat);

                // CORNERS
                //Y+
                createCell(1, 1, 1, potentialCaptureCellMat);
                createCell(-1, 1, 1, availableCellMat);
                createCell(1, 1, -1, availableCellMat);
                createCell(-1, 1, -1, availableCellMat);

                //Y-
                createCell(1, -1, 1, potentialCaptureCellMat);
                createCell(-1, -1, 1, availableCellMat);
                createCell(1, -1, -1, availableCellMat);
                createCell(-1, -1, -1, availableCellMat);

                tutorialText.text = "THE KING CANNOT MOVE ON A CELL THAT IS ATTACKED BY THE OPPONENT.";
                break;
            case 32:
                var fig32_K = Instantiate(K, new Vector3(0, 0, 0), Quaternion.identity);
                var fig32_b = Instantiate(b, new Vector3(2 * _positionScale, 0, 2 * _positionScale), Quaternion.identity);
                fig32_K.transform.tag = "Deletable";
                fig32_b.transform.tag = "Deletable";

                fig32_K.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

                createCell(0, 0, 0, potentialCaptureCellMat);
                createCell(2, 0, 2, currentCellMat);
                createCell(3, 0, 1, currentCellMat);

                // ROOK
                createCell(1, 0, 0, availableCellMat);
                createCell(-1, 0, 0, availableCellMat);
                createCell(0, 0, 1, availableCellMat);
                createCell(0, 0, -1, availableCellMat);
                createCell(0, 1, 0, availableCellMat);
                createCell(0, -1, 0, availableCellMat);

                // BISHOP
                createCell(1, 0, 1, potentialCaptureCellMat);
                createCell(1, 0, -1, availableCellMat);
                createCell(-1, 0, 1, availableCellMat);
                createCell(-1, 0, -1, potentialCaptureCellMat);

                //Y+
                createCell(1, 1, 0, availableCellMat);
                createCell(-1, 1, 0, availableCellMat);
                createCell(0, 1, -1, availableCellMat);
                createCell(0, 1, 1, availableCellMat);

                //Y-
                createCell(1, -1, 0, availableCellMat);
                createCell(-1, -1, 0, availableCellMat);
                createCell(0, -1, -1, availableCellMat);
                createCell(0, -1, 1, availableCellMat);

                // CORNERS
                //Y+
                createCell(1, 1, 1, availableCellMat);
                createCell(-1, 1, 1, availableCellMat);
                createCell(1, 1, -1, availableCellMat);
                createCell(-1, 1, -1, availableCellMat);

                //Y-
                createCell(1, -1, 1, availableCellMat);
                createCell(-1, -1, 1, availableCellMat);
                createCell(1, -1, -1, availableCellMat);
                createCell(-1, -1, -1, availableCellMat);

                tutorialText.text = "IF THE KING IS ON A CELL ATTACKED BY THE OPPONENT, THEN THE KING IS IN CHECK. THE CHECK MUST BE AVOIDED IMMEDIATELY.";
                break;
            case 33:
                var fig33_K = Instantiate(K, new Vector3(0, 0, 0), Quaternion.identity);
                var fig33_q = Instantiate(q, new Vector3(_positionScale, 0, 0), Quaternion.identity);
                var fig33_b = Instantiate(b, new Vector3(2 * _positionScale, 0, _positionScale), Quaternion.identity);
                fig33_K.transform.tag = "Deletable";
                fig33_q.transform.tag = "Deletable";
                fig33_b.transform.tag = "Deletable";

                fig33_K.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

                createCell(0, 0, 0, potentialCaptureCellMat);
                createCell(1, 0, 0, currentCellMat);
                createCell(2, 0, 1, cellMatWhite);

                // ROOK
                createCell(0, 0, 1, potentialCaptureCellMat);
                createCell(0, 0, -1, potentialCaptureCellMat);
                createCell(0, 1, 0, potentialCaptureCellMat);
                createCell(0, -1, 0, potentialCaptureCellMat);

                // BISHOP
                createCell(1, 0, 1, potentialCaptureCellMat);
                createCell(1, 0, -1, potentialCaptureCellMat);

                //Y+
                createCell(1, 1, 0, potentialCaptureCellMat);
                createCell(0, 1, -1, potentialCaptureCellMat);
                createCell(0, 1, 1, potentialCaptureCellMat);

                //Y-
                createCell(1, -1, 0, potentialCaptureCellMat);
                createCell(0, -1, -1, potentialCaptureCellMat);
                createCell(0, -1, 1, potentialCaptureCellMat);

                // CORNERS
                //Y+
                createCell(1, 1, 1, potentialCaptureCellMat);
                createCell(1, 1, -1, potentialCaptureCellMat);

                //Y-
                createCell(1, -1, 1, potentialCaptureCellMat);
                createCell(1, -1, -1, potentialCaptureCellMat);

                tutorialText.text = "IF THE KING IS IN CHECK AND HAS NO AVAILABLE CELLS, THEN THE KING IS IN CHECKMATE.";
                break;
            default:
                break;
        }
    }

    public void createCell(int x, int y, int z, Material mat) {
        var cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cell.transform.position = new Vector3(x * _positionScale, y * _positionScale, z * _positionScale);
        cell.transform.localScale = new Vector3(1f, 0.05f, 1f);
        cell.GetComponent<Renderer>().material = mat;
        cell.gameObject.tag = "Deletable";
    }
}
