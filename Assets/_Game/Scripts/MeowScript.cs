using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class MeowScript : MonoBehaviour {

    public Text TextNyer;
    public Text TextKeyInput;
    public int CurrentStepInt;

    private AudioSource _theAudio;
    private ICollection<Step> _steps;
    private float _window = 0.09f; // Press window in seconds

	// Use this for initialization
	void Start () {

        // init
        _steps = new List<Step>();
        _theAudio = GetComponent<AudioSource>();

        string[] meow = File.ReadAllLines("assets/_game/files/beats.txt");
        int i = 0;

        foreach(var item in meow.ToList())
        {
            var theBeat = item.Split('\t')[0];
            _steps.Add(new Step { IntStep = i, Time = float.Parse(theBeat) });
            i += 1;
        }

        CurrentStepInt = 0;
        TextNyer.text = "";

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TextKeyInput.text += " " + _theAudio.time;
        }

        // determine the current step
        // window is -0.09 to +0.09
        Step currentCalculatedStep = _steps.Where(x => _theAudio.time <= (x.Time + _window)).Where(x => _theAudio.time >= (x.Time - _window)).OrderBy(x => x.IntStep).FirstOrDefault();

        // if no steps
        if (currentCalculatedStep == null)
        {
            CurrentStepInt = 99999;
            return;
        }

        // mark all step that is passed but is NA as missed.
        IEnumerable<Step> stepsThatHasAlreadyPassedButIsNa = _steps.Where(x => _theAudio.time > (x.Time + _window)).Where(x => x.Score == StepScore.NA).OrderBy(x => x.IntStep);
        foreach (Step passedStep in stepsThatHasAlreadyPassedButIsNa)
        {
            passedStep.Score = StepScore.Missed;
            TextNyer.text += " " + passedStep.IntStep.ToString() + " " + passedStep.Score.ToString() + " ";
            return;
        }

        CurrentStepInt = currentCalculatedStep.IntStep;

        // only record score if NA (user haven't press yet)
        if (currentCalculatedStep.Score != StepScore.NA)
        {
            return;
        }

        // detect input if user haven't pressed for current step
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float timeDifference = Mathf.Abs(currentCalculatedStep.Time - _theAudio.time);

            if (timeDifference <= 0.06)
            {
                currentCalculatedStep.Score = StepScore.Excellent;
            }
            else if (timeDifference <= 0.75)
            {
                currentCalculatedStep.Score = StepScore.Good;
            }
            else if (timeDifference <= 0.09)
            {
                currentCalculatedStep.Score = StepScore.OK;
            }
            else
            {
                currentCalculatedStep.Score = StepScore.Poor;
            }

            currentCalculatedStep.TimePressed = _theAudio.time;
            TextNyer.text += " " + currentCalculatedStep.IntStep.ToString() + " " + currentCalculatedStep.Score.ToString() + " " + currentCalculatedStep.TimePressed.Value.ToString();
        }

    }
}

