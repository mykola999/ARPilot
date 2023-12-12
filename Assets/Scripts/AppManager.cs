/*****************************************
 * It is the main manager script of the 
 * project. It creates the check UI button objects,
 * receives their click events and send the result to 
 * CheckListHandler as well.
 * *************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    [SerializeField]
    GameObject uiCheckPointPrefab;
    [SerializeField]
    Transform[] targetPositions;
    [SerializeField]
    Transform transCanvas;
    [SerializeField]
    Camera cam;
    [SerializeField]
    CheckListHandler checkListHandler;
    [SerializeField]
    private float hitDistance = 10f;
    [SerializeField]
    private string frontLayerName = "FrontDisplay";

    private List<UICheckPoint> uiCheckPoints = new List<UICheckPoint>();
    private int currentPointNumber = 0;

    private Vector2 centerScreenPos = Vector2.zero;
    private Vector2 centerLeftScreenPos = Vector2.zero;
    private Vector2 centerRightScreenPos = Vector2.zero;

    public int CurrentPointNumber => currentPointNumber;
    public bool HitFrontScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        GenerateUIPoints();
    }


    void Update ()
    {
        CastRaysFromCamera();
    }

    private void CastRaysFromCamera()
    {
        int width = Screen.width;
        int height = Screen.height;
        bool centerHit = CastRayFromCamera(new Vector2(width / 2f, height / 2f));
        bool leftHit = CastRayFromCamera(new Vector2(0f, height / 2f));
        bool rightHit = CastRayFromCamera(new Vector2(width - 1f, height / 2f));

        if (centerHit || leftHit || rightHit)
        {
            HitFrontScreen = true;
        } else
        {
            HitFrontScreen = false;
        }
    }

    private bool CastRayFromCamera(Vector2 screenPoint)
    {
        Ray ray = cam.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hitDistance, LayerMask.GetMask(frontLayerName)))
        {
            return true;
        }
        return false;
    }
    //Generate the UI buttons of the check points in the screen from the target positions array and uiCheckPoint Prefab.
    private void GenerateUIPoints()
    {
        uiCheckPoints.Clear();
        currentPointNumber = 0;
        for (int i = 0; i < targetPositions.Length; i++)
        {
            GameObject objUiPoint = Instantiate(uiCheckPointPrefab, transCanvas);
            UICheckPoint uiCheckPoint = objUiPoint.GetComponent<UICheckPoint>();
            if (uiCheckPoint != null)
            {
                uiCheckPoint.InitializeCheckPoint(this, cam, targetPositions[i], i, OnCheckButtonClicked);
            } 
            uiCheckPoints.Add(uiCheckPoint);
        }
    }

    //If both the Check UI button is clicked or CheckList UI item is clicked, this function is called.
    //It checks the current order number and if the clicked object is correct one then inform the button
    // click is correct for related UI objects.
    public void OnCheckButtonClicked(int orderNumber)
    {
        if (uiCheckPoints == null)
        {
            return;
        }
        UICheckPoint uiCheckPoint = uiCheckPoints[orderNumber];
        if (uiCheckPoints[orderNumber] == null)
        {
            return;
        }

        if (orderNumber == currentPointNumber)
        {
            uiCheckPoint.SetButton(true);
            checkListHandler.OnListItemClickSuccess(orderNumber);
            
            if (currentPointNumber < uiCheckPoints.Count - 1)
            {
                currentPointNumber++;
            }
        } else
        {
            uiCheckPoint.ShowError();
        }
    }

    //Handles the reset with current point order number and all checkpoint UI objects.
    public void Reset()
    {
        currentPointNumber = 0;
        foreach (var checkpoint in uiCheckPoints)
        {
            checkpoint.SetButton(false);
        }
    }

}
