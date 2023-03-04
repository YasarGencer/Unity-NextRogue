using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Skill_Selection : AUI
{
    [SerializeField]
    HUDSkillSelection[] _skillSelection;
    [SerializeField]
    HUDSlotSelection[] _slotSelection;
    Animator _animator;
    ASpell _spell; 
    public override void Initialize() {
        _animator = GetComponent<Animator>();
        Close();
    }
    public override void Open() {
        base.Open();
        SetSkills();
    }
    public override void Close() {
        base.Close();
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
        _animator.SetTrigger("nextPage");
        SetSlots();
    }  
    public void SaveButton(int value) {
        MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Spells.SetSpell(value + 4, _spell);
        MainManager.Instance.EventManager.RunOnGameUnPuase();
    }
}
