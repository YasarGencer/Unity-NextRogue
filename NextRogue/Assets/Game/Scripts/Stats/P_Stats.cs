using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats/PlayerStats", order = 0)]
public class P_Stats : AStats {
    [Header("Spell Set")]
    public ASpell[] Spells;
    public bool SecondaryBar;
    public int SecondaryValue = 0; 
    [Header("Level")]
    public int Level;
    public int EXP, EXPRequired; 
    public override void Initialize() {
        base.Initialize();
        foreach (var item in Spells)
            item.IsInit = false; 
        if (SecondaryBar)
            SecondaryValue = 0;
        Level = 0;
        EXP = 0;
        EXPRequired = 100; 
    } 
    public void SetSecondaryValue(int value) {
        SecondaryValue = value;
        MainManager.Instance.CanvasManager.Player_GUI_HUD.SetSecondary(value);
    }
    public void SetTutorial(int index = 0) {
        if (GetTutorial() == true)
            return;
        PlayerPrefs.SetInt(Name + "tutorial", index);
        Initialize(); 
    }
    public bool GetTutorial() {
        return PlayerPrefs.GetInt(Name + "tutorial", 0) == 0? false: true;
    } 
}
