using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public ShopItemType MyShopItemType; 
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] ShopItemShake _shake;
    public ASpell Spell { get; private set; }
    public ShopItemShake Shake { get { return _shake; } }
    public int Price { get; private set; }  

    public void Initialize() {
        switch (MyShopItemType) {
            case ShopItemType.SPELL:
                var multiplier = MainManager.Instance.LevelManager.GetLevel() == 0 ? 1 : MainManager.Instance.LevelManager.GetLevel();
                Price = 100 * multiplier;
                break;
            case ShopItemType.HEALTH_POTION:
                Price = 100;
                break;
            default:
                break;
        }
        price.SetText(Price.ToString() + " g");
    } 
    public void Interact(GameObject gameObject) {
        switch (MyShopItemType) {
            case ShopItemType.SPELL:
                MainManager.Instance.CanvasManager.OpenSkillSelection(gameObject);
                break;
            case ShopItemType.HEALTH_POTION:
                MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Health.GainHealth(1000);
                gameObject.GetComponent<ShopItemSlot>().Buy();
                break;
            default:
                break;
        }
    }
    public enum ShopItemType {
        SPELL,
        HEALTH_POTION
    }
}
