using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    [SerializeField] Transform startPoint, endPoint;
    [SerializeField] Damager sawBlade;
    [SerializeField] int damage;
    [SerializeField] LayerMask layers;
    [SerializeField] float minReachTime, maxReachTime;
    [SerializeField] float minSize, maxSize;
    float speed;
    LineRenderer line;
    IDisposable updateRX;
    int distance = 3;
    float counter;
    private void Start() {
        Invoke("Initialize",1);
    }
    void Initialize() {  
        MainManager.Instance.EventManager.onGamePause += Pause;
        MainManager.Instance.EventManager.onGameUnPause += UnPause;
        sawBlade.transform.localScale *= UnityEngine.Random.Range(minSize, maxSize);
        sawBlade.Initialize(damage);
        sawBlade.GetComponent<Rigidbody2D>().AddTorque(350);
        endPoint.position = CheckForEndPoint();
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, startPoint.localPosition);
        line.SetPosition(1, endPoint.localPosition);
        speed = UnityEngine.Random.Range(minReachTime, maxReachTime);
        counter = 0;
        UnPause();
    }

    private void UnPause() {
        updateRX?.Dispose();
        updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
    }

    private void Pause() {
        updateRX?.Dispose();
    }
    void UpdateRX(long obj) {
        if(counter >= speed) {
            if (sawBlade.transform.position == startPoint.position)
                sawBlade.transform.DOLocalMove(endPoint.localPosition, speed).SetEase(Ease.Linear);
            else
                sawBlade.transform.DOLocalMove(startPoint.localPosition, speed).SetEase(Ease.Linear);
            counter = 0;
        }
        counter += Time.deltaTime * Time.timeScale;
    }
    Vector2 CheckForEndPoint() {
        List<Vector2Int> directions = Direction2D.EightDirectionList;
        directions = directions.OrderBy(a => Guid.NewGuid()).ToList();


        for (int i = 0; i < distance; i++) {
            foreach (var item in directions) {
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, item, distance - i, layers);
                if(hit.Length == 0)
                    return new Vector2(transform.position.x + item.x * (distance - i), transform.position.y + item.y * (distance - i));
            }
        }
        gameObject.SetActive(false);
        return transform.position;  
    }
}
