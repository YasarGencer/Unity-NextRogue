using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cinemachine.Utility;

public class GroundSlam_Projectile : ANP_Projectile
{
    public GameObject Particle;

    private SpriteRenderer maxAttackSprite;
    private SpriteRenderer currentAttackSprite;
    private Material golemSkillMaterial;
    private Vector3 oldScale;
    private bool smashing;

    private float duration = 2f;
    private float timer = 0f;
    private bool soundPlayed;

    IDisposable _moveRX;
    public override void Initialize(Vector3 targetPos, float damage, float time, float speed, DOTInfo dotInfo)
    {
        base.Initialize(targetPos, damage, time, speed, dotInfo);
        //base.Initialize(mainController);

        _moveRX?.Dispose();

        if (maxAttackSprite == null)
            maxAttackSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (currentAttackSprite == null)
            currentAttackSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        if (golemSkillMaterial == null)
            golemSkillMaterial = currentAttackSprite.material;
        currentAttackSprite.enabled = false;
        maxAttackSprite.enabled = false;
        oldScale = currentAttackSprite.transform.localScale;

        _moveRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(MoveRX);
        smashing = true;
    }
    public void MoveRX(long obj)
    {
        StoneSmash();
    }
    void StoneSmash()
    {
        currentAttackSprite.enabled = true;
        maxAttackSprite.enabled = true;
        if (smashing == true)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            currentAttackSprite.transform.localScale = Vector3.Lerp(oldScale, maxAttackSprite.transform.localScale, t);
            golemSkillMaterial.SetFloat("_percent", currentAttackSprite.transform.localScale.x/10f);
            if (timer >= 2 && soundPlayed == false)
            {
                PlaySound();
                //AudioManager.PlaySound(Sound, _mainController.transform, AudioManager.AudioVolume.sfx, stopAudio);
                soundPlayed = true;
            }
            if (timer >= duration)
            {
                timer = 0f;
                currentAttackSprite.transform.localScale = oldScale;
                golemSkillMaterial.SetFloat("_percent", currentAttackSprite.transform.localScale.x);
                currentAttackSprite.enabled = false;
                maxAttackSprite.enabled = false;
                smashing = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                gameObject.GetComponent<Damager>().Initialize(_damage,_dotInfo);  
                _moveRX?.Dispose();
                //buraya golem playerin colliderýnda ise hasar ver knocpback at
            }
        }
    }
    protected override void OnGamePause()
    {
        base.OnGamePause();
        _moveRX?.Dispose();
    }
    protected override void OnGameUnPause()
    {
        base.OnGameUnPause();
        _moveRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(MoveRX);
    }
}
