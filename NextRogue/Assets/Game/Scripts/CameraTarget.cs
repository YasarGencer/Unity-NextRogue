using UnityEngine;

public class CameraTarget : MonoBehaviour
{ 
    public Transform Player { get; set; } 
    bool follow;
    public bool Follow { get { return follow; } set { follow = value; } }
    public void Initialize() {
        follow = true;
        Player = MainManager.Instance.Player.GetChild(0);
    }
    private void Update() {
        if (follow == false)
            return; 
        var range = AccessibilityManager.GetValue(AccessibilityManager.AccessibilityType.cameramouserange);
        var maousePos = MainManager.Instance.InputManager.GetMouseWorldPos();
        var targetPos = (Player.position + maousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -range + Player.position.x, range + Player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -range + Player.position.y, range + Player.position.y);

        transform.position = targetPos;
    }
}
