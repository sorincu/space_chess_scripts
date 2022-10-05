using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellProperties : MonoBehaviour {
    public Material originalMat;
    public Material availableCellMat;
    public Material currentCellMat;
    public Material potentialCaptureCellMat;

    private string availableCellTag = "AvailableCell";
    private string potentialCaptureCellTag = "PotentialCaptureCell";

    public bool isAttackedByWhite = false;
    public bool isAttackedByBlack = false;

    // Dependent on variables of other classes
    public GameObject[,,,] spaceGrid;
    private int _cX, _cY, _cZ;
    private float _positionScale;

    void Start() {
        _cX = (int)GridSizeController.xSize.value;
        _cY = (int)GridSizeController.ySize.value;
        _cZ = (int)GridSizeController.zSize.value;

        _positionScale = GridInitializationGame.positionScale;
        spaceGrid = GridInitializationGame.spaceGrid;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f)) {
                var selection = hit.transform;

                if (selection.CompareTag(availableCellTag) || selection.CompareTag(potentialCaptureCellTag)) {
                    GameObject activeFigure = GameObject.Find("Grid").GetComponent<ActiveFigure>().activeFigure;
                    var playersTurn = GameObject.Find("Camera").GetComponent<SelectionManager>().playersTurn;

                    var prevcX = Convert.ToInt32(activeFigure.transform.position.x / _positionScale);
                    var prevcY = Convert.ToInt32(activeFigure.transform.position.y / _positionScale);
                    var prevcZ = Convert.ToInt32(activeFigure.transform.position.z / _positionScale);

                    var currentX = Convert.ToInt32(hit.transform.position.x / _positionScale);
                    var currentY = Convert.ToInt32(hit.transform.position.y / _positionScale);
                    var currentZ = Convert.ToInt32(hit.transform.position.z / _positionScale);

                    activeFigure.transform.position = hit.transform.position;

                    spaceGrid[prevcX, prevcZ, prevcY, 1] = null;

                    if (selection.CompareTag(potentialCaptureCellTag))
                        Destroy(spaceGrid[currentX, currentZ, currentY, 1]);

                    spaceGrid[currentX, currentZ, currentY, 1] = activeFigure;

                    // Clear cell properties
                    clearCellProperties();

                    // Check for pawn promotion
                    if (activeFigure.name[1] == 'P') {
                        if (activeFigure.name[0] == 'w' && currentZ == _cZ - 1) {
                            Destroy(spaceGrid[currentX, currentZ, currentY, 1]);
                            spaceGrid[currentX, currentZ, currentY, 1] = 
                                Instantiate( GameObject.Find("Grid").GetComponent<GridInitializationGame>().Q,
                                new Vector3(currentX * _positionScale, currentY * _positionScale, currentZ * _positionScale),
                                Quaternion.identity);
                        } else  if (currentZ == 0) {
                            Destroy(spaceGrid[currentX, currentZ, currentY, 1]);
                            spaceGrid[currentX, currentZ, currentY, 1] =
                                Instantiate(GameObject.Find("Grid").GetComponent<GridInitializationGame>().q,
                                new Vector3(currentX * _positionScale, currentY * _positionScale, currentZ * _positionScale),
                                Quaternion.identity);
                        }
                    }

                    // Determine attacking space
                    getAttackingSpace('w');
                    getAttackingSpace('b');

                    // Track king position
                    if (activeFigure.name[1] == 'K') {
                        if (activeFigure.name[0] == 'w') {
                            GameObject.Find("Grid").GetComponent<KingData>().setwKPosition(currentX, currentZ, currentY);
                        } else {
                            GameObject.Find("Grid").GetComponent<KingData>().setbKPosition(currentX, currentZ, currentY);
                        }
                    }

                    // Verify check
                    GameObject.Find("Grid").GetComponent<KingData>().verifyIfKingIsInCheck();

                    verifyIfGameOver(playersTurn);

                    selection.gameObject.GetComponent<Renderer>().material = currentCellMat;
                    spaceGrid[prevcX, prevcZ, prevcY, 0].GetComponent<Renderer>().material = currentCellMat;

                    if (playersTurn == 'w') {
                        GameObject.Find("Camera").GetComponent<SelectionManager>().playersTurn = 'b'; 
                    } else {
                        GameObject.Find("Camera").GetComponent<SelectionManager>().playersTurn = 'w';
                    }
                }
            }
        }
    }

    public void availableOn() {
        this.GetComponent<Renderer>().material = availableCellMat;
        this.tag = availableCellTag;
    }

    public void availableOff() {
        this.GetComponent<Renderer>().material = originalMat;
        this.tag = "Untagged";
    }

    public void potentialCaptureOn() {
        this.GetComponent<Renderer>().material = potentialCaptureCellMat;
        this.tag = potentialCaptureCellTag;
    }

    public void kingPotentialCaptureOn() {
        this.GetComponent<Renderer>().material = potentialCaptureCellMat;
    }

    // ---------------------------------------------------------------------------------------------------
    //  Class methods

    public void getAttackingSpace(char playersTurn) {
        for (int y = 0; y < _cY; y++) {
            for (int z = 0; z < _cZ; z++) {
                for (int x = 0; x < _cX; x++) {
                    try {
                        if (spaceGrid[x, z, y, 1].name[0] == playersTurn) {
                            switch (spaceGrid[x, z, y, 1].name[1]) {
                                case 'P':
                                    if (playersTurn == 'w') showWhitePawnAttacks(x, z, y);
                                    else if (playersTurn == 'b') showBlackPawnAttacks(x, z, y);
                                    break;
                                case 'N':
                                    showKnightAttacks(x, z, y, playersTurn);
                                    break;
                                case 'B':
                                    showBishopAttacks(x, z, y, playersTurn);
                                    break;
                                case 'R':
                                    showRookAttacks(x, z, y, playersTurn);
                                    break;
                                case 'Q':
                                    showBishopAttacks(x, z, y, playersTurn);
                                    showRookAttacks(x, z, y, playersTurn);
                                    showQueenAttacks(x, z, y, playersTurn);
                                    break;
                                case 'K':
                                    showKingAttacks(x, z, y, playersTurn);
                                    break;
                                default: break;
                            }
                        }
                    } catch (Exception e) { }
                }
            }
        }
    }

    public void clearCellProperties() {
        for (var y = 0; y < _cY; y++) {
            for (var z = 0; z < _cZ; z++) {
                for (var x = 0; x < _cX; x++) {
                    spaceGrid[x, z, y, 0].GetComponent<CellProperties>().availableOff();
                    spaceGrid[x, z, y, 0].GetComponent<CellProperties>().isAttackedByWhite = false;
                    spaceGrid[x, z, y, 0].GetComponent<CellProperties>().isAttackedByBlack = false;
                }
            }
        }
    }

    public void allAvailableOff() {
        for (var y = 0; y < _cY; y++) {
            for (var z = 0; z < _cZ; z++) {
                for (var x = 0; x < _cX; x++) {
                    spaceGrid[x, z, y, 0].GetComponent<CellProperties>().availableOff();
                }
            }
        }
    }

    public void clearAttackingSpace() {
        for (var y = 0; y < _cY; y++) {
            for (var z = 0; z < _cZ; z++) {
                for (var x = 0; x < _cX; x++) {
                    spaceGrid[x, z, y, 0].GetComponent<CellProperties>().isAttackedByWhite = false;
                    spaceGrid[x, z, y, 0].GetComponent<CellProperties>().isAttackedByBlack = false;
                }
            }
        }
    }

    public void verifyIfGameOver(char playersTurn) {
        if (playersTurn == 'w') {
            GameObject.Find("Camera").GetComponent<SelectionManager>().showKingMoves(
                GameObject.Find("Grid").GetComponent<KingData>().bK_PosX,
                GameObject.Find("Grid").GetComponent<KingData>().bK_PosZ,
                GameObject.Find("Grid").GetComponent<KingData>().bK_PosY);

            if (GameObject.Find("Camera").GetComponent<SelectionManager>().kingsAvailableMoves == 0) {
                for (var y = 0; y < _cY; y++) {
                    for (var z = 0; z < _cZ; z++) {
                        for (var x = 0; x < _cX; x++) {
                            if (spaceGrid[x, z, y, 1] != null && spaceGrid[x, z, y, 1].name[0] == 'b') {
                                switch (spaceGrid[x, z, y, 1].name[1]) {
                                    case 'P':
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showBlackPawnMoves(x, z, y);
                                        break;
                                    case 'N':
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showKnightMoves(x, z, y);
                                        break;
                                    case 'B':
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showBishopMoves(x, z, y);
                                        break;
                                    case 'R':
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showRookMoves(x, z, y);
                                        break;
                                    case 'Q':
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showBishopMoves(x, z, y);
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showRookMoves(x, z, y);
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showQueenMoves(x, z, y);
                                        break;
                                    default: break;
                                }
                            }
                        }
                    }
                }

                if (GameObject.Find("Camera").GetComponent<SelectionManager>().allAvailableMoves == 0) {
                    if (GameObject.Find("Grid").GetComponent<KingData>().bK_isInCheck) {
                        GameObject.Find("Camera").GetComponent<SelectionManager>().resolutionMsg.text = "CHECKMATE !";
                        GameObject.Find("Camera").GetComponent<SelectionManager>().winnerMsg.text = "WHITE WINS";
                        disableActiveComponents();
                    } else {
                        GameObject.Find("Camera").GetComponent<SelectionManager>().resolutionMsg.text = "STALEMATE !";
                        GameObject.Find("Camera").GetComponent<SelectionManager>().winnerMsg.text = "BLACK WINS";
                        disableActiveComponents();
                    }
                }
            }

            allAvailableOff();
        } else if (playersTurn == 'b') {
            GameObject.Find("Camera").GetComponent<SelectionManager>().showKingMoves(
                GameObject.Find("Grid").GetComponent<KingData>().wK_PosX,
                GameObject.Find("Grid").GetComponent<KingData>().wK_PosZ,
                GameObject.Find("Grid").GetComponent<KingData>().wK_PosY);

            if (GameObject.Find("Camera").GetComponent<SelectionManager>().kingsAvailableMoves == 0) {
                for (var y = 0; y < _cY; y++) {
                    for (var z = 0; z < _cZ; z++) {
                        for (var x = 0; x < _cX; x++) {
                            if (spaceGrid[x, z, y, 1] != null && spaceGrid[x, z, y, 1].name[0] == 'w') {
                                switch (spaceGrid[x, z, y, 1].name[1]) {
                                    case 'P':
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showWhitePawnMoves(x, z, y);
                                        break;
                                    case 'N':
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showKnightMoves(x, z, y);
                                        break;
                                    case 'B':
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showBishopMoves(x, z, y);
                                        break;
                                    case 'R':
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showRookMoves(x, z, y);
                                        break;
                                    case 'Q':
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showBishopMoves(x, z, y);
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showRookMoves(x, z, y);
                                        GameObject.Find("Camera").GetComponent<SelectionManager>().showQueenMoves(x, z, y);
                                        break;
                                    default: break;
                                }
                            }
                        }
                    }
                }

                if (GameObject.Find("Camera").GetComponent<SelectionManager>().allAvailableMoves == 0) {
                    if (GameObject.Find("Grid").GetComponent<KingData>().wK_isInCheck) {
                        GameObject.Find("Camera").GetComponent<SelectionManager>().resolutionMsg.text = "CHECKMATE !";
                        GameObject.Find("Camera").GetComponent<SelectionManager>().winnerMsg.text = "BLACK WINS";
                        disableActiveComponents();
                    } else {
                        GameObject.Find("Camera").GetComponent<SelectionManager>().resolutionMsg.text = "STALEMATE !";
                        GameObject.Find("Camera").GetComponent<SelectionManager>().winnerMsg.text = "WHITE WINS";
                        disableActiveComponents();
                    }
                }
            }

            allAvailableOff();
        }

        GameObject.Find("Camera").GetComponent<SelectionManager>().kingsAvailableMoves = 0;
        GameObject.Find("Camera").GetComponent<SelectionManager>().allAvailableMoves = 0;
    }

    public void disableActiveComponents() {
        GameObject.Find("Camera").GetComponent<SelectionManager>().gameOverMenu.SetActive(true);
        GameObject.Find("Camera").GetComponent<SelectionManager>().enabled = false;
        GameObject.Find("FocusObject").GetComponent<CameraOrbitKeyboard>().enabled = false;
        GameObject.Find("Camera").GetComponent<CameraZoom>().enabled = false;
    }

    // ---------------------------------------------------------------------------------------------------
    //  Figure functionality for attacking space

    public void showWhitePawnAttacks(int x, int z, int y) {
        try {
            spaceGrid[x + 1, z + 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
        } catch (Exception e) { }

        try {
            spaceGrid[x + 1, z + 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
        } catch (Exception e) { }

        try {
            spaceGrid[x + 1, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
        } catch (Exception e) { }

        try {
            spaceGrid[x - 1, z + 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
        } catch (Exception e) { }

        try {
            spaceGrid[x - 1, z + 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
        } catch (Exception e) { }

        try {
            spaceGrid[x - 1, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
        } catch (Exception e) { }
    }

    public void showBlackPawnAttacks(int x, int z, int y) {
        try {
            spaceGrid[x + 1, z - 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            spaceGrid[x + 1, z - 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            spaceGrid[x + 1, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            spaceGrid[x - 1, z - 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            spaceGrid[x - 1, z - 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            spaceGrid[x - 1, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }
    }

    public void showKnightAttacks(int x, int z, int y, char playersTurn) {
        // direction z+
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z + 2, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z + 2, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z + 2, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z + 2, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x, z + 2, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z + 2, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x, z + 2, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z + 2, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction z-
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z - 2, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z - 2, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z - 2, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z - 2, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x, z - 2, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z - 2, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x, z - 2, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z - 2, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x+
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 2, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 2, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x + 2, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 2, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x + 2, z, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 2, z, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x + 2, z, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 2, z, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x-
        try {
            if (playersTurn == 'w')
                spaceGrid[x - 2, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 2, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x - 2, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 2, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x - 2, z, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 2, z, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x - 2, z, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 2, z, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction y+
        try {
            if (playersTurn == 'w')
                spaceGrid[x, z + 1, y + 2, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z + 1, y + 2, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x, z - 1, y + 2, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z - 1, y + 2, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z, y + 2, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z, y + 2, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z, y + 2, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z, y + 2, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction y-
        try {
            if (playersTurn == 'w')
                spaceGrid[x, z + 1, y - 2, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z + 1, y - 2, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x, z - 1, y - 2, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z - 1, y - 2, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z, y - 2, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z, y - 2, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z, y - 2, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z, y - 2, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }
    }

    public void showBishopAttacks(int x, int z, int y, char playersTurn) {
        int tempX = x;
        int tempZ = z;
        int tempY = y;

        // plane space direction
        // direction x+ z+ y
        while (tempX < _cX - 1 && tempZ < _cZ - 1) {
            tempX += 1;
            tempZ += 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, y, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempZ = z;

        // direction x- z+ y
        while (tempX > 0 && tempZ < _cZ - 1) {
            tempX -= 1;
            tempZ += 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, y, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempZ = z;

        // direction x+ z- y
        while (tempX < _cX - 1 && tempZ > 0) {
            tempX += 1;
            tempZ -= 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, y, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempZ = z;

        // direction x- z- y
        while (tempX > 0 && tempZ > 0) {
            tempX -= 1;
            tempZ -= 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, y, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempZ = z;

        // higher space direction
        // direction x+ z y+
        while (tempX < _cX - 1 && tempY < _cY - 1) {
            tempX += 1;
            tempY += 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, z, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempY = y;

        // direction x- z y+
        while (tempX > 0 && tempY < _cY - 1) {
            tempX -= 1;
            tempY += 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, z, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempY = y;

        // direction x z+ y+
        while (tempZ < _cZ - 1 && tempY < _cY - 1) {
            tempZ += 1;
            tempY += 1;

            if (playersTurn == 'w')
                spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[x, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempZ = z;
        tempY = y;

        // direction x z- y+
        while (tempZ > 0 && tempY < _cY - 1) {
            tempZ -= 1;
            tempY += 1;

            if (playersTurn == 'w')
                spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[x, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempZ = z;
        tempY = y;

        // lower space direction
        // direction x+ z y-
        while (tempX < _cX - 1 && tempY > 0) {
            tempX += 1;
            tempY -= 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, z, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempY = y;

        // direction x- z y-
        while (tempX > 0 && tempY > 0) {
            tempX -= 1;
            tempY -= 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, z, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempY = y;

        // direction x z+ y-
        while (tempZ < _cZ - 1 && tempY > 0) {
            tempZ += 1;
            tempY -= 1;

            if (playersTurn == 'w')
                spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[x, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempZ = z;
        tempY = y;

        // direction x z- y-
        while (tempZ > 0 && tempY > 0) {
            tempZ -= 1;
            tempY -= 1;

            if (playersTurn == 'w')
                spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[x, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[x, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }
    }

    public void showRookAttacks(int x, int z, int y, char playersTurn) {
        int tempX = x;
        int tempZ = z;
        int tempY = y;

        // direction z+
        while (tempZ < _cZ - 1) {
            tempZ += 1;

            if (playersTurn == 'w')
                spaceGrid[x, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[x, tempZ, y, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[x, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[x, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempZ = z;

        // direction z-
        while (tempZ > 0) {
            tempZ -= 1;

            if (playersTurn == 'w')
                spaceGrid[x, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[x, tempZ, y, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[x, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[x, tempZ, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        // direction x+
        while (tempX < _cX - 1) {
            tempX += 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, z, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, z, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, z, y, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, z, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, z, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;

        // direction x-
        while (tempX > 0) {
            tempX -= 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, z, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, z, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, z, y, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, z, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, z, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        // direction y+
        while (tempY < _cY - 1) {
            tempY += 1;

            if (playersTurn == 'w')
                spaceGrid[x, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[x, z, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[x, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[x, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempY = y;

        // direction y-
        while (tempY > 0) {
            tempY -= 1;

            if (playersTurn == 'w')
                spaceGrid[x, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[x, z, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[x, z, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[x, z, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }
    }

    public void showQueenAttacks(int x, int z, int y, char playersTurn) {
        int tempX = x;
        int tempZ = z;
        int tempY = y;

        // higher space direction
        // direction x+ z+ y+
        while (tempX < _cX - 1 && tempZ < _cZ - 1 && tempY < _cY - 1) {
            tempX += 1;
            tempZ += 1;
            tempY += 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x- z+ y+
        while (tempX > 0 && tempZ < _cZ - 1 && tempY < _cY - 1) {
            tempX -= 1;
            tempZ += 1;
            tempY += 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x+ z- y+
        while (tempX < _cX - 1 && tempZ > 0 && tempY < _cY - 1) {
            tempX += 1;
            tempZ -= 1;
            tempY += 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x- z- y+
        while (tempX > 0 && tempZ > 0 && tempY < _cY - 1) {
            tempX -= 1;
            tempZ -= 1;
            tempY += 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // lower space direction
        // direction x+ z+ y-
        while (tempX < _cX - 1 && tempZ < _cZ - 1 && tempY > 0) {
            tempX += 1;
            tempZ += 1;
            tempY -= 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x- z+ y-
        while (tempX > 0 && tempZ < _cZ - 1 && tempY > 0) {
            tempX -= 1;
            tempZ += 1;
            tempY -= 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x+ z- y-
        while (tempX < _cX - 1 && tempZ > 0 && tempY > 0) {
            tempX += 1;
            tempZ -= 1;
            tempY -= 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x- z- y-
        while (tempX > 0 && tempZ > 0 && tempY > 0) {
            tempX -= 1;
            tempZ -= 1;
            tempY -= 1;

            if (playersTurn == 'w')
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;

            if (spaceGrid[tempX, tempZ, tempY, 1] != null) {
                if (playersTurn == 'w') {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
                    break;
                } else {
                    spaceGrid[tempX, tempZ, tempY, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
                    break;
                }
            }
        }
    }

    public void showKingAttacks(int x, int z, int y, char playersTurn) {

        // plane space direction
        // direction x+ z y
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x- z y
        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x z+ y
        try {
            if (playersTurn == 'w')
                spaceGrid[x, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x z- y
        try {
            if (playersTurn == 'w')
                spaceGrid[x, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x+ z+ y
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x- z+ y
        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z + 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x+ z- y
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x- z- y
        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z - 1, y, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // higher space direction
        // direction x z y+
        try {
            if (playersTurn == 'w')
                spaceGrid[x, z, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x+ z+ y+
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z + 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z + 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x- z+ y+
        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z + 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z + 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x+ z- y+
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z - 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z - 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x- z- y+
        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z - 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z - 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x z+ y+
        try {
            if (playersTurn == 'w')
                spaceGrid[x, z + 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z + 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x z- y+
        try {
            if (playersTurn == 'w')
                spaceGrid[x, z - 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z - 1, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x+ z y+
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x- z y+
        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z, y + 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z, y + 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        //lower space direction
        // direction x z y-
        try {
            if (playersTurn == 'w')
                spaceGrid[x, z, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x+ z+ y-
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z + 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z + 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x- z+ y-
        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z + 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z + 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x+ z- y-
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z - 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z - 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x- z- y-
        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z - 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z - 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x z+ y-
        try {
            if (playersTurn == 'w')
                spaceGrid[x, z + 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z + 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x z- y-
        try {
            if (playersTurn == 'w')
                spaceGrid[x, z - 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x, z - 1, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x+ z y-
        try {
            if (playersTurn == 'w')
                spaceGrid[x + 1, z, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x + 1, z, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }

        // direction x- z y-
        try {
            if (playersTurn == 'w')
                spaceGrid[x - 1, z, y - 1, 0].GetComponent<CellProperties>().isAttackedByWhite = true;
            else
                spaceGrid[x - 1, z, y - 1, 0].GetComponent<CellProperties>().isAttackedByBlack = true;
        } catch (Exception e) { }
    }
}
