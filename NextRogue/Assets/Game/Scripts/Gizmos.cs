using UnityEngine;

public class Gizmos : MonoBehaviour
{
    [SerializeField]
    float[] circles = new float[4];
    private void OnDrawGizmos() {
        for (int i = 0; i < circles.Length; i++) {
            UnityEngine.Gizmos.DrawWireSphere(transform.position, circles[i]);
        }
    }
}
