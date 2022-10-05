using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingData : MonoBehaviour {
    public GameObject[,,,] spaceGrid;
    public Material potentialCaptureCellMat;

    public int wK_PosX, wK_PosZ, wK_PosY;
    public int bK_PosX, bK_PosZ, bK_PosY;
    public bool wK_isInCheck = false;
    public bool bK_isInCheck = false;

    void Start() {
        spaceGrid = GridInitializationGame.spaceGrid;
    }

    void Update() {
        if (wK_isInCheck) {
            spaceGrid[wK_PosX, wK_PosZ, wK_PosY, 1].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            spaceGrid[wK_PosX, wK_PosZ, wK_PosY, 0].GetComponent<CellProperties>().kingPotentialCaptureOn();
        } else {
            spaceGrid[wK_PosX, wK_PosZ, wK_PosY, 1].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }

        if (bK_isInCheck) {
            spaceGrid[bK_PosX, bK_PosZ, bK_PosY, 1].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            spaceGrid[bK_PosX, bK_PosZ, bK_PosY, 0].GetComponent<CellProperties>().kingPotentialCaptureOn();
        } else {
            spaceGrid[bK_PosX, bK_PosZ, bK_PosY, 1].GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        }
    }

    public void setwKPosition(int x, int z, int y) {
        wK_PosX = x;
        wK_PosZ = z;
        wK_PosY = y;
    }

    public void setbKPosition(int x, int z, int y) {
        bK_PosX = x;
        bK_PosZ = z;
        bK_PosY = y;
    }

    public void verifyIfKingIsInCheck() {
        if (spaceGrid[bK_PosX, bK_PosZ, bK_PosY, 0].
                      GetComponent<CellProperties>().isAttackedByWhite) {
            bK_isInCheck = true;
        } else bK_isInCheck = false;

        if (spaceGrid[wK_PosX, wK_PosZ, wK_PosY, 0].
                      GetComponent<CellProperties>().isAttackedByBlack) {
            wK_isInCheck = true;
        } else wK_isInCheck = false;
    }
}
