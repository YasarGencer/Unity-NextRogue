using UnityEngine;

public class TutorialSpellGiver : MonoBehaviour
{
    [SerializeField] int spellId; 
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
            GameObject.FindObjectOfType<TutorialManager>().TutorialSpellsManager.AddMainSpell(spellId); 
    }
}
