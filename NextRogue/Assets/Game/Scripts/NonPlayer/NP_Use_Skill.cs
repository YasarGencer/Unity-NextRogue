using UniRx; 

public class NP_Use_Skill : ANP_Use_Skill
{ 
    public override void Initialize(ANP_MainController mainController) { 
        base.Initialize(mainController);
    }  
    protected override void PlaySpell(int index) {
        base.PlaySpell(index);
    }
    protected override void UpdateRX(long obj) { 
        base.UpdateRX(obj);
    }
    protected override void OnGamePause() {
        _updateRX?.Dispose();
    }
    protected override void OnGameUnPause() { 
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
    }
}
