using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{
    public void Retry()
    {
        Time.timeScale = 1f; // 혹시 정지 상태라면 되돌림
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene"); // 메인 타이틀 씬 이름에 맞게 수정
    }
}
