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

    public void OnListItemClicked(int orderNum)
    {
        appManager.OnCheckButtonClicked(orderNum);
    }

    public void OnListItemClickSuccess(int orderNum)
    {
        checkUIButtons[orderNum].SetChecked();
    }

    public void OnClickReset()
    {
        foreach (var uiItems in checkUIButtons)
        {
            uiItems.Reset();
        }
        appManager.Reset();
    }
}
