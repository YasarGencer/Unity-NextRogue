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
            // Eðer cursor etkinse, "E" tuþuna basarak devre dýþý býrakabilirsiniz
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                isCursorActive = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        else
        {
            // Eðer cursor etkin deðilse, fare pozisyonunu kontrol ederek tekrar etkinleþtirebilirsiniz
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
