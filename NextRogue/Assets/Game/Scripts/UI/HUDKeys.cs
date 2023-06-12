using System.Collections.Generic;
using UnityEngine;

public class HUDKeys : MonoBehaviour
{
    [SerializeField] Transform _keysParent;
    [SerializeField] GameObject _keyBindings;
    [SerializeField] List<string> _keyNames;
    private void Start() {
        if (_keysParent.childCount > 1)
            return;

        var skills = new List<GameObject>(); 
        for (int i = 0; i < 9; i++) {
            skills.Add(Instantiate(_keyBindings, _keysParent));
        }
        var text = MainManager.Instance?.InputManager.GetKeyInfo(); 
        var textPart = text.Split("/");
        for (int i = 0; i < textPart.Length - 1; i++)
            skills[i].GetComponent<HUDKeyBinding>().Initialize(_keyNames[i], textPart[i]);
        Instantiate(_keyBindings, _keysParent).GetComponent<HUDKeyBinding>().Initialize("interact", "e");
    } 
} 