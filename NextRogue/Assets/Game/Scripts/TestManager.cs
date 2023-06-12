using UnityEngine;

public class TestManager : MonoBehaviour
{
    public ASpell[] Spells;
    public void Initialize() {
        MainManager.Instance.EventManager.onPlayerInitialized += PlayerInitialized;
    }
    void PlayerInitialized() { 
        for (int i = 0; i < Spells.Length; i++) {
            MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Spells.SetSpell(i + 4, Spells[i]);
        }
    }
}
