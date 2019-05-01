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
            Debug.Log(levelEntry.Value.name);
            levelButton.GetComponent<LevelButton>().SetGrade(levelEntry.Value.grade);
            Debug.Log(levelEntry.Value.grade);
            levelButton.GetComponent<LevelButton>().SetLevel(levelEntry.Value);
            levelButton.transform.parent = levelGrid;
            levelNum++;
        }
        GameState gameState = new GameState(PlayerData.levelDict);

        SaveSystem.SaveGame(gameState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
