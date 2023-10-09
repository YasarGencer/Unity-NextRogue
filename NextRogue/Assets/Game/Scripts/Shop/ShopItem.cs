using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public ASpell Spell { get; private set; }
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI _name, price;
    [SerializeField] ShopItemShake _shake;
    public ShopItemShake Shake { get { return _shake; } }
    public int Price { get; private set; }  

    public void Initialize(SpellHolder spell) {
        //Shake = shake;
        Spell = spell.IsChallangeDone == true? spell.EnhancedSpell: spell.Spell;
        //defBG.gameObject.SetActive(!spell.IsChallangeDone);
        //enhBG.gameObject.SetActive(spell.IsChallangeDone);
        icon.sprite = Spell.ShopIcon;
        _name.SetText(Spell.Name);
        var multiplier = MainManager.Instance.LevelManager.GetLevel() == 0 ? 1 : MainManager.Instance.LevelManager.GetLevel();
        Price = 100 * multiplier;
        price.SetText(Price.ToString() + " g");
    } 
}
