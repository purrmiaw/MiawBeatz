using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameManager {

    private IEnumerable<Step> _steps;
    private string _stepFilePath;
    private float _window; // step window.

    private float _currentTime;

    public GameManager()
    {
        _window = .9f;
        _stepFilePath = "assets/_game/files/beats.txt";
        _steps = GetStepsFromText(_stepFilePath);
    }

    private IEnumerable<Step> GetStepsFromText(string filePath)
    {
        string[] meow = File.ReadAllLines(filePath);
        int i = 0;

        List<Step> steps = new List<Step>();

        foreach (var item in meow.ToList())
        {
            var theBeat = item.Split('\t')[0];
            steps.Add(new Step { IntStep = i, Time = float.Parse(theBeat) });
            i += 1;
        }

        return steps;
    }

    private Step GetCurrentStep(float currentTime)
    {
        Step currentCalculatedStep = _steps.Where(x => currentTime <= (x.Time + _window))
            .Where(x => currentTime >= (x.Time - _window))
            .OrderBy(x => x.IntStep).FirstOrDefault();

        return currentCalculatedStep;
    }

    // mark all step that is passed but is NA as missed.
    private void MarkPreviousNAStepsAsMissed(float currentTime)
    {
        IEnumerable<Step> stepsThatHasAlreadyPassedButIsNa 
            = _steps.Where(x => currentTime > (x.Time + _window)).Where(x => x.Score == StepScore.NA).OrderBy(x => x.IntStep);

        foreach (Step passedStep in stepsThatHasAlreadyPassedButIsNa)
        {
            passedStep.Score = StepScore.Missed;
        }
    }

    public void PlayerPress(float currentTime)
    {
        Step currentStep = GetCurrentStep(currentTime);

        // only record score if NA (user haven't press yet)
        if (currentStep.Score != StepScore.NA)
        {
            return;
        }

    }


}
