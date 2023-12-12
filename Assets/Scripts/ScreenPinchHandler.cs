using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPinchHandler : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    [SerializeField]
    float minFov = 50f;
    [SerializeField]
    float maxFov = 75f;
    [SerializeField]
    float currentFov = 65f;

    float startingDistance = 0f;
    float initialScaleRatio;
    // Start is called before the first frame update
    void Start()
    {
        initialScaleRatio = 15f / 25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch2.phase == TouchPhase.Began)
            {
                startingDistance = Vector2.Distance(touch1.position, touch2.position);
                initialScaleRatio = (cam.fieldOfView - minFov) / (maxFov - minFov);
            } else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);
                float deltaDistance = startingDistance - currentDistance;
                float scaleRatio = Mathf.Clamp(initialScaleRatio + deltaDistance / Screen.width, 0, 1f);

                cam.fieldOfView = Mathf.Lerp(minFov, maxFov, scaleRatio);
            }
        }
    }
}
