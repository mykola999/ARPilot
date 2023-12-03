using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UICheckPoint : MonoBehaviour
{
    
    [SerializeField]
    RectTransform uiObject;
    [SerializeField]
    Transform targetObject;
    [SerializeField]
    GameObject blinkButton;
    [SerializeField]
    GameObject numberCircle;
    [SerializeField]
    TextMeshProUGUI txtOrderNum;
    [SerializeField]
    GameObject errorNotification;

    private Camera cam;
    private CanvasGroup blinkCanvasGroup;
    private Action<int> ButtonClickAction;

    private int orderNumber;
    public int OrderNumber { get { return orderNumber; } }

    private bool isChecked;
    public bool IsChecked => isChecked;
    
    public void InitializeCheckPoint(Camera cam, Transform target, int orderNumber, Action<int> btnClickedAction)
    {
        this.cam = cam;
        this.orderNumber = orderNumber;
        targetObject = target;
        isChecked = false;
        txtOrderNum.text = (orderNumber + 1).ToString();
        blinkButton.SetActive(!isChecked);
        numberCircle.SetActive(isChecked);
        ButtonClickAction = btnClickedAction;
        blinkButton.GetComponent<Button>().onClick.AddListener(OnClickButton);
        Blink();
    }
    public void SetButton(bool isCorrect)
    {
        isChecked = isCorrect;
        blinkButton.SetActive(!isChecked);
        numberCircle.SetActive(isChecked);
    }

    private void Update()
    {
        UpdateUIPosition();
    }

    private void UpdateUIPosition()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(targetObject.position);
        uiObject.position = screenPos;
    }
    
    private void Blink()
    {
        blinkCanvasGroup = blinkButton.GetComponentInChildren<CanvasGroup>();
        if (blinkCanvasGroup != null)
        {
            Sequence sequence = DOTween.Sequence()
                .Append(blinkCanvasGroup.DOFade(0,1))
                .Append(blinkCanvasGroup.DOFade(1,1))
                .AppendInterval(1)
                .SetLoops(-1);
        }
    }

    private void OnClickButton ()
    {
        if (!isChecked)
        {
            ButtonClickAction(orderNumber);
        }
    }

    public void ShowError()
    {
        StartCoroutine(ShowErrorMsg());
    }

    private IEnumerator ShowErrorMsg()
    {
        blinkButton.SetActive(false);
        errorNotification.SetActive(true);
        CanvasGroup errorCanvas = errorNotification.GetComponentInChildren<CanvasGroup>();
        Sequence sequence = DOTween.Sequence()
            .Append(errorCanvas.DOFade(0, 0.1f))
            .Append(errorCanvas.DOFade(1, 1))
            .AppendInterval(1)
            .Append(errorCanvas.DOFade(0, 1));
        yield return new WaitForSeconds(3f);
        blinkButton.SetActive(true);
        errorNotification.SetActive(false);
    }
}
