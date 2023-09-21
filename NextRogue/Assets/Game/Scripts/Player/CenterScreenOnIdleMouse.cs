using UnityEngine;
using UnityEngine.InputSystem;


public class CenterScreenOnIdleMouse : MonoBehaviour
{
    private bool isCursorActive = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (isCursorActive)
        {
            // E�er cursor etkinse, "E" tu�una basarak devre d��� b�rakabilirsiniz
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                isCursorActive = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        else
        {
            // E�er cursor etkin de�ilse, fare pozisyonunu kontrol ederek tekrar etkinle�tirebilirsiniz
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            if (mousePosition != Vector2.zero)
            {
                isCursorActive = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
