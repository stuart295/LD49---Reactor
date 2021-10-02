using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Build panel")]
    public Transform buildPanel;
    public GameObject buildIconPref;

    [Header("Play panel")]
    public Transform playButton;
    public Transform resetButton;

    [Header("Energy panel")]
    public Image energyBar;
    public TMP_Text energyText;
    public TMP_Text demandText;

    private GameManager gm;
    private BuildManager build;
    private EnergyManager energy;

    private List<Button> buildButtons;

    // Start is called before the first frame update
    void Start() {
        build = GetComponent<BuildManager>();
        gm = GetComponent<GameManager>();
        energy = GetComponent<EnergyManager>();

        SetupBuildMenu();
    }


    // Update is called once per frame
    void Update() {
        UpdateEnergyDisplay();
    }

    private void UpdateEnergyDisplay() {
        energyBar.fillAmount = energy.GetPercentAmount();
        energyText.text = string.Format("{0} / {1}", energy.CurrentEnergy, energy.required);
        demandText.text = string.Format("Demand: {0}", energy.demandDescription);
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
