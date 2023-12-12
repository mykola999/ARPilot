using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResponsiveObject : MonoBehaviour
{
    [SerializeField]
    RectTransform rect;
    [SerializeField]
    Vector2 originalSize;
    [SerializeField]
    bool adjustHeight;
    [SerializeField]
    bool adjustWidth;

    public void ChangeOrientation(float ratio)
    {
        float fNewHeight = adjustHeight ? ratio * originalSize.y : originalSize.y;
        float fNewWidth = adjustWidth ? ratio * originalSize.x : originalSize.x;
        rect.sizeDelta = new Vector2(fNewWidth, fNewHeight);
    }
}
