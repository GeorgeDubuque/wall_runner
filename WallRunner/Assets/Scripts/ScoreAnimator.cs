using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreAnimator : MonoBehaviour
{
    public float finalScore = 0;
    public string gradeScore = "";
    public int totalNumCoins = 0;
    float animScore = 0;
    public TextMeshProUGUI scoreText;
    public GameObject finishText;
    public GameObject totalCoins;
    public GameObject totalScore;
    public GameObject grade;

    Animator anim;

    private void Start ( ) {
        anim = GetComponent<Animator>();
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
        grade.GetComponent<TextMeshProUGUI>().text = gradeScore;
        grade.SetActive(true);
    }

    public void LoadMainMenu ( ) {
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerData.levelDict[sceneName].grade = gradeScore;
        SceneManager.LoadScene("MainMenu");
    }
}
