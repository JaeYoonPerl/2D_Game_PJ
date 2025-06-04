using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ClearUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timeText;

    public void ShowClear(int finalScore, float finalTime)
    {
        scoreText.text = $"{finalScore}";

        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);
        timeText.text = $"{minutes:00}:{seconds:00}";

        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoNextStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("NextStageScene");
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }
}
