using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    public Transform levelGrid;
    public GameObject levelButtonPrefab;
    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        int levelNum = 1;
        foreach (KeyValuePair<string,Level> levelEntry in PlayerData.levelDict) {
            GameObject levelButton = Instantiate(levelButtonPrefab);
            levelButton.GetComponent<LevelButton>().SetLevelNum(levelNum.ToString());
            levelButton.GetComponent<LevelButton>().SetGrade(levelEntry.Value.letterGrade);
            levelButton.GetComponent<LevelButton>().SetLevel(levelEntry.Value);
            levelButton.transform.SetParent(levelGrid);
            levelNum++;
        }
        GameState gameState = new GameState(PlayerData.levelDict);
        SaveSystem.SaveGame(gameState);
    }
}
