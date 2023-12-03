using System.Collections;
using System.Collections.Generic;
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

    private List<UICheckPoint> uiCheckPoints = new List<UICheckPoint>();
    private int currentPointNumber = 0;
    public int CurrentPointNumber => currentPointNumber;

    // Start is called before the first frame update
    void Start()
    {
        GenerateUIPoints();
    }

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
                uiCheckPoint.InitializeCheckPoint(cam, targetPositions[i], i, OnCheckButtonClicked);
            } 
            uiCheckPoints.Add(uiCheckPoint);
        }
    }

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

    public void Reset()
    {
        currentPointNumber = 0;
        foreach (var checkpoint in uiCheckPoints)
        {
            checkpoint.SetButton(false);
        }
    }

}
