using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    PlayerHealth playerHealth;

    [SerializeField]
    Guage guage;


    private void Awake()
    {
        playerHealth.onHealthChanged.AddListener(UpdateUI);
    }

    void UpdateUI(int current, int max)
    {
        float ratio = (float)current / max;
        guage.SetGuage(ratio);
        guage.SetLable(current.ToString());
        //guage.SetLableMax(max.ToString());
    }
}
