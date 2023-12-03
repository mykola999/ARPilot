using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckUIButton : MonoBehaviour
{
    [SerializeField]
    Color originColor;
    [SerializeField]
    Color checkedColor;
    [SerializeField]
    Image bgImage;
    [SerializeField]
    Image checkImage;
    [SerializeField]
    CheckListHandler checkListHandler;
    [SerializeField]
    int orderNumber;

    bool isChecked;
    
    public void OnClick()
    {
        if (checkListHandler == null)
        {
            return;
        }
        if (isChecked)
        {
            return;
        }
        checkListHandler.OnListItemClicked(orderNumber);
    }

    public void SetChecked()
    {
        isChecked = true;
        bgImage.color = checkedColor;
        checkImage.color = checkedColor;
    }

    public void Reset()
    {
        isChecked = false;
        bgImage.color = originColor;
        checkImage.color = originColor;
    }

}
