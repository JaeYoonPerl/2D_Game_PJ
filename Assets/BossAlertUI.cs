using System.Collections;
using UnityEngine;
using TMPro;

public class BossAlertUI : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TMP_Text alertText;

    public void ShowAlert(string message)
    {
        alertText.text = message;
        StartCoroutine(ShowAndHide());
    }

    IEnumerator ShowAndHide()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        yield return new WaitForSeconds(2.5f); // 알림 유지 시간

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
