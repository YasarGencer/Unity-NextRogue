using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
    public void Initialize() {
        CloseSkills();
    } 
    public void OnPlayerInitialized() { 
    }

    void CloseSkills() {
        MainManager.Instance.CanvasManager.Player_GUI_HUD.SkillIconsVisibility(false);
    } 
}
