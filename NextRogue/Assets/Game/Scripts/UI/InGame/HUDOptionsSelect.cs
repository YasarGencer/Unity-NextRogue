using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDOptionsSelect : MonoBehaviour
{
    public GameObject selectedProb;

    public void OnPointerEnter()
    {
        selectedProb.SetActive(true);
    }

    public void OnPointerExit()
    {
        selectedProb.SetActive(false);
    }

    public void OnSelect()
    {
        selectedProb.SetActive(true);
    }

    public void OnDeselect()
    {
        selectedProb.SetActive(false);
    }
    public void OnDisable()
    {
        selectedProb.SetActive(false);
    }
}
