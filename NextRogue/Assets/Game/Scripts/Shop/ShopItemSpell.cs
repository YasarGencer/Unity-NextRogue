using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSpell : ShopItem {
    public override void Initialize() {
        var multiplier = MainManager.Instance.LevelManager.GetLevel() == 0 ? 1 : MainManager.Instance.LevelManager.GetLevel();
        Price = 100 * multiplier;
        base.Initialize();
    }
    public override void Interact(ShopItemSlot itemSlot) {
        base.Interact(itemSlot);
        MainManager.Instance.CanvasManager.OpenSkillSelection(itemSlot);
    }
}
