using DG.Tweening;
using UnityEngine; 

public class Canvas_Skill_Selection : AUI
{
    [SerializeField] AudioClip _selectedSkill;
    Transform _child1;
     
    [SerializeField]
    HUDSlotSelection[] _slotSelection; 
    ASpell _spell;

    GameObject _shopFront;
    public override void Initialize() {
        base.Initialize(); 
        _child1 = transform.GetChild(1);
        _child1.gameObject.SetActive(false);
        Close();
    }
    public void Open(GameObject gameObject, ShopItem shopitem) {
        this.gameObject.SetActive(true); 
        _child.gameObject.SetActive(false);
        _child1.gameObject.SetActive(true);
        _shopFront = gameObject;

        if (_openClip)
            AudioManager.PlaySound(_openClip, null, AudioManager.AudioVolume.ui, false);

        //alpha 
        GetComponent<CanvasGroup>().alpha = 0; 
        GetComponent<CanvasGroup>().DOFade(1,1); 

        //headers
        var header = _child1.GetChild(0).GetComponent<RectTransform>();
        var header1 = _child1.GetChild(1).GetComponent<RectTransform>();
        var value = 0;
        DOTween.To(() => value, x => value = x, 1250, 1)
        .OnUpdate(() => {
            header.sizeDelta = new Vector2(value, header.sizeDelta.y);
            header1.sizeDelta = new Vector2(value, header.sizeDelta.y);
        }).SetEase(Ease.InCirc);
        //slots
        var slots = _child1.GetChild(2);
        slots.localPosition = new(0, -100, 0);
        slots.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InCirc);
        var alpha = slots.GetComponent<CanvasGroup>();
        float alphaValue = 0;
        alpha.alpha = 0;
        DOTween.To(() => alphaValue, x => alphaValue = x, 1, 1)
        .OnUpdate(() => {
            alpha.alpha = alphaValue;
        }).SetEase(Ease.InCirc);


        _spell = shopitem.Spell;
        if (_selectedSkill)
            AudioManager.PlaySound(_selectedSkill, null, AudioManager.AudioVolume.ui, false);
        SetSlots(); 

    }
    public override void Close(float time = 0) {
        base.Close(time);
    }
    void SetSlots() {
        for (int i = 0; i < 5; i++) {
            ASpell spell = MainManager.Instance.Player.GetComponentInChildren<P_MainController>()
                .Spells.GetSpell(i + 4);
            if (spell != null)
                _slotSelection[i].Initialize(spell);
            else
                _slotSelection[i].Initialize();
        }
    } 
    public void Buy(int value) {
        if (_selectedSkill)
            AudioManager.PlaySound(_selectedSkill, null, AudioManager.AudioVolume.ui, false);
        MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Spells.SetSpell(value + 4, _spell);
        MainManager.Instance.EventManager.RunOnGameUnPuase();
        _shopFront.GetComponent<ShopItemSlot>().Buy();

    }
}
