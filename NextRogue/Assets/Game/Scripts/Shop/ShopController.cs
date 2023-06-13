using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] ShopItemSlot[] slots;
    [SerializeField] GameObject ShopItem;
    [SerializeField] bool isFirstShop;

    private void Start() {
        var spells = MainManager.Instance.GameManager.AllSpells.GetRandomSpell(slots.Length);
        for (int i = 0; i < slots.Length; i++)
            if (spells[i] != null)
                slots[i].Initialize(spells[i], ShopItem, isFirstShop); 
    }
}
