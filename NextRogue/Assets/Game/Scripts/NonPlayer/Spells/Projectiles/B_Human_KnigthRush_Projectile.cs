using DG.Tweening;
 using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class B_Human_KnigthRush_Projectile: ANP_Projectile
{ 
    [SerializeField] List<Damager> _leftKnights;
    [SerializeField] List<Damager> _rightKnights;
    public override async void Initialize(Vector3 targetPos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(targetPos, 0, time, 0, dotInfo); 

        foreach (var item in _leftKnights) {
            item.Initialize(damage, dotInfo);
            var sprite = item.gameObject.GetComponent<SpriteRenderer>();
            var collider = item.gameObject.GetComponent<BoxCollider2D>();
            var shadow = item.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            item.gameObject.transform.DOLocalMove(new Vector3(7, item.gameObject.transform.localPosition.y, item.gameObject.transform.localPosition.z), 4.5f).SetEase(Ease.InQuart).OnComplete(() => {
                sprite.DOFade(0, .25f).OnComplete(()=> Destroy(sprite.gameObject));
                shadow.DOFade(0, .20f);
            });
            sprite.DOFade(0, 0);
            shadow.DOFade(0, 0);
            collider.enabled = false;
            sprite.DOFade(1, 1).OnComplete(()=> collider.enabled = true);
            shadow.DOFade(.75f, 1);
        }
        foreach (var item in _rightKnights) {
            item.Initialize(damage, dotInfo);
            var sprite = item.gameObject.GetComponent<SpriteRenderer>();
            var collider = item.gameObject.GetComponent<BoxCollider2D>();
            var shadow = item.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            item.gameObject.transform.DOLocalMove(new Vector3(-7, item.gameObject.transform.localPosition.y, item.gameObject.transform.localPosition.z), 4.5f).SetEase(Ease.InQuart).OnComplete(() => {
                sprite.DOFade(0, .25f).OnComplete(() => Destroy(sprite.gameObject));
                shadow.DOFade(0, .20f);
            });
            sprite.DOFade(0, 0);
            shadow.DOFade(0, 0);
            collider.enabled = false;
            sprite.DOFade(1, 1).OnComplete(() => collider.enabled = true);
            sprite.DOFade(1, 1);
            shadow.DOFade(.75f, 1);
        }
        await Task.Delay(100);
        PlaySound();
    } 
}
