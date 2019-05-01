using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string name;
    public string letterGrade = "";
    public float gradePercent = 0.0f;

    public Level(string name, string letterGrade, float gradePercent ) {
        this.name = name;
        this.letterGrade = letterGrade;
        this.gradePercent = gradePercent;
    }
}
