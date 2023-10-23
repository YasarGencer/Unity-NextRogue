using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class OpenPanelAndSelectFirst : MonoBehaviour
{
    public GameObject firstSelectedObject; // Ýlk seçilen nesneyi buraya sürükleyin

    private void OnEnable()
    {
        EventSystem eventSystem = EventSystem.current;

        eventSystem.SetSelectedGameObject(null);
        // Ýlk seçilen nesneyi ayarlayýn
        eventSystem.SetSelectedGameObject(firstSelectedObject);
    }
    private void Start()
    {
      
        // EventSystem'i alýn
        EventSystem eventSystem = EventSystem.current;

        eventSystem.SetSelectedGameObject(null);
        // Ýlk seçilen nesneyi ayarlayýn
        eventSystem.SetSelectedGameObject(firstSelectedObject);

    }
    public async void SelectThisObject(GameObject selectedGameObject)
    {
        await Task.Delay(50);
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(selectedGameObject);
    }
}