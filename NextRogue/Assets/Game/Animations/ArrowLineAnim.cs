using DG.Tweening;
 using System.Collections.Generic; 
using System.Threading.Tasks;
using UnityEngine; 

public class ArrowLineAnim : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> arrows;
     
    private async void Start() { 
        foreach (var item in arrows) {
            Color color = item.color;
            color.a = 0;
            item.color = color;
        }
        for (int i = 0; i < arrows.Count; i++) {
            if (arrows[i] != null) {
                Color color = arrows[i].color;
                color.a = 1;
                arrows[i].DOColor(color, .5f);
                await Task.Delay(50);
            }
        }
        await Task.Delay(1500);
        for (int i = 0; i < arrows.Count; i++) {
            if (arrows[i] != null) {
                Color color = arrows[i].color;
                color.a = 0;
                arrows[i].DOColor(color, .05f);
                await Task.Delay(10);
            }
        }
    } 
}
