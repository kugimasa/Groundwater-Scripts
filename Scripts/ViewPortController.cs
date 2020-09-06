﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPortController : MonoBehaviour
{
    [SerializeField] float aspectWidth = 4.0f;
    [SerializeField] float aspectHeight = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = calculateAspect(aspectWidth, aspectHeight);
        camera.rect = rect;
    }

    // Update is called once per frame
    private Rect calculateAspect(float width, float height)
    {
        float target_aspect = width / height;
        float window_aspect = (float)Screen.width / (float)Screen.height;
        float scale_height = window_aspect / target_aspect;
        Rect rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

        if (1.0f > scale_height)
        {
            rect.x = 0.0f;
            rect.y = (1.0f - scale_height) / 2.0f;
            rect.width = 1.0f;
            rect.height = scale_height;
        }
        else
        {
            float scale_width = 1.0f / scale_height;
            rect.x = (1.0f - scale_width) / 2.0f;
            rect.y = 0.0f;
            rect.width = scale_width;
            rect.height = 1.0f;
        }
        return rect;
    }
}
