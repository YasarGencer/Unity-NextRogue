using TMPro;
using UnityEngine; 

public class ShopItem : MonoBehaviour
{ 
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] ShopItemShake _shake; 
    public ShopItemShake Shake { get { return _shake; } }
    public int Price { get; protected set; }  

    public virtual void Initialize() { 
        Price = 100;
        price.SetText(Price.ToString() + " g");
    } 
    public virtual void Interact(ShopItemSlot itemSlot) {
    } 
}
