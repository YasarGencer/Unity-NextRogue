using DG.Tweening;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class Chest : AInteract {
    [SerializeField] Transform spriteClosed, spriteOpened;
    [Space(5f)]
    [SerializeField] List<GameObject> loot;
    [SerializeField] AudioClip trapSound;
    [SerializeField] Animator trapParticle;
    [SerializeField, Range(0, 5)] int itemDropCount;
    [Space(5f)]
    [SerializeField] List<GameObject> trap;
    [SerializeField] AudioClip lootSound;
    [SerializeField] Animator lootParticle;
    [SerializeField, Range(0, 100)] float trapChance; 
    protected override void OnStart() {
        base.OnStart();
        spriteClosed.gameObject.SetActive(true);
        spriteOpened.gameObject.SetActive(false);
        trapParticle?.gameObject.SetActive(false);
        lootParticle?.gameObject.SetActive(false);
    } 
    protected override void Interact() { 
        base.Interact();
        _canUse = false;
        spriteClosed.gameObject.SetActive(false);
        spriteOpened.gameObject.SetActive(true);
        transform.DOPunchScale(Vector3.one * .5f, .4f);

        var chance = Random.Range(0, 100);
        if (trapChance > chance) {
            Trap();
        } else {
            Loot();
        }
    }
    void Trap() {
        trapParticle?.gameObject.SetActive(true);
        AudioManager.PlaySound(trapSound, transform, AudioManager.AudioVolume.environment);
        AudioManager.PlaySound(lootSound, transform, AudioManager.AudioVolume.environment);

        for (int i = 0; i < trap.Count; i++) {
            var item = Instantiate(trap[i], transform);
            item.transform.localPosition = Vector3.zero; 
            int xValue = Random.Range(0, 2);
            int yValue = Random.Range(0, 2);
            if (xValue == 0)
                xValue = -1;
            if (yValue == 0)
                yValue = -1;
            item.transform.DOLocalMove(new Vector3(Random.value * xValue * 3, Random.value * yValue * 3, 0), 1.5f).OnComplete(()=> item.transform.parent = MainManager.Instance.Enemies);
        }
    }
    void Loot() {
        lootParticle?.gameObject.SetActive(true);
        AudioManager.PlaySound(lootSound, transform, AudioManager.AudioVolume.environment);
        for (int i = 0; i < itemDropCount; i++) {
            var item = Instantiate(loot[Random.Range(0, loot.Count)], transform);
            item.transform.localPosition = Vector3.zero;
            int xValue = Random.Range(0, 2);
            int yValue = Random.Range(0, 2);
            if (xValue == 0)
                xValue = -1;
            if(yValue == 0)
                yValue = -1;
            item.transform.DOLocalMove(new Vector3(Random.value * xValue, Random.value * yValue, 0), .25f); 
        }
    } 
}