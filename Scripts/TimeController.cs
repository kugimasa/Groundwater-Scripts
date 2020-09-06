using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float currentTime;

    void Start()
    {
        currentTime = 0.0f;
    }
    public void SetTime(float time)
    {
        currentTime = time;
    }
    public float GetTime()
    {
        return currentTime;
    }
    public void UpdateTime()
    {
        currentTime += Time.deltaTime;
    }
}
