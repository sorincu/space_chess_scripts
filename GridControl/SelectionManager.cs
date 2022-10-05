using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour {
    public GameObject gameOverMenu;
    public TMPro.TMP_Text resolutionMsg;
    public TMPro.TMP_Text winnerMsg;
    private string selectableTagWhite = "SelectableW";
    private string selectableTagBlack = "SelectableB";
    private float _positionScale;
    public char playersTurn = 'w'; 
    public Material currentCellMat;
    public GameObject[,,,] spaceGrid;
    private int _cX, _cY, _cZ;
    public int kingsAvailableMoves, allAvailableMoves;

    private Transform _selection;

    void Start() {
        _cX = (int)GridSizeController.xSize.value;
        _cY = (int)GridSizeController.ySize.value;
        _cZ = (int)GridSizeController.zSize.value;

        _positionScale = GridInitializationGame.positionScale;
        spaceGrid = GridInitializationGame.spaceGrid;
        gameOverMenu.SetActive(false);
    }

    void Update() {
        if (_selection != null) {
            _selection.GetComponent<FigureProperties>().isHovered = false;
            _selection = null;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f)) {
            var selection = hit.transform;
            string currentSelectable;

            if (playersTurn == 'w') currentSelectable = selectableTagWhite;
            else currentSelectable = selectableTagBlack;

            if (selection.CompareTag(currentSelectable)) {
                if (selection != null) {
                    selection.GetComponent<FigureProperties>().isHovered = true;

                    if (Input.GetMouseButtonDown(0)) {
                        GameObject.Find("Grid").GetComponent<ActiveFigure>().activeFigure = hit.transform.gameObject;

                        for (var y = 0; y < _cY; y++) {
                            for (var z = 0; z < _cZ; z++) {
                                for (var x = 0; x < _cX; x++) {
                                    spaceGrid[x, z, y, 0].GetComponent<CellProperties>().availableOff();
                                }
                            }
                        }

                        int posX = Convert.ToInt32(selection.position.x / _positionScale);
                        int posY = Convert.ToInt32(selection.position.y / _positionScale);
                        int posZ = Convert.ToInt32(selection.position.z / _positionScale);

                        switch (selection.name[1]) {
                            case 'P':
                                if (selection.name[0] == 'w')
                                    showWhitePawnMoves(posX, posZ, posY);
                                else showBlackPawnMoves(posX, posZ, posY);

                                allAvailableMoves = 0;
                                break;
                            case 'N':
                                showKnightMoves(posX, posZ, posY);

                                allAvailableMoves = 0;
                                break;
                            case 'B':
                                showBishopMoves(posX, posZ, posY);

                                allAvailableMoves = 0;
                                break;
                            case 'R':
                                showRookMoves(posX, posZ, posY);

                                allAvailableMoves = 0;
                                break;
                            case 'Q':
                                showBishopMoves(posX, posZ, posY);
                                showRookMoves(posX, posZ, posY);
                                showQueenMoves(posX, posZ, posY);

                                allAvailableMoves = 0;
                                break;
                            case 'K':
                                showKingMoves(posX, posZ, posY);

                                kingsAvailableMoves = 0;
                                break;
                            default: break;
                        }
                    }
                }

                _selection = selection;
            }
        }
    }

    // -------------------------------------------------------------------------------------------------------
    //  Figure move patterns

    public void pawnMovePattern(int x, int z, int y, int x2, int z2, int y2) {
        GameObject activeFigure = spaceGrid[x, z, y, 1];
        bool isAvailable = false;

        spaceGrid[x, z, y, 1] = null;
        spaceGrid[x2, z2, y2, 1] = activeFigure;

        spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().clearAttackingSpace();

        if (activeFigure.name[0] == 'w') {
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');

            if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().wK_PosX,
                           GameObject.Find("Grid").GetComponent<KingData>().wK_PosZ,
                           GameObject.Find("Grid").GetComponent<KingData>().wK_PosY, 0].
                           GetComponent<CellProperties>().isAttackedByBlack) {
                isAvailable = true;
                allAvailableMoves++;
            }
        } else {
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');

            if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().bK_PosX,
                           GameObject.Find("Grid").GetComponent<KingData>().bK_PosZ,
                           GameObject.Find("Grid").GetComponent<KingData>().bK_PosY, 0].
                           GetComponent<CellProperties>().isAttackedByWhite) {
                isAvailable = true;
                allAvailableMoves++;
            }
        }

        spaceGrid[x, z, y, 1] = activeFigure;
        spaceGrid[x2, z2, y2, 1] = null;

        if (isAvailable)
            spaceGrid[x2, z2, y2, 0].GetComponent<CellProperties>().availableOn();

        if (activeFigure.name[0] == 'w')
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');
        else
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');
    }

    public void pawnCapturePattern(int x, int z, int y, int x2, int z2, int y2) {
        GameObject activeFigure = spaceGrid[x, z, y, 1];
        bool isAvailable = false;

        var capturedFigure = spaceGrid[x2, z2, y2, 1];
        spaceGrid[x, z, y, 1] = null;
        spaceGrid[x2, z2, y2, 1] = activeFigure;

        spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().clearAttackingSpace();

        if (activeFigure.name[0] == 'w') {
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');

            if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().wK_PosX,
                           GameObject.Find("Grid").GetComponent<KingData>().wK_PosZ,
                           GameObject.Find("Grid").GetComponent<KingData>().wK_PosY, 0].
                           GetComponent<CellProperties>().isAttackedByBlack) {
                isAvailable = true;
                allAvailableMoves++;
            }
        } else {
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');

            if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().bK_PosX,
                           GameObject.Find("Grid").GetComponent<KingData>().bK_PosZ,
                           GameObject.Find("Grid").GetComponent<KingData>().bK_PosY, 0].
                           GetComponent<CellProperties>().isAttackedByWhite) {
                isAvailable = true;
                allAvailableMoves++;
            }
        }

        spaceGrid[x, z, y, 1] = activeFigure;
        spaceGrid[x2, z2, y2, 1] = capturedFigure;

        if (isAvailable) 
            spaceGrid[x2, z2, y2, 0].GetComponent<CellProperties>().potentialCaptureOn();

        if (activeFigure.name[0] == 'w')
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');
        else
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');
    }

    public void knightMovePattern(int x, int z, int y, int x2, int z2, int y2) {
        GameObject activeFigure = spaceGrid[x, z, y, 1];
        bool isAvailable = false;

        if (spaceGrid[x2, z2, y2, 1] == null) {
            spaceGrid[x, z, y, 1] = null;
            spaceGrid[x2, z2, y2, 1] = activeFigure;

            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().clearAttackingSpace();

            if (activeFigure.name[0] == 'w') {
                spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');

                if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().wK_PosX,
                               GameObject.Find("Grid").GetComponent<KingData>().wK_PosZ,
                               GameObject.Find("Grid").GetComponent<KingData>().wK_PosY, 0].
                               GetComponent<CellProperties>().isAttackedByBlack) {
                    isAvailable = true;
                    allAvailableMoves++;
                }
            } else {
                spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');

                if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().bK_PosX,
                               GameObject.Find("Grid").GetComponent<KingData>().bK_PosZ,
                               GameObject.Find("Grid").GetComponent<KingData>().bK_PosY, 0].
                               GetComponent<CellProperties>().isAttackedByWhite) {
                    isAvailable = true;
                    allAvailableMoves++;
                }
            }

            spaceGrid[x, z, y, 1] = activeFigure;
            spaceGrid[x2, z2, y2, 1] = null;

            if (isAvailable)
                spaceGrid[x2, z2, y2, 0].GetComponent<CellProperties>().availableOn();

            if (activeFigure.name[0] == 'w')
                spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');
            else
                spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');
        } else if (spaceGrid[x2, z2, y2, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
            var capturedFigure = spaceGrid[x2, z2, y2, 1];
            spaceGrid[x, z, y, 1] = null;
            spaceGrid[x2, z2, y2, 1] = activeFigure;

            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().clearAttackingSpace();

            if (activeFigure.name[0] == 'w') {
                spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');

                if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().wK_PosX,
                               GameObject.Find("Grid").GetComponent<KingData>().wK_PosZ,
                               GameObject.Find("Grid").GetComponent<KingData>().wK_PosY, 0].
                               GetComponent<CellProperties>().isAttackedByBlack) {
                    isAvailable = true;
                    allAvailableMoves++;
                }
            } else {
                spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');

                if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().bK_PosX,
                               GameObject.Find("Grid").GetComponent<KingData>().bK_PosZ,
                               GameObject.Find("Grid").GetComponent<KingData>().bK_PosY, 0].
                               GetComponent<CellProperties>().isAttackedByWhite) {
                    isAvailable = true;
                    allAvailableMoves++;
                }
            }

            spaceGrid[x, z, y, 1] = activeFigure;
            spaceGrid[x2, z2, y2, 1] = capturedFigure;

            if (isAvailable)
                spaceGrid[x2, z2, y2, 0].GetComponent<CellProperties>().potentialCaptureOn();

            if (activeFigure.name[0] == 'w')
                spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');
            else
                spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');
        }
    }

    public void majorFigureMovePattern(int x, int z, int y, int x2, int z2, int y2) {
        GameObject activeFigure = spaceGrid[x, z, y, 1];
        bool isAvailable = false;

        spaceGrid[x, z, y, 1] = null;
        spaceGrid[x2, z2, y2, 1] = activeFigure;

        spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().clearAttackingSpace();

        if (activeFigure.name[0] == 'w') {
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');

            if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().wK_PosX,
                            GameObject.Find("Grid").GetComponent<KingData>().wK_PosZ,
                            GameObject.Find("Grid").GetComponent<KingData>().wK_PosY, 0].
                            GetComponent<CellProperties>().isAttackedByBlack) {
                isAvailable = true;
                allAvailableMoves++;
            }
        } else {
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');

            if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().bK_PosX,
                            GameObject.Find("Grid").GetComponent<KingData>().bK_PosZ,
                            GameObject.Find("Grid").GetComponent<KingData>().bK_PosY, 0].
                            GetComponent<CellProperties>().isAttackedByWhite) {
                isAvailable = true;
                allAvailableMoves++;
            }
        }

        spaceGrid[x, z, y, 1] = activeFigure;
        spaceGrid[x2, z2, y2, 1] = null;

        if (isAvailable)
            spaceGrid[x2, z2, y2, 0].GetComponent<CellProperties>().availableOn();

        if (activeFigure.name[0] == 'w')
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');
        else
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');
    }

    public void majorFigureCapturePattern(int x, int z, int y, int x2, int z2, int y2) {
        GameObject activeFigure = spaceGrid[x, z, y, 1];
        bool isAvailable = false;

        var capturedFigure = spaceGrid[x2, z2, y2, 1];
        spaceGrid[x, z, y, 1] = null;
        spaceGrid[x2, z2, y2, 1] = activeFigure;

        spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().clearAttackingSpace();

        if (activeFigure.name[0] == 'w') {
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');

            if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().wK_PosX,
                            GameObject.Find("Grid").GetComponent<KingData>().wK_PosZ,
                            GameObject.Find("Grid").GetComponent<KingData>().wK_PosY, 0].
                            GetComponent<CellProperties>().isAttackedByBlack) {
                isAvailable = true;
                allAvailableMoves++;
            }
        } else {
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');

            if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().bK_PosX,
                            GameObject.Find("Grid").GetComponent<KingData>().bK_PosZ,
                            GameObject.Find("Grid").GetComponent<KingData>().bK_PosY, 0].
                            GetComponent<CellProperties>().isAttackedByWhite) {
                isAvailable = true;
                allAvailableMoves++;
            }
        }

        spaceGrid[x, z, y, 1] = activeFigure;
        spaceGrid[x2, z2, y2, 1] = capturedFigure;

        if (isAvailable)
            spaceGrid[x2, z2, y2, 0].GetComponent<CellProperties>().potentialCaptureOn();

        if (activeFigure.name[0] == 'w')
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');
        else
            spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');            
    }

    public void kingMovePattern(int x, int z, int y, int x2, int z2, int y2) {
        GameObject activeFigure = spaceGrid[x, z, y, 1];
        bool isAvailable = false;

        if (x2 < _cX && z2 < _cZ && y2 < _cY && x2 > -1 && z2 > -1 && y2 > -1) {
            if (spaceGrid[x2, z2, y2, 1] == null) {
                spaceGrid[x, z, y, 1] = null;
                spaceGrid[x2, z2, y2, 1] = activeFigure;

                spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().clearAttackingSpace();

                if (activeFigure.name[0] == 'w') {
                    GameObject.Find("Grid").GetComponent<KingData>().setwKPosition(x2, z2, y2);
                    spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');

                    if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().wK_PosX,
                                   GameObject.Find("Grid").GetComponent<KingData>().wK_PosZ,
                                   GameObject.Find("Grid").GetComponent<KingData>().wK_PosY, 0].
                                   GetComponent<CellProperties>().isAttackedByBlack) {
                        isAvailable = true;
                        kingsAvailableMoves++;
                    }
                } else {
                    GameObject.Find("Grid").GetComponent<KingData>().setbKPosition(x2, z2, y2);
                    spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');

                    if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().bK_PosX,
                                   GameObject.Find("Grid").GetComponent<KingData>().bK_PosZ,
                                   GameObject.Find("Grid").GetComponent<KingData>().bK_PosY, 0].
                                   GetComponent<CellProperties>().isAttackedByWhite) {
                        isAvailable = true;
                        kingsAvailableMoves++;
                    }
                }

                spaceGrid[x, z, y, 1] = activeFigure;
                spaceGrid[x2, z2, y2, 1] = null;

                if (activeFigure.name[0] == 'w') {
                    GameObject.Find("Grid").GetComponent<KingData>().setwKPosition(x, z, y);
                } else {
                    GameObject.Find("Grid").GetComponent<KingData>().setbKPosition(x, z, y);
                }

                if (isAvailable) {
                    spaceGrid[x2, z2, y2, 0].GetComponent<CellProperties>().availableOn();
                }

                if (activeFigure.name[0] == 'w')
                    spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');
                else
                    spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');
            } else if (spaceGrid[x2, z2, y2, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                var capturedFigure = spaceGrid[x2, z2, y2, 1];
                spaceGrid[x, z, y, 1] = null;
                spaceGrid[x2, z2, y2, 1] = activeFigure;

                spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().clearAttackingSpace();

                if (activeFigure.name[0] == 'w') {
                    GameObject.Find("Grid").GetComponent<KingData>().setwKPosition(x2, z2, y2);
                    spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');

                    if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().wK_PosX,
                                   GameObject.Find("Grid").GetComponent<KingData>().wK_PosZ,
                                   GameObject.Find("Grid").GetComponent<KingData>().wK_PosY, 0].
                                   GetComponent<CellProperties>().isAttackedByBlack) {
                        isAvailable = true;
                        kingsAvailableMoves++;
                    }
                } else {
                    GameObject.Find("Grid").GetComponent<KingData>().setbKPosition(x2, z2, y2);
                    spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');

                    if (!spaceGrid[GameObject.Find("Grid").GetComponent<KingData>().bK_PosX,
                                   GameObject.Find("Grid").GetComponent<KingData>().bK_PosZ,
                                   GameObject.Find("Grid").GetComponent<KingData>().bK_PosY, 0].
                                   GetComponent<CellProperties>().isAttackedByWhite) {
                        isAvailable = true;
                        kingsAvailableMoves++;
                    }
                }

                spaceGrid[x, z, y, 1] = activeFigure;
                spaceGrid[x2, z2, y2, 1] = capturedFigure;

                if (activeFigure.name[0] == 'w') {
                    GameObject.Find("Grid").GetComponent<KingData>().setwKPosition(x, z, y);
                } else {
                    GameObject.Find("Grid").GetComponent<KingData>().setbKPosition(x, z, y);
                }

                if (isAvailable)
                    spaceGrid[x2, z2, y2, 0].GetComponent<CellProperties>().potentialCaptureOn();

                if (activeFigure.name[0] == 'w')
                    spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('b');
                else
                    spaceGrid[0, 0, 0, 0].GetComponent<CellProperties>().getAttackingSpace('w');
            }
        }
    }

    // -------------------------------------------------------------------------------------------------------
    //  Figure functionality

    public void showWhitePawnMoves(int x, int z, int y) {
        // Current cell
        spaceGrid[x, z, y, 0].GetComponent<Renderer>().material = currentCellMat;
 
        // right-diagonal
        if (x + 1 != _cX) {
            // top-right
            if (y + 1 != _cY &&
                spaceGrid[x + 1, z + 1, y + 1, 1] != null &&
                spaceGrid[x + 1, z + 1, y + 1, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x + 1, z + 1, y + 1);
            }

            // low-right
            if (y - 1 != -1 &&
                spaceGrid[x + 1, z + 1, y - 1, 1] != null &&
                spaceGrid[x + 1, z + 1, y - 1, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x + 1, z + 1, y - 1);
            }

            // mid-right
            if (spaceGrid[x + 1, z + 1, y, 1] != null &&
                spaceGrid[x + 1, z + 1, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x + 1, z + 1, y);

            }
        }

        // left-diagonal
        if (x - 1 != -1) {
            // top-left
            if (y + 1 != _cY &&
                spaceGrid[x - 1, z + 1, y + 1, 1] != null &&
                spaceGrid[x - 1, z + 1, y + 1, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x - 1, z + 1, y + 1);
            }

            // low-left
            if (y - 1 != -1 &&
                spaceGrid[x - 1, z + 1, y - 1, 1] != null &&
                spaceGrid[x - 1, z + 1, y - 1, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x - 1, z + 1, y - 1);
            }

            // mid-left
            if (spaceGrid[x - 1, z + 1, y, 1] != null &&
                spaceGrid[x - 1, z + 1, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x - 1, z + 1, y);
            }
        }

        // top-center
        if (y + 1 != _cY &&
            spaceGrid[x, z + 1, y + 1, 1] == null) {
            pawnMovePattern(x, z, y, x, z + 1, y + 1);
        }

        // low-center
        if (y - 1 != -1 &&
            spaceGrid[x, z + 1, y - 1, 1] == null) {
            pawnMovePattern(x, z, y, x, z + 1, y - 1);
        }

        // mid-center
        if (spaceGrid[x, z + 1, y, 1] == null) {
            pawnMovePattern(x, z, y, x, z + 1, y);
        }
    }

    public void showBlackPawnMoves(int x, int z, int y) {
        // Current cell
        spaceGrid[x, z, y, 0].GetComponent<Renderer>().material = currentCellMat;

        // right-diagonal
        if (x + 1 != _cX) {
            // top-right
            if (y + 1 != _cY &&
                spaceGrid[x + 1, z - 1, y + 1, 1] != null &&
                spaceGrid[x + 1, z - 1, y + 1, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x + 1, z - 1, y + 1);
            }

            // low-right
            if (y - 1 != -1 &&
                spaceGrid[x + 1, z - 1, y - 1, 1] != null &&
                spaceGrid[x + 1, z - 1, y - 1, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x + 1, z - 1, y - 1);
            }

            // mid-right
            if (spaceGrid[x + 1, z - 1, y, 1] != null &&
                spaceGrid[x + 1, z - 1, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x + 1, z - 1, y);
            }
        }

        // left-diagonal
        if (x - 1 != -1) {
            // top-left
            if (y + 1 != _cY &&
                spaceGrid[x - 1, z - 1, y + 1, 1] != null &&
                spaceGrid[x - 1, z - 1, y + 1, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x - 1, z - 1, y + 1);
            }

            // low-left
            if (y - 1 != -1 &&
                spaceGrid[x - 1, z - 1, y - 1, 1] != null &&
                spaceGrid[x - 1, z - 1, y - 1, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x - 1, z - 1, y - 1);
            }

            // mid-left
            if (spaceGrid[x - 1, z - 1, y, 1] != null &&
                spaceGrid[x - 1, z - 1, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                pawnCapturePattern(x, z, y, x - 1, z - 1, y);
            }
        }

        // top-center
        if (y + 1 != _cY &&
            spaceGrid[x, z - 1, y + 1, 1] == null) {
            pawnMovePattern(x, z, y, x, z - 1, y + 1);
        }

        // low-center
        if (y - 1 != -1 &&
            spaceGrid[x, z - 1, y - 1, 1] == null) {
            pawnMovePattern(x, z, y, x, z - 1, y - 1);
        }

        // mid-center
        if (spaceGrid[x, z - 1, y, 1] == null) {
            pawnMovePattern(x, z, y, x, z - 1, y);
        }
    }

    public void showKnightMoves(int x, int z, int y) {
        spaceGrid[x, z, y, 0].GetComponent<Renderer>().material = currentCellMat;

        // direction z+
        try {
            knightMovePattern(x, z, y, x + 1, z + 2, y);    
        } catch (Exception e) { }
        
        try {
            knightMovePattern(x, z, y, x - 1, z + 2, y);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x, z + 2, y + 1);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x, z + 2, y - 1);
        } catch (Exception e) { }

        // direction z-
        try {
            knightMovePattern(x, z, y, x + 1, z - 2, y);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x - 1, z - 2, y);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x, z - 2, y + 1);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x, z - 2, y - 1);
        } catch (Exception e) { }

        // direction x+
        try {
            knightMovePattern(x, z, y, x + 2, z + 1, y);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x + 2, z - 1, y);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x + 2, z, y + 1);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x + 2, z, y - 1);
        } catch (Exception e) { }

        // direction x-
        try {
            knightMovePattern(x, z, y, x - 2, z + 1, y);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x - 2, z - 1, y);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x - 2, z, y + 1);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x - 2, z, y - 1);
        } catch (Exception e) { }

        // direction y+
        try {
            knightMovePattern(x, z, y, x, z + 1, y + 2);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x, z - 1, y + 2);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x + 1, z, y + 2);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x - 1, z, y + 2);
        } catch (Exception e) { }

        // direction y-
        try {
            knightMovePattern(x, z, y, x, z + 1, y - 2);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x, z - 1, y - 2);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x + 1, z, y - 2);
        } catch (Exception e) { }

        try {
            knightMovePattern(x, z, y, x - 1, z, y - 2);
        } catch (Exception e) { }
    }

    public void showBishopMoves(int x, int z, int y) {
        spaceGrid[x, z, y, 0].GetComponent<Renderer>().material = currentCellMat;

        int tempX = x;
        int tempZ = z;
        int tempY = y;

        // plane space direction
        // direction x+ z+ y
        while (tempX < _cX - 1 && tempZ < _cZ - 1) {
            tempX += 1;
            tempZ += 1;

            if (spaceGrid[tempX, tempZ, y, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY);
                break;
            } else break;
        }

        tempX = x;
        tempZ = z;

        // direction x- z+ y
        while (tempX > 0 && tempZ < _cZ - 1) {
            tempX -= 1;
            tempZ += 1;

            if (spaceGrid[tempX, tempZ, y, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempZ = z;

        // direction x+ z- y
        while (tempX < _cX - 1 && tempZ > 0) {
            tempX += 1;
            tempZ -= 1;

            if (spaceGrid[tempX, tempZ, y, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempZ = z;

        // direction x- z- y
        while (tempX > 0 && tempZ > 0) {
            tempX -= 1;
            tempZ -= 1;

            if (spaceGrid[tempX, tempZ, y, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempZ = z;

        // higher space direction
        // direction x+ z y+
        while (tempX < _cX - 1 && tempY < _cY - 1) {
            tempX += 1;
            tempY += 1;

            if (spaceGrid[tempX, z, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, z, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempY = y;

        // direction x- z y+
        while (tempX > 0 && tempY < _cY - 1) {
            tempX -= 1;
            tempY += 1;

            if (spaceGrid[tempX, z, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, z, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempY = y;

        // direction x z+ y+
        while (tempZ < _cZ - 1 && tempY < _cY - 1) {
            tempZ += 1;
            tempY += 1;

            if (spaceGrid[x, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[x, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempZ = z;
        tempY = y;

        // direction x z- y+
        while (tempZ > 0 && tempY < _cY - 1) {
            tempZ -= 1;
            tempY += 1;

            if (spaceGrid[x, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[x, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempZ = z;
        tempY = y;

        // lower space direction
        // direction x+ z y-
        while (tempX < _cX - 1 && tempY > 0) {
            tempX += 1;
            tempY -= 1;

            if (spaceGrid[tempX, z, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, z, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempY = y;

        // direction x- z y-
        while (tempX > 0 && tempY > 0) {
            tempX -= 1;
            tempY -= 1;

            if (spaceGrid[tempX, z, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, z, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempY = y;

        // direction x z+ y-
        while (tempZ < _cZ - 1 && tempY > 0) {
            tempZ += 1;
            tempY -= 1;

            if (spaceGrid[x, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[x, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempZ = z;
        tempY = y;

        // direction x z- y-
        while (tempZ > 0 && tempY > 0) {
            tempZ -= 1;
            tempY -= 1;

            if (spaceGrid[x, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[x, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }
    }

    public void showRookMoves(int x, int z, int y) {
        spaceGrid[x, z, y, 0].GetComponent<Renderer>().material = currentCellMat;
        int tempX = x;
        int tempZ = z;
        int tempY = y;

        // direction z+
        while (tempZ < _cZ - 1) {
            tempZ += 1;

            if (spaceGrid[x, tempZ, y, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[x, tempZ, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY);
                break;
            } else break;
        }

        tempZ = z;

        // direction z-
        while (tempZ > 0) {
            tempZ -= 1;

            if (spaceGrid[x, tempZ, y, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[x, tempZ, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY);
                break;
            } else break;
        }

        tempZ = z;

        // direction x+
        while (tempX < _cX - 1) {
            tempX += 1;

            if (spaceGrid[tempX, z, y, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, z, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY);
                break;
            } else break;
        }

        tempX = x;

        // direction x-
        while (tempX > 0) {
            tempX -= 1;

            if (spaceGrid[tempX, z, y, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, z, y, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY);
                break;
            } else break;
        }

        tempX = x;

        // direction y+
        while (tempY < _cY - 1) {
            tempY += 1;

            if (spaceGrid[x, z, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[x, z, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY);
                break;
            } else break;
        }

        tempY = y;

        // direction y-
        while (tempY > 0) {
            tempY -= 1;

            if (spaceGrid[x, z, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[x, z, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY);
                break;
            } else break;
        }
    }

    public void showQueenMoves(int x, int z, int y) {
        spaceGrid[x, z, y, 0].GetComponent<Renderer>().material = currentCellMat;

        int tempX = x;
        int tempZ = z;
        int tempY = y;

        // higher plane direction
        // direction x+ z+ y+
        while (tempX < _cX - 1 && tempZ < _cZ - 1 && tempY < _cY - 1) {
            tempX += 1;
            tempZ += 1;
            tempY += 1;

            if (spaceGrid[tempX, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x- z+ y+
        while (tempX > 0 && tempZ < _cZ - 1 && tempY < _cY - 1) {
            tempX -= 1;
            tempZ += 1;
            tempY += 1;

            if (spaceGrid[tempX, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x+ z- y+
        while (tempX < _cX - 1 && tempZ > 0 && tempY < _cY - 1) {
            tempX += 1;
            tempZ -= 1;
            tempY += 1;

            if (spaceGrid[tempX, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x- z- y+
        while (tempX > 0 && tempZ > 0 && tempY < _cY - 1) {
            tempX -= 1;
            tempZ -= 1;
            tempY += 1;

            if (spaceGrid[tempX, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // lower plane direction
        // direction x+ z+ y-
        while (tempX < _cX - 1 && tempZ < _cZ - 1 && tempY > 0) {
            tempX += 1;
            tempZ += 1;
            tempY -= 1;

            if (spaceGrid[tempX, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x- z+ y-
        while (tempX > 0 && tempZ < _cZ - 1 && tempY > 0) {
            tempX -= 1;
            tempZ += 1;
            tempY -= 1;

            if (spaceGrid[tempX, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x+ z- y-
        while (tempX < _cX - 1 && tempZ > 0 && tempY > 0) {
            tempX += 1;
            tempZ -= 1;
            tempY -= 1;

            if (spaceGrid[tempX, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }

        tempX = x;
        tempZ = z;
        tempY = y;

        // direction x- z- y-
        while (tempX > 0 && tempZ > 0 && tempY > 0) {
            tempX -= 1;
            tempZ -= 1;
            tempY -= 1;

            if (spaceGrid[tempX, tempZ, tempY, 1] == null)
                majorFigureMovePattern(x, z, y, tempX, tempZ, tempY);
            else if (spaceGrid[tempX, tempZ, tempY, 1].name[0] != spaceGrid[x, z, y, 1].name[0]) {
                majorFigureCapturePattern(x, z, y, tempX, tempZ, tempY); break;
            } else break;
        }
    }

    public void showKingMoves(int x, int z, int y) {
        spaceGrid[x, z, y, 0].GetComponent<Renderer>().material = currentCellMat;

        // plane space direction
        // direction x+ z y
        try {
            kingMovePattern(x, z, y, x + 1, z, y);
        } catch (Exception e) { }

        // direction x- z y
        try {
            kingMovePattern(x, z, y, x - 1, z, y);
        } catch (Exception e) { }

        // direction x z+ y
        try {
            kingMovePattern(x, z, y, x, z + 1, y);
        } catch (Exception e) { }

        // direction x z- y
        try {
            kingMovePattern(x, z, y, x, z - 1, y);
        } catch (Exception e) { }

        // direction x+ z+ y
        try {
            kingMovePattern(x, z, y, x + 1, z + 1, y);
        } catch (Exception e) { }

        // direction x- z+ y
        try {
            kingMovePattern(x, z, y, x - 1, z + 1, y);
        } catch (Exception e) { }

        // direction x+ z- y
        try {
            kingMovePattern(x, z, y, x + 1, z - 1, y);
        } catch (Exception e) { }

        // direction x- z- y
        try {
            kingMovePattern(x, z, y, x - 1, z - 1, y);
        } catch (Exception e) { }

        // higher space direction
        // direction x z y+
        try {
            kingMovePattern(x, z, y, x, z, y + 1);
        } catch (Exception e) { }

        // direction x+ z+ y+
        try {
            kingMovePattern(x, z, y, x + 1, z + 1, y + 1);
        } catch (Exception e) { }

        // direction x- z+ y+
        try {
            kingMovePattern(x, z, y, x - 1, z + 1, y + 1);
        } catch (Exception e) { }

        // direction x+ z- y+
        try {
            kingMovePattern(x, z, y, x + 1, z - 1, y + 1);
        } catch (Exception e) { }

        // direction x- z- y+
        try {
            kingMovePattern(x, z, y, x - 1, z - 1, y + 1);
        } catch (Exception e) { }

        // direction x z+ y+
        try {
            kingMovePattern(x, z, y, x, z + 1, y + 1);
        } catch (Exception e) { }

        // direction x z- y+
        try {
            kingMovePattern(x, z, y, x, z - 1, y + 1);
        } catch (Exception e) { }

        // direction x+ z y+
        try {
            kingMovePattern(x, z, y, x + 1, z, y + 1);
        } catch (Exception e) { }

        // direction x- z y+
        try {
            kingMovePattern(x, z, y, x - 1, z, y + 1);
        } catch (Exception e) { }

        //lower space direction
        // direction x z y-
        try {
            kingMovePattern(x, z, y, x, z, y - 1);
        } catch (Exception e) { }

        // direction x+ z+ y-
        try {
            kingMovePattern(x, z, y, x + 1, z + 1, y - 1);
        } catch (Exception e) { }

        // direction x- z+ y-
        try {
            kingMovePattern(x, z, y, x - 1, z + 1, y - 1);
        } catch (Exception e) { }

        // direction x+ z- y-
        try {
            kingMovePattern(x, z, y, x + 1, z - 1, y - 1);
        } catch (Exception e) { }

        // direction x- z- y-
        try {
            kingMovePattern(x, z, y, x - 1, z - 1, y - 1);
        } catch (Exception e) { }

        // direction x z+ y-
        try {
            kingMovePattern(x, z, y, x, z + 1, y - 1);
        } catch (Exception e) { }

        // direction x z- y-
        try {
            kingMovePattern(x, z, y, x, z - 1, y - 1);
        } catch (Exception e) { }

        // direction x+ z y-
        try {
            kingMovePattern(x, z, y, x + 1, z, y - 1);
        } catch (Exception e) { }

        // direction x- z y-
        try {
            kingMovePattern(x, z, y, x - 1, z, y - 1);
        } catch (Exception e) { }
    }
}
