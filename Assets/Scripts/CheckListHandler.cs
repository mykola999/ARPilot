/*************************************************
 * Check List UI item container and handler script
 * It receives the List UI Item click event and 
 * send it to AppManager script. It also handles 
 * Resetting Button Click event.
************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckListHandler : MonoBehaviour
{
    [SerializeField]
    AppManager appManager;
    [SerializeField]
    RectTransform rectTransform;
    [SerializeField]
    Transform transFoldIcon;
    [SerializeField]
    Vector2 foldSize;
    [SerializeField]
    Vector2 openSize;
    [SerializeField]
    Vector2 extSize;
    [SerializeField]
    Vector2 minSize;
    [SerializeField]
    List<CheckUIButton> checkUIButtons;

    private bool isFold = true;

    // This function is used to fold and open the check list UI panel.
    // The UI animation uses the DOTWEEN 
    public void FoldPanelOnOff()
    {
        isFold = !isFold;
        if (isFold)
        {
            Sequence sequence = DOTween.Sequence()
                .Append(rectTransform.DOSizeDelta(extSize, 0.5f))
                .Append(rectTransform.DOSizeDelta(openSize, 0.5f));
            transFoldIcon.localScale = new Vector3(1f, 1f, 1f);
        } else
        {
            Sequence sequence = DOTween.Sequence()
                .Append(rectTransform.DOSizeDelta(minSize, 0.5f))
                .Append(rectTransform.DOSizeDelta(foldSize, 0.5f));
            transFoldIcon.localScale = new Vector3(1f, -1f, 1f);
        }
    }

    //If the CheckList UI Item is clicked it send the info to AppManager
    public void OnListItemClicked(int orderNum)
    {
        appManager.OnCheckButtonClicked(orderNum);
    }

    //Receives the UI checklist item click result success from AppManager
    // and send it to the correct Checklist item.
    public void OnListItemClickSuccess(int orderNum)
    {
        checkUIButtons[orderNum].SetChecked();
    }

    //If Reset button is clicked it resets the check list UI items and 
    // send the info to AppManager
    public void OnClickReset()
    {
        foreach (var uiItems in checkUIButtons)
        {
            uiItems.Reset();
        }
        appManager.Reset();
    }
}
