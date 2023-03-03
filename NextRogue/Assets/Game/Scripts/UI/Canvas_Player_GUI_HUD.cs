using UnityEngine;

public class Canvas_Player_GUI_HUD : AUI
{ 
    P_MainController _mainController;

    [SerializeField]
    HUDSlider _health, _level;
    [SerializeField]
    GameObject _damagetext;

    public HUDSkillIcon[] spellIconList; 

    [Header("SPELL DESCRIPTION")]
    public HUDSkillDescription Description; 
    public override void Initialize() {
        base.Initialize();
        _mainController = GameObject.FindGameObjectWithTag("Player").GetComponent<P_MainController>();
        
        Description.Hide();
        if (_mainController) {
            _health.Initialize(_mainController.Stats.Health, _mainController.Stats.MaxHealth, 0);
            _level.Initialize(_mainController.Stats.EXP, _mainController.Stats.EXPRequired, _mainController.Stats.Level);
            Invoke("SetKeys", .5f);
        }
        Close();
    }
    public override void Open() {
        base.Open();
    }
    public override void Close() {
        base.Close();
    } 
    void SetKeys() {
        var text = _mainController.Input.GetKeyInfo();
        var textPart = text.Split("/");
        for (int i = 0; i < textPart.Length; i++) {
            spellIconList[i].SetKey(textPart[i]);
        }
    }
    public void SetHealth() {
        _health.SetValue(_mainController.Stats.Health);
    }
    public void SetLevel() {
        _level.SetValue(_mainController.Stats.EXP, _mainController.Stats.EXPRequired, _mainController.Stats.Level);
    }
    public void SetSkillIcon(HUDSkillIcon[] list,int keyIndex, ASpell spell) {
        list[keyIndex].Initialize(spell, _mainController);
    }
    public void DamageText(bool damage,string text,Vector2 pos) {
        Instantiate(_damagetext, pos, Quaternion.identity).GetComponent<WORLDDamageText>().Initialize(damage,text);
    }
}