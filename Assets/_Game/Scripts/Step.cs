using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step
{
    public int IntStep;
    public float Time;
    public float? TimePressed;

    public StepScore Score = StepScore.NA;
}

public enum StepScore
{
    NA,
    Missed,
    OK,
    Good,
    Excellent,
    Poor
}