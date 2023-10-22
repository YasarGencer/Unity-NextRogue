using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemHealthPotıon : ShopItem {
    public override void Initialize() {
        Price = 100;
        base.Initialize();
    }
    public override void Interact(ShopItemSlot itemSlot) {
        base.Interact(itemSlot);
        MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Health.GainHealth(1000);
        itemSlot.Buy();
    }
}
