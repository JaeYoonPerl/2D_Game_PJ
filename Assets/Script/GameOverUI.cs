using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{
    public void Retry()
    {
        Time.timeScale = 1f; // Ȥ�� ���� ���¶�� �ǵ���
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���� �� �ٽ� �ε�
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene"); // ���� Ÿ��Ʋ �� �̸��� �°� ����
    }
}
