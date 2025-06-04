using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    public float currentTime = 0f;
    public bool isCounting = true;

    public UnityEvent<float> onTimeChanged;

    private void Update()
    {
        if (!isCounting) return;

        currentTime += Time.deltaTime;
        onTimeChanged?.Invoke(currentTime);
    }

    public void ResetTimer()
    {
        currentTime = 0f;
        onTimeChanged?.Invoke(currentTime);
    }

    public void StopTimer()
    {
        isCounting = false;
    }

    public void StartTimer()
    {
        isCounting = true;
    }
}
