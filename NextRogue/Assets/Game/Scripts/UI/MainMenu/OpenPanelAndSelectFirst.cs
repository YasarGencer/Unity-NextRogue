using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class OpenPanelAndSelectFirst : MonoBehaviour
{
    public GameObject firstSelectedObject; // �lk se�ilen nesneyi buraya s�r�kleyin

    private void OnEnable()
    {
        EventSystem eventSystem = EventSystem.current;

        eventSystem.SetSelectedGameObject(null);
        // �lk se�ilen nesneyi ayarlay�n
        eventSystem.SetSelectedGameObject(firstSelectedObject);
    }
    private void Start()
    {
      
        // EventSystem'i al�n
        EventSystem eventSystem = EventSystem.current;

        eventSystem.SetSelectedGameObject(null);
        // �lk se�ilen nesneyi ayarlay�n
        eventSystem.SetSelectedGameObject(firstSelectedObject);

    }
    public async void SelectThisObject(GameObject selectedGameObject)
    {
        await Task.Delay(50);
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(selectedGameObject);
    }
}