using System;
using UnityEngine;
using UnityEngine.UI;

public class AutoLayout : MonoBehaviour
{
    void Start()
    {
        float standard_width = 960f; //初始宽度
        float standard_height = 640f; //初始高度
        float device_width = 0f; //当前设备宽度
        float device_height = 0f; //当前设备高度
        float adjuster = 0f; //屏幕矫正比例
        //获取设备宽高
        device_width = Screen.width;
        device_height = Screen.height;
        //计算宽高比例
        float standard_aspect = standard_width / standard_height;
        float device_aspect = device_width / device_height;
        //计算矫正比例
        if (device_aspect < standard_aspect)
        {
            adjuster = standard_aspect / device_aspect;
        }

        CanvasScaler canvasScaleTemp = transform.GetComponent<CanvasScaler>();
        canvasScaleTemp.matchWidthOrHeight = Math.Abs(adjuster) < 0.1f ? 1 : 0;
    }
}
