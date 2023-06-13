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

    public void Initialize(ASpell spell, bool isFirstShop) {
        Animator = GetComponent<Animator>();
        Spell = spell;
        icon.sprite = spell.Icon;
        name.SetText(spell.Name);
        Price = isFirstShop? 100: spell.Price;
        price.SetText(Price.ToString() + " g");
    } 
}
