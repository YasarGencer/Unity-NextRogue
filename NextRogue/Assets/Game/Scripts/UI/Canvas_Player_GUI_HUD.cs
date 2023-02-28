using UnityEngine;

public class Canvas_Player_GUI_HUD : AUI
{ 
    P_MainController _mainController;

    [SerializeField]
    HUDHealthBar _health;
    [SerializeField]
    GameObject _damagetext;

    public HUDSkillIcon[] spellIconList;
    public string[] spellKeys;

    [Header("SPELL DESCRIPTION")]
    public HUDSkillDescription Description; 
    public override void Initialize() {
        base.Initialize();
        _mainController = GameObject.FindGameObjectWithTag("Player").GetComponent<P_MainController>();
        
        Description.Hide(); 
        _health.Initialize(_mainController);

        for (int i = 0; i < spellKeys.Length; i++) {
            spellIconList[i].SetKey(spellKeys[i]);
        }
        Close();
    }
    public override void Open() {
        base.Open();
    }
    public override void Close() {
        base.Close();
    }

    public void SetHealth(float value) {
        _health.SetHealth(value);
    }
    public void SetSkillIcon(HUDSkillIcon[] list,int keyIndex, ASpell spell) {
        list[keyIndex].Initialize(spell, _mainController);
    }
    public void DamageText(bool damage,string text,Vector2 pos) {
        Instantiate(_damagetext, pos, Quaternion.identity).GetComponent<WORLDDamageText>().Initialize(damage,text);
    }
}