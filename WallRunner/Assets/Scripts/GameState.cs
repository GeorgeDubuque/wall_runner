using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public Dictionary<string, Level> levelDictionary;

    public GameState( Dictionary<string, Level> levels ) {
        levelDictionary = levels;
    }
}
