using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public List<BuildPiece> pieces;

    public float rotationSize = 30f;
    public float rotationSpeed = 1f;

    private GameObject placingPieceGo;
    private BuildPiece placingPiece;
    private float placeRotation = 0f;

    private GameManager gm;
    private UIManager ui;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        ui = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.Playing) return;

        if (IsPlacing()) {
            UpdatePlacement();
        }
        else {
            UpdateReplacement();
        }

        
    }

    private Placable CheckObjectClick() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity); ;

        if (hit != null && hit.collider != null) {
            Debug.Log("Clicked on" + hit.collider.gameObject.name);
            return hit.collider.transform.root.GetComponent<Placable>();
        }

        return null;
    }

    private void UpdateReplacement() {
        //Move piece
        if (Input.GetMouseButtonDown(0)) {
            Placable placable = CheckObjectClick();
            if (placable != null) {
                placable.OnRemoved();
                ui.SetPlayEnabled(false);
                placingPieceGo = placable.gameObject;
                placingPiece = null;
                placeRotation = placingPieceGo.transform.rotation.eulerAngles.z;
            }
        }

        //Delete piece
        if (Input.GetMouseButtonDown(1)) {
            Placable placable = CheckObjectClick();
            if (placable != null && placable.destroyable) {
                placable.OnRemoved();
                Destroy(placable.gameObject);
            }
        }
    }

    private void UpdatePlacement() {

        //Position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        placingPieceGo.transform.position = SnapToGrid(mousePos);

        //Place on left click
        if (Input.GetMouseButtonDown(0)) {
            FinishPlacement();
            return;
        }

        //Cancel on right click
        if (Input.GetMouseButtonDown(1)) {
            CancelPlacement();
            return;
        }

        //Rotate
        placeRotation += Input.mouseScrollDelta.y * rotationSpeed;

        placingPieceGo.transform.rotation = Quaternion.Euler(0, 0, SnapRotation(placeRotation));

    }

    private void FinishPlacement() {
        Placable placable = placingPieceGo.GetComponent<Placable>();
        placable.OnPlaced();

        placingPieceGo = null;
        if (placingPiece != null)
            StartPlacing(placingPiece);
        else
            ui.SetPlayEnabled(true);
    }

    private void CancelPlacement() {
        Placable placeable = placingPieceGo.GetComponent<Placable>();
        if (placeable.destroyable) {
            Destroy(placingPieceGo);
            placingPieceGo = null;
            placingPiece = null;
            ui.SetPlayEnabled(true);
        }
    }

    public Vector3 SnapToGrid(Vector3 pos) {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0);
    }

    public float SnapRotation(float rotation) {
        float snapped = rotation / rotationSize;
        snapped = Mathf.Round(snapped);
        return snapped * rotationSize;
    }


    public bool IsPlacing() {
        return placingPieceGo != null;
    }

    public void StartPlacing(BuildPiece piece) {
        if (IsPlacing()) return;

        Debug.Log("Placing " + piece.pieceName);

        ui.SetPlayEnabled(false);

        //Position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        placingPieceGo = Instantiate(piece.prefab, SnapToGrid(mousePos), Quaternion.identity);
        placingPiece = piece;

    }


    public void OnPlay() {
    }

    public void OnReset() {
    }



}
