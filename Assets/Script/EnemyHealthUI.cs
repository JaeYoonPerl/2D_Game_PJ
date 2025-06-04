using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField]
    Enemy enemy;

    [SerializeField]
    Guage guage;

    private void Awake()
    {
        enemy.ChangedHPEvent.AddListener(UpdateUI);
    }

    void UpdateUI(int current, int max)
    {
        float ratio = (float)current / max;
        guage.SetGuage(ratio);
    }


    void LateUpdate()
    {
        // �׻� ī�޶� ���ϰ�
        transform.forward = Camera.main.transform.forward;
    }
}
