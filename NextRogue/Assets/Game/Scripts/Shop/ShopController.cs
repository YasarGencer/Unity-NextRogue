using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] ShopItemSlot[] slots;
    [SerializeField] GameObject SpellItem, PotionItem;

    private void Start() {
        //var spells = MainManager.Instance.GameManager.AllSpells.GetRandomSpell(slots.Length);
        //for (int i = 0; i < slots.Length; i++)
        //    if (spells[i] != null)
        //        slots[i].Initialize(spells[i], ShopItem);

        slots[0].Initialize(SpellItem);
        slots[1].Initialize(SpellItem);
        slots[2].Initialize(PotionItem);
    }
}
