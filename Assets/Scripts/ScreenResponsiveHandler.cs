using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResponsiveHandler : MonoBehaviour
{
    [SerializeField]
    Vector2 defaultScreenSize = new Vector2(2960f, 1440f);
    [SerializeField]
    CanvasScaler[] arrCanvasScalers;
    [SerializeField]
    ScreenResponsiveObject[] arrResponsiveUI;

    [SerializeField]
    ScreenOrientation currentOrientation = ScreenOrientation.LandscapeLeft;

    // Update is called once per frame
    void Update()
    {
        CheckScreenOrientation();
    }

    private void CheckScreenOrientation()
    {

        if (Screen.orientation != currentOrientation)
        {
            if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                if (currentOrientation == ScreenOrientation.Portrait || currentOrientation == ScreenOrientation.PortraitUpsideDown)
                {
                    ChangeCanvasScreenRatio(true);
                    currentOrientation = Screen.orientation;
                }
            } else if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                if (currentOrientation == ScreenOrientation.LandscapeLeft || currentOrientation == ScreenOrientation.LandscapeRight)
                {
                    ChangeCanvasScreenRatio(false);
                    currentOrientation = Screen.orientation;
                }
            }
        }
    }

    float GetRelativeScreenRatio ()
    {
        float ratio = 1f * Screen.width / Screen.height;
        float originalRatio = defaultScreenSize.x / defaultScreenSize.y;
        return ratio / originalRatio;
    }

    void ChangeCanvasScreenRatio(bool isLandScape)
    {
        foreach (var canvas in arrCanvasScalers)
        {
            canvas.matchWidthOrHeight = isLandScape ? 0 : 1f;
        }
        float relativeRatio = GetRelativeScreenRatio();
        foreach (var ui in arrResponsiveUI)
        {
            ui.ChangeOrientation(relativeRatio);
        }
    }
}
