using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This script initializes the grid and the game figures

public class GridInitializationGame : MonoBehaviour {
    public GameObject P, R, B, N, Q, K, p, r, b, n, q, k;
    public Material cellMatWhite, cellMatBlack, availableCellMat, currentCellMat, potentialCaptureCellMat;

    // c(X, Y, Z) - Cells Per Axis (X, Y, Z)
    private int _cX, _cY, _cZ;
    public static float positionScale = 1.3f;
    
    // The space grid is an 4-dimensional array that stores every cell and figure object
    public static GameObject[, , ,] spaceGrid;

    // Constructor
    public GridInitializationGame() {
        spaceGrid = new GameObject[8, 8, 4, 2];
    }

    void Start() {
        _cX = (int)GridSizeController.xSize.value;
        _cY = (int)GridSizeController.ySize.value;
        _cZ = (int)GridSizeController.zSize.value;

        figurePositioning(_cX, _cY, _cZ);

        // Cell initialization and positioning
        for (var y = 0; y < _cY; y++) {
            for (var z = 0; z < _cZ; z++) {
                for (var x = 0; x < _cX; x++) {
                    var cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cell.name = "" + x + z + y;
                    cell.transform.position = new Vector3(x * positionScale, y * positionScale, z * positionScale);
                    cell.transform.localScale = new Vector3(1f, 0.05f, 1f);
                    cell.AddComponent<CellProperties>();
                    cell.GetComponent<CellProperties>().availableCellMat = availableCellMat;
                    cell.GetComponent<CellProperties>().currentCellMat = currentCellMat;
                    cell.GetComponent<CellProperties>().potentialCaptureCellMat = potentialCaptureCellMat;

                    // Assigning cells with black or white color
                    if ((x + y + z) % 2 == 0) {
                        cell.GetComponent<Renderer>().material = cellMatBlack;
                        cell.GetComponent<CellProperties>().originalMat = cellMatBlack;
                    } else {
                        cell.GetComponent<Renderer>().material = cellMatWhite;
                        cell.GetComponent<CellProperties>().originalMat = cellMatWhite;
                    }

                    // Index  [*, *, *, 1] of the grid is the figure object
                    // Index  [*, *, *, 0] of the grid is the cell object
                    spaceGrid[x, z, y, 0] = cell;
                }
            }
        }
    }

    private void figurePositioning(int x, int y, int z) {
        switch (x) {
            case 6:
                GameObject[] whPos6 = { R, B, Q, K, B, R, P };
                GameObject[] blPos6 = { r, b, q, k, b, r, p };

                
                for (var i = 0; i < x; i++) {
                    // Instantiate white pieces
                    spaceGrid[i, 0, 0, 1] = 
                        Instantiate(whPos6[i], 
                        new Vector3(i * positionScale, 0, 0), 
                        Quaternion.identity);

                    spaceGrid[i, 1, 0, 1] = 
                        Instantiate(whPos6[6], 
                        new Vector3(i * positionScale, 0, positionScale), 
                        Quaternion.identity);

                    // Instantiate black pieces
                    spaceGrid[i, z - 1, y - 1, 1] = 
                        Instantiate(blPos6[i], 
                        new Vector3(i * positionScale, (y - 1) * positionScale, (z - 1) * positionScale), 
                        Quaternion.identity);
                
                    spaceGrid[i, z - 2, y - 1, 1] = 
                        Instantiate(blPos6[6], 
                        new Vector3(i * positionScale, (y - 1) * positionScale, (z - 2) * positionScale), 
                        Quaternion.identity);
                }

                GameObject.Find("Grid").GetComponent<KingData>().setwKPosition(3, 0, 0);
                GameObject.Find("Grid").GetComponent<KingData>().setbKPosition(3, z - 1, y - 1);

                break;
            case 7:
                GameObject[] whPos7 = { R, B, Q, K, Q, B, R, P };
                GameObject[] blPos7 = { r, b, q, k, q, b, r, p };

                // Instantiate white pieces
                for (var i = 0; i < x; i++) {
                    spaceGrid[i, 0, 0, 1] =
                        Instantiate(whPos7[i],
                        new Vector3(i * positionScale, 0, 0),
                        Quaternion.identity);
                }
                for (var i = 0; i < x; i++) {
                    spaceGrid[i, 1, 0, 1] =
                        Instantiate(whPos7[7],
                        new Vector3(i * positionScale, 0, positionScale),
                        Quaternion.identity);
                }

                // Instantiate black pieces
                for (var i = 0; i < x; i++) {
                    spaceGrid[i, z - 1, y - 1, 1] =
                        Instantiate(blPos7[i],
                        new Vector3(i * positionScale, (y - 1) * positionScale, (z - 1) * positionScale),
                        Quaternion.identity);
                }
                for (var i = 0; i < x; i++) {
                    spaceGrid[i, z - 2, y - 1, 1] =
                        Instantiate(blPos7[7],
                        new Vector3(i * positionScale, (y - 1) * positionScale, (z - 2) * positionScale),
                        Quaternion.identity);
                }

                GameObject.Find("Grid").GetComponent<KingData>().setwKPosition(3, 0, 0);
                GameObject.Find("Grid").GetComponent<KingData>().setbKPosition(3, z - 1, y - 1);

                break;
            case 8:
                GameObject[] whPos8 = { R, N, B, Q, K, B, N, R, P };
                GameObject[] blPos8 = { r, n, b, q, k, b, n, r, p };

                // Instantiate white pieces
                for (var i = 0; i < x; i++) {                
                    spaceGrid[i, 0, 0, 1] = 
                        Instantiate(whPos8[i], 
                        new Vector3(i * positionScale, 0, 0), 
                        Quaternion.Euler(0f, -90f, 0f));
                }
                for (var i = 0; i < x; i++) {                   
                    spaceGrid[i, 1, 0, 1] = 
                        Instantiate(whPos8[8], 
                        new Vector3(i * positionScale, 0, positionScale),
                        Quaternion.identity);
                }

                // Instantiate black pieces
                for (var i = 0; i < x; i++) {
                    spaceGrid[i, z - 1, y - 1, 1] = 
                        Instantiate(blPos8[i], 
                        new Vector3(i * positionScale, (y - 1) * positionScale, (z - 1) * positionScale), 
                        Quaternion.Euler(0f, 90f, 0f));
                }
                for (var i = 0; i < x; i++) {                   
                    spaceGrid[i, z - 2, y - 1, 1] = 
                        Instantiate(blPos8[8], 
                        new Vector3(i * positionScale, (y - 1) * positionScale, (z - 2) * positionScale), 
                        Quaternion.identity);
                }

                GameObject.Find("Grid").GetComponent<KingData>().setwKPosition(4, 0, 0);
                GameObject.Find("Grid").GetComponent<KingData>().setbKPosition(4, z - 1, y - 1);

                break;
            default:
                Debug.Log("Something went wrong !");
                break;
        }
    }
}

