using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Build panel")]
    public Transform buildPanel;
    public GameObject buildIconPref;

    private GameManager gm;
    private BuildManager build;

    // Start is called before the first frame update
    void Start()
    {
        build = GetComponent<BuildManager>();
        gm = GetComponent<GameManager>();

        SetupBuildMenu();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupBuildMenu() {
        foreach (BuildPiece piece in build.pieces) {
            GameObject iconGo = Instantiate(buildIconPref, buildPanel);
            Image image = iconGo.GetComponent<Image>();
            image.sprite = piece.icon;
            Button btn = iconGo.GetComponent<Button>();
            btn.onClick.AddListener(() => build.StartPlacing(piece));

        }
    }

}
