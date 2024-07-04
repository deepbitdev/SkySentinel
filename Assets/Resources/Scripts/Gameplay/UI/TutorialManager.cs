using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextMeshPro tutorialTxt;
    public GameObject[] tutorialSteps;

    private int currentStep = 0;

    void Start()
    {
        ShowTutorialStep(currentStep);
    }

    public void NextButtonClicked()
    {
        if (currentStep < tutorialSteps.Length - 1)
        {
            currentStep++;
            ShowTutorialStep(currentStep);
        }
        else
        {
            EndTutorial();
        }
    }

    private void EndTutorial()
    {
        //tutorialPanel.SetActive(false);

        MenuManager.Instance.StartGame();
    }

    private void ShowTutorialStep(int stepIndex)
    {
        foreach(GameObject step in tutorialSteps)
        {
            step.SetActive(false);
        }

        // Activate the current tutorial step
        tutorialSteps[stepIndex].SetActive(true);

        // Update text
        tutorialTxt.text = "Step " + (stepIndex + 1) + ": " + tutorialSteps[stepIndex].name;
    }
}
