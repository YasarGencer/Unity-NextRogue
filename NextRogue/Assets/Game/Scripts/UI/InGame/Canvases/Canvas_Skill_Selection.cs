using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Skill_Selection : AUI
{
    Transform _child1;

    [SerializeField]
    HUDSkillSelection[] _skillSelection;
    [SerializeField]
    HUDSlotSelection[] _slotSelection;
    Animator _animator;
    ASpell _spell; 
    public override void Initialize() {
        base.Initialize(); 
        _child1 = transform.GetChild(1);
        _child1.gameObject.SetActive(false);
        Close();
    }
    public override void Open() {
        gameObject.SetActive(true); 
        _child.gameObject.SetActive(true);
        _child1.gameObject.SetActive(false);
        //alpha 
        GetComponent<CanvasGroup>().alpha = 0; 
        GetComponent<CanvasGroup>().DOFade(1,1);         
        //header 
        var header = _child.GetChild(0).GetComponent<RectTransform>();
        var value = 0;
        DOTween.To(() => value, x => value = x, 1250, 1)
        .OnUpdate(() => {
            header.sizeDelta = new Vector2(value, header.sizeDelta.y);
        }).SetEase(Ease.InCirc);
        //skills
        var skills = _child.GetChild(1);
        skills.localPosition = new(0, -100, 0);
        skills.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InCirc);

        SetSkills();
    }
    public override void Close(float time = 0) {
        base.Close(time);
    }
    void SetSkills() {
        var spells = MainManager.Instance.GameManager.AllSpells.GetRandomSpell(3);
        for (int i = 0; i < spells.Count; i++) 
            _skillSelection[i].Initialize(spells[i]); 
    }
    void SetSlots() {
        for (int i = 0; i < 5; i++) {
            ASpell spell = MainManager.Instance.Player.GetComponentInChildren<P_MainController>()
                .Spells.GetSpell(i + 4);
            if (spell != null)
                _slotSelection[i].Initialize(spell);
            else
                _slotSelection[i].Initialize();
        }
    }
    public void SaveSelected(ASpell spell) {
        _spell = spell;
        SetSlots();
        NextPage();
    }  
    void NextPage() {
        _child.gameObject.SetActive(false);
        _child1.gameObject.SetActive(true);

        //headers
        var header = _child1.GetChild(0).GetComponent<RectTransform>();
        var header1 = _child1.GetChild(1).GetComponent<RectTransform>();
        var value = 0;
        DOTween.To(() => value, x => value = x, 1250, 1)
        .OnUpdate(() => {
            header.sizeDelta = new Vector2(value, header.sizeDelta.y);
            header1.sizeDelta = new Vector2(value, header.sizeDelta.y);
        }).SetEase(Ease.InCirc); 
        //slots
        var slots = _child1.GetChild(2);
        slots.localPosition = new(0, -100, 0);
        slots.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InCirc);
        var alpha = slots.GetComponent<CanvasGroup>();
        float alphaValue = 0;
        alpha.alpha = 0;
        DOTween.To(() => alphaValue, x => alphaValue = x, 1, 1)
        .OnUpdate(() => {
            alpha.alpha = alphaValue;
        }).SetEase(Ease.InCirc);
    }
    public void SaveButton(int value) {
        MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Spells.SetSpell(value + 4, _spell);
        MainManager.Instance.EventManager.RunOnGameUnPuase();
    }
}
