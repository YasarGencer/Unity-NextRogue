using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDSkillFrame : MonoBehaviour
{
    public GameObject selectedFrame;

    public void OnPointerEnter()
    {
        selectedFrame.SetActive(true);
    }

    public void OnPointerExit()
    {
        selectedFrame.SetActive(false);
    }

    public void OnSelect()
    {
        selectedFrame.SetActive(true);
    }

    public void OnDeselect()
    {
        selectedFrame.SetActive(false);
    }
}
