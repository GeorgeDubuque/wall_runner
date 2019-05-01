using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    private void Awake ( ) {
        GameState gameState = SaveSystem.LoadGame();
        if(gameState != null) {
            PlayerData.levelDict = gameState.levelDictionary;
        }
        SceneManager.LoadScene("MainMenu");
    }
}
