using UnityEngine;

public class AccessibilityManager : MonoBehaviour
{
    public static float GetValue(AccessibilityType accessibilityType) {
        float def = new();
        switch (accessibilityType) {
            case AccessibilityType.cameramouserange:
                def = 1;
                break; 
        }
        return PlayerPrefs.GetFloat(GetString(accessibilityType), def);
    }
    static string GetString(AccessibilityType accessibilityType) {
        switch (accessibilityType) {
            case AccessibilityType.cameramouserange:
                return "ACCESSIBILITY-CAMERAMOUSERANGE";
            default:
                break;
        } 
        return "a";
    }
    public static void SetValue(AccessibilityType accessibilityType, float value) {
        PlayerPrefs.SetFloat(GetString(accessibilityType), value);
    }
    public enum AccessibilityType {
        cameramouserange,
    }
}
