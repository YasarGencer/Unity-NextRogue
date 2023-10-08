using System; 
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopItemSlot : AInteract
{
    [SerializeField] AudioClip _notEnoughMoney;
    public ShopItem ShopItem { get ; private set; } 
    public void Initialize(SpellHolder spell, GameObject item) {
        ShopItem = Instantiate(item, this.transform).GetComponent<ShopItem>();
        ShopItem.Initialize(spell); 
    }

    protected override void Interact() {
        if (CanBuy()) {
            base.Interact();
            GameObject.FindObjectOfType<CanvasManager>().OpenSkillSelection(this.gameObject, ShopItem);
        } else {
            AudioManager.PlaySound(_notEnoughMoney, transform, AudioManager.AudioVolume.sfx);
        }
    }
    public void Buy() {
        Destroy(transform.GetComponentInChildren<ShopItem>().gameObject);  
        GameObject.FindObjectOfType<CanvasManager>().TextPopUp(false);
        MainManager.Instance.EventManager.RunOnCoinChange(-ShopItem.Price);
    }
    protected override void Info(bool value) { 
        if (CanBuy()) {
            InfoText("press e to buy");
        } else {
            InfoText("not enough coins");
        }
        base.Info(value);

        //if (ShopItem.Shake == null)
        //    return;
        //if (value) {
        //    ShopItem.Shake.StartMovement();
        //} else { 
        //    ShopItem.Shake.StopMovement();
        //}
    } 
    bool CanBuy() {
        return MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Stats.Coin >= ShopItem.Price;
    }
}
