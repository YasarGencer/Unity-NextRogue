using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public ASpell Spell { get; private set; }
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI name, price;
    public int Price { get; private set; }
    public Animator Animator { get; private set; }

    public void Initialize(ASpell spell) {
        Animator = GetComponent<Animator>();
        Spell = spell;
        icon.sprite = spell.Icon;
        name.SetText(spell.Name);
        var multiplier = MainManager.Instance.LevelManager.GetLevel() == 0 ? 1 : MainManager.Instance.LevelManager.GetLevel();
        Price = 100 * multiplier;
        price.SetText(Price.ToString() + " g");
    } 
}
