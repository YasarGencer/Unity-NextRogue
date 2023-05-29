using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{ 
    Transform _player; 
    bool follow;
    public bool Follow { get { return follow; } set { follow = value; } }
    public void Initialize() {
        follow = true;
        _player = MainManager.Instance.Player.GetChild(0);
    }
    private void Update() {
        if (follow == false)
            return;
        var range = AccessibilityManager.GetValue(AccessibilityManager.AccessibilityType.cameramouserange);
        var maousePos = MainManager.Instance.InputManager.GetMouseWolrdPos();
        var targetPos = (_player.position + maousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -range + _player.position.x, range + _player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -range + _player.position.y, range + _player.position.y);

        transform.position = targetPos;
    }
}
