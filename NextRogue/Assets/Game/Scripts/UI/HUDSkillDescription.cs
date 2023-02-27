using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSkillDescription : MonoBehaviour {
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Desc;
    public TextMeshProUGUI Cooldown;
    public Image Icon;

    public void Show(ASpell spell) {
        gameObject.SetActive(true);

        Name.text = spell.Name;
        Desc.text = spell.Description;
        Cooldown.text = spell.CooldownTime.ToString();
        Icon.sprite = spell.Icon;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
