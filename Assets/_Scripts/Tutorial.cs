using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Button nextButton;

    public List<GameObject> steps;

    private UIManager ui;
    private int step = 0;


    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
        StartTutorial();
    }

    private void StartTutorial() {
        ui.SetBuildEnabled(false);
        ui.SetPlayEnabled(false);
        nextButton.gameObject.SetActive(true);
        step = 0;
        nextButton.onClick.AddListener(NextStep);
        steps[step].SetActive(true);
    }

    
    private void NextStep() {
        steps[step].SetActive(false);

        step += 1;
        if (step >= steps.Count) {
            EndTutorial();
            return;
        }

        steps[step].SetActive(true);
    }

    private void EndTutorial() {
        nextButton.gameObject.SetActive(false);
        ui.SetBuildEnabled(true);
        ui.SetPlayEnabled(true);
    }
}
