using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDMainmenuPlayer : AUI
{
    [SerializeField]
    TextMeshProUGUI _name;
    [SerializeField]
    HUDSkillDescription[] _skills;
    public override void Initialize() { 
        _child = transform.GetChild(0);
        GetComponent<CanvasGroup>().alpha = 0f;
    }
    public void Open(P_Stats stat) {
        base.Open();

        _name.SetText(stat.Name);

        for (int i = 0; i < _skills.Length; i++)
            _skills[i].Show(stat.Spells[i]);
    }
    public override void Close() {
        var alpha = GetComponent<CanvasGroup>();
        float alphaValue = alpha.alpha; 

        DOTween.To(() => alphaValue, x => alphaValue = x, 0, .5f)
        .OnUpdate(() => {
            alpha.alpha = alphaValue;
        }).SetEase(Ease.InCirc);
    }
}
