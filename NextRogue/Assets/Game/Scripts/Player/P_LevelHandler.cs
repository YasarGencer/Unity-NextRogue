using UnityEngine;

public class P_LevelHandler : MonoBehaviour {
    bool _isInit;
    P_MainController _mainController;
    public void Initialize(P_MainController mainController) {
        if (_isInit)
            return;
        _isInit = true;

        _mainController = mainController;
        //GainEXP(175);
    }
    public void GainEXP(int value) {
        _mainController.Stats.EXP += value;
        if(_mainController.Stats.EXP >= _mainController.Stats.EXPRequired)
            LevelUp();
        MainManager.Instance.CanvasManager.Player_GUI_HUD.SetLevel();
    }
    public void LevelUp() {
        _mainController.Stats.EXP -= _mainController.Stats.EXPRequired;
        _mainController.Stats.Level++;
        MainManager.Instance.CanvasManager.OpenSkillSelection();
    }
}
