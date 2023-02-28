using TMPro;
using UnityEngine;

public class WORLDDamageText : MonoBehaviour
{
    [SerializeField]
    Color _colorDamage, _colorHeal;
    public TextMeshProUGUI Text;
    public void Initialize(bool damage,string text) {
        Destroy(gameObject, 1);
        transform.position += new Vector3(Random.Range(0,.5f), Random.Range(0, .5f),0);
        if (damage) {
            Text.SetText("-" + text);
            Text.color = _colorDamage;
        }
        else {
            Text.SetText("+" + text);
            Text.color = _colorHeal;
        }
    }
}
