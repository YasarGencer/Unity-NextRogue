using UnityEngine;
using UnityEngine.UI;

public class PauseScreenPanel_Options : AUI {
    [SerializeField] HUDSound HUDSound;
    public override void Initialize() {
        base.Initialize();
        Close();
    }
    public override void Open() {
        base.Open();
    }
    public override void Close(float time = 0) {
        base.Close(time);
    }
    public override void ButtonPressed(Image buttonSprite = null) {
        base.ButtonPressed(buttonSprite);
    }
}
