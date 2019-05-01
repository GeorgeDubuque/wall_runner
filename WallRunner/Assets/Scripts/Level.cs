using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string name;
    public string grade = "";

    public Level(string name, string grade ) {
        this.name = name;
        this.grade = grade;
    }
}
