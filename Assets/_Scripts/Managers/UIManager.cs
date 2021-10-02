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

    [Header("Play panel")]
    public Transform playButton;
    public Transform resetButton;

    private GameManager gm;
    private BuildManager build;

    private List<Button> buildButtons;

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
        buildButtons = new List<Button>();

        foreach (BuildPiece piece in build.pieces) {
            GameObject iconGo = Instantiate(buildIconPref, buildPanel);
            Image image = iconGo.GetComponent<Image>();
            image.sprite = piece.icon;
            Button btn = iconGo.GetComponent<Button>();
            btn.onClick.AddListener(() => build.StartPlacing(piece));
            buildButtons.Add(btn);
        }
    }

    public void OnPlay() {
        playButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(true);
        SetBuildEnabled(false);
    }

    public void OnReset() {
        playButton.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(false);
        SetBuildEnabled(true);
    }

    public void SetPlayEnabled(bool enabled) {
        playButton.GetComponent<Button>().interactable = enabled;
    }

    public void SetBuildEnabled(bool enabled) {
        foreach (Button btn in buildButtons) {
            btn.interactable = enabled;
        }

    }

}
