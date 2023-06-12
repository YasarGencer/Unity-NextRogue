using UnityEngine;

public class TutorialSpellsManager : MonoBehaviour {
    [SerializeField] int startWith = 0;
    public void Initialize() {
    }
    public void OnPlayerInitialized() {
        MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Spells.DeleteMainSpells();
        if (startWith >= 0 && startWith <= 3)
            AddMainSpell(startWith);
    }

    public void AddMainSpell(int index) {
        MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Spells.SetSpell(
            keyIndex: index,
            spell: MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Stats.Spells[index]
            ); 
        MainManager.Instance.CanvasManager.Player_GUI_HUD.SkillIconVisibility(index, true);
    }

}
