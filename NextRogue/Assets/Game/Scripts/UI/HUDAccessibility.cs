using UnityEngine;
using UnityEngine.UI;

public class HUDAccessibility : MonoBehaviour
{
    [SerializeField] Slider cameraMouseRange;
    private void Start() {

        cameraMouseRange.value = AccessibilityManager.GetValue(AccessibilityManager.AccessibilityType.cameramouserange);


        cameraMouseRange.onValueChanged.AddListener(delegate { ValueChange(cameraMouseRange, AccessibilityManager.AccessibilityType.cameramouserange); }); 

    }
    void ValueChange(Slider slider,  AccessibilityManager.AccessibilityType accessibilityType) {
        AccessibilityManager.SetValue(accessibilityType, slider.value);
    }
}
