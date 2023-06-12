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
        _mainController = MainManager.Instance.Player.GetComponentInChildren<P_MainController>();
        Description.Hide();
        SkillIconsVisibility(true); 
    }
    public override void Open() {
        base.Open();
    }
    public override void Close(float time = 0) {
        base.Close(time);
    }
    protected override void OnPlayerInitialized() {
        SetKeys();
        _health.Initialize(_mainController.Stats.Health, _mainController.Stats.MaxHealth, 0);
        _level.Initialize(_mainController.Stats.EXP, _mainController.Stats.EXPRequired, _mainController.Stats.Level);
        if (_mainController.Stats.SecondaryBar)
            InitializeSecondary(0, 1, 0);
    }
    void SetKeys() {
        var text = MainManager.Instance.InputManager    .GetKeyInfo();
        var textPart = text.Split("/");
        for (int i = 0; i < textPart.Length - 1; i++)
            spellIconList[i].SetKey(textPart[i]);  
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
    public void SkillIconsVisibility(bool value) {
        foreach (var item in spellIconList)
            SkillIconVisibility(item, value); 
    } 
    public void SkillIconVisibility(int icon, bool value) {
        spellIconList[icon].gameObject.SetActive(value);
    }
    public void SkillIconVisibility(HUDSkillIcon icon, bool value) {
        icon.gameObject.SetActive(value);
    }
}