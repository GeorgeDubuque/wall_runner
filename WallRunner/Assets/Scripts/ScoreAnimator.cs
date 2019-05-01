using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreAnimator : MonoBehaviour
{
    public float finalScore = 0;
    public string letterGrade = "";
    public float gradePercent = 0f;
    public int totalNumCoins = 0;
    float animScore = 0;
    public TextMeshProUGUI scoreText;
    public GameObject finishText;
    public GameObject totalCoins;
    public GameObject totalScore;
    public GameObject grade;
    Level currLevel;
    Animator anim;

    private void Start ( ) {
        anim = GetComponent<Animator>();
        currLevel = PlayerData.levelDict[SceneManager.GetActiveScene().name];
    }

    public void IncreaseAnimScore ( ) {
        scoreText.text = ((int)(animScore)).ToString();
        if (animScore != finalScore) {
            anim.SetTrigger("IncreaseScore");
            animScore++;
        } else {
            anim.SetTrigger("ScoreDone");
        }

    }

    public void ShowFinishText ( ) {
        finishText.SetActive(true);
    }

    public void ShowTotalCoins ( ) {
        totalCoins.GetComponent<TextMeshProUGUI>().text = "/" + totalNumCoins.ToString();
        totalCoins.SetActive(true);
    }

    public void ShowTotalScore ( ) {
        totalScore.SetActive(true);
    }

    public void ShowGrade ( ) {
        grade.GetComponent<TextMeshProUGUI>().text = this.letterGrade;
        grade.SetActive(true);
    }

    public void SetGrade(string newLetterGrade, float newGradePercent ) {
        this.letterGrade = newLetterGrade;
        if (currLevel.gradePercent < newGradePercent) {
            currLevel.gradePercent = newGradePercent;
            currLevel.letterGrade = newLetterGrade;
        }
    }

    public void LoadMainMenu ( ) {
        SceneManager.LoadScene("MainMenu");
    }
}
