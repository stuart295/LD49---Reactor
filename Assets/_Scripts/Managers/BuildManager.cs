using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public List<BuildPiece> pieces;

    private GameObject placingPieceGo;
    private BuildPiece placingPiece;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacement();
    }

    private void UpdatePlacement() {
        if (!IsPlacing()) return;

        //Position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        placingPieceGo.transform.position = SnapToGrid(mousePos);

        //Place on left click
        if (Input.GetMouseButtonDown(0)) {
            placingPieceGo = null;
            StartPlacing(placingPiece);
        }

        //Cancel on right clicl
        if (Input.GetMouseButtonDown(1)) {
            placingPieceGo = null;
            placingPiece = null;
        }
    }

    public Vector3 SnapToGrid(Vector3 pos) {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0);
    }


    public bool IsPlacing() {
        return placingPieceGo != null;
    }

    public void StartPlacing(BuildPiece piece) {
        if (IsPlacing()) return;

        Debug.Log("Placing " + piece.pieceName);

        placingPieceGo = Instantiate(piece.prefab);
        placingPiece = piece;

    }

 


}
