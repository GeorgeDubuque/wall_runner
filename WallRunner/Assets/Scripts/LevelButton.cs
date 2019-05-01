using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public TextMeshProUGUI levelNumText;
    public TextMeshProUGUI gradeText;
    public Level level;
    Button button;

    public void SetLevelNum(string name ) {
        levelNumText.text = name;
    }

    public void SetGrade(string grade ) {
        gradeText.text = grade;
    }

    public void SetLevel ( Level level ) {
        this.level = level;
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(this.LoadLevel);
    }

    private void LoadLevel(){
        SceneManager.LoadScene(level.name);
    }
}
