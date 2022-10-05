using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script initializes the grid for the Menu scene

public class GridInitializationMenu : MonoBehaviour {
    public Material cellMatWhite, cellMatBlack;
    public TMPro.TMP_Text xSize, ySize, zSize;

    // c(X, Y, Z) - Cells Per Axis (X, Y, Z)
    public int cX, cY, cZ;
    private int _prevcX, _prevcY, _prevcZ;

    private float _positionScale = 1.3f;

    private GameObject[, ,] _spaceGrid;

    // Constructor
    public GridInitializationMenu() {
        _spaceGrid = new GameObject[8, 8, 4];
    }

    void Start() {
        cX = int.Parse(xSize.text);
        cY = int.Parse(ySize.text);
        cZ = int.Parse(zSize.text);
        _prevcX = cX;
        _prevcY = cY;
        _prevcZ = cZ;
        
        for (var y = 0; y < 4; y++) {
            for (var z = 0; z < 8; z++) {
                for (var x = 0; x < 8; x++) {
                    var cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cell.name = "" + (x + 1) + (z + 1) + (y + 1);
                    cell.transform.position = new Vector3(x * _positionScale, y * _positionScale, z * _positionScale);
                    cell.transform.localScale = new Vector3(1f, 0.05f, 1f);

                    // Assigning cells with black or white color
                    if ((x + y + z) % 2 == 0) cell.GetComponent<Renderer>().material = cellMatBlack;
                    else cell.GetComponent<Renderer>().material = cellMatWhite;

                    if (x >= cX) cell.SetActive(false);
                    if (y >= cY) cell.SetActive(false);
                    if (z >= cZ) cell.SetActive(false);

                    _spaceGrid[x, z, y] = cell;
                }
            }
        }     
    }

    void Update() {
        cX = int.Parse(xSize.text);
        cY = int.Parse(ySize.text);
        cZ = int.Parse(zSize.text);

        if (cX != _prevcX) {
            if (cX > _prevcX) {
                for (; cX > _prevcX; _prevcX++) {
                    for (var y = 0; y < cY; y++) {
                        for (var z = 0; z < cZ; z++)
                            _spaceGrid[_prevcX, z, y].SetActive(true);
                    }
                }
            }

            if (cX < _prevcX) {
                for (; cX < _prevcX; _prevcX--) {
                    for (var y = 0; y < cY; y++) {
                        for (var z = 0; z < cZ; z++)
                            _spaceGrid[_prevcX - 1, z, y].SetActive(false);
                    }
                }
            }
        }

        if (cY != _prevcY) {
            if (cY > _prevcY) {
                for (; cY > _prevcY; _prevcY++) {
                    for (var x = 0; x < cX; x++) {
                        for (var z = 0; z < cZ; z++)
                            _spaceGrid[x, z, _prevcY].SetActive(true);
                    }
                }
            }

            if (cY < _prevcY) {
                for (; cY < _prevcY; _prevcY--) {
                    for (var x = 0; x < cX; x++) {
                        for (var z = 0; z < cZ; z++)
                            _spaceGrid[x, z, _prevcY - 1].SetActive(false);
                    }
                }
            }
        }

        if (cZ != _prevcZ) {
            if (cZ > _prevcZ) {
                for (; cZ > _prevcZ; _prevcZ++) {
                    for (var y = 0; y < cY; y++) {
                        for (var x = 0; x < cX; x++)
                            _spaceGrid[x, _prevcZ, y].SetActive(true);
                    }
                }
            }

            if (cZ < _prevcZ) {
                for (; cZ < _prevcZ; _prevcZ--) {
                    for (var y = 0; y < cY; y++) {
                        for (var x = 0; x < cX; x++)
                            _spaceGrid[x, _prevcZ - 1, y].SetActive(false);
                    }
                }
            }
        }
    }
}

