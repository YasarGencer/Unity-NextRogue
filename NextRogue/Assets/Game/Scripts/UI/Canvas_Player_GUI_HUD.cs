using UnityEngine;

public class Canvas_Player_GUI_HUD : AUI
{ 
    P_MainController _mainController;

    [SerializeField]
    HUDSlider _health, _level, _secondary;
    [SerializeField]
    GameObject _damagetext;

    public HUDSkillIcon[] spellIconList; 

    [Header("SPELL DESCRIPTION")]
    public HUDSkillDescription Description; 
    public override void Initialize() {
        base.Initialize();
        Description.Hide();
        Close();
    }
    protected override void OnGameStart() {
        base.OnGameStart();
        _mainController = GameObject.FindGameObjectWithTag("Player").GetComponent<P_MainController>();
        Description.Hide();
        if (_mainController) {
            _health.Initialize(_mainController.Stats.Health, _mainController.Stats.MaxHealth, 0);
            _level.Initialize(_mainController.Stats.EXP, _mainController.Stats.EXPRequired, _mainController.Stats.Level);
            Invoke("SetKeys", .5f);
            if (_mainController.Stats.SecondaryBar)
                InitializeSecondary(0, 1, 0);
        }
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
        for (int i = 0; i < textPart.Length - 1; i++) {
            spellIconList[i].SetKey(textPart[i]);
        }
    }
    public void SetHealth() {
        _health.SetValue(_mainController.Stats.Health);
    }
    public void SetLevel() {
        _level.SetValue(_mainController.Stats.EXP, _mainController.Stats.EXPRequired, _mainController.Stats.Level);
    }
    public void InitializeSecondary(float value, float maxValue,float constant) {
        _secondary.gameObject.SetActive(true);
        _secondary.Initialize(value, maxValue, constant);
    }
    public void SetSecondary(float value, float maxValue) {
        _secondary.SetValue(value, maxValue);
    }
    public void SetSecondary(float value) {
        _secondary.SetValue(value);
    }
    public void SetSkillIcon(HUDSkillIcon[] list,int keyIndex, ASpell spell) {
        list[keyIndex].Initialize(spell, _mainController);
    }
    public void DamageText(bool damage,string text,Vector2 pos) {
        Instantiate(_damagetext, pos, Quaternion.identity).GetComponent<WORLDDamageText>().Initialize(damage,text);
    }
}