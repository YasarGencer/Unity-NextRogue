using System;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "Overclock", menuName = "ScriptableObjects/Spells/Overclock")]
public class _Overclock : ASpell {
    [SerializeField] bool _isEnhanced;
    [SerializeField] float _timer = 2;
    [SerializeField] float _speedBoost = 2;
    float _time;
    IDisposable _updateRX;


    public int coolDownedSpellCounter = 0;
    public override void Initialize(P_MainController mainController, int value) {
        base.Initialize(mainController, value);
    }

    public override void ActivateSpell() {
        coolDownedSpellCounter = 0;
        base.ActivateSpell();
        Destroy(Instantiate(Spell, _mainController.transform), 2f);
        foreach (var item in GameObject.FindGameObjectWithTag("Player").GetComponent<P_MainController>().Spells.SpellList)
        {
            
            if (item && item.IsInit && item != this)
            {
                item.RetrieveCooldown();
                coolDownedSpellCounter++;
            }
        }
        if (coolDownedSpellCounter==8) 
        {
            MainManager.Instance.GameManager.ChallangeManager.RegisterChallangeDone(SpellType.Overclock);
            Debug.Log("oldu");
        }
           
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().TakeUntilDisable(_mainController).Subscribe(UpdateRX); 
        _mainController.Stats.Speed *= _speedBoost;
        _time = _timer;
    }
    void UpdateRX(long obj) {
        _time -= Time.deltaTime;
        if (_time <= 0) {
            _updateRX?.Dispose();
            _mainController.Stats.Speed /= _speedBoost;
        }
    }
    protected override void OnGamePause() {
        base.OnGamePause();
    }
    protected override void OnGameUnPause() {
        base.OnGameUnPause();
    }

                
     
}