using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour {
    bool _isInit = false;
    protected Transform _player;
    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected Type _type;
    [SerializeField]
    protected Vector2 _amount;
    [SerializeField]
    protected float _range;
    [SerializeField]
    protected AudioClip _collectedClip;
    protected AudioSource _audioSource;
    protected enum Type {
        EXP,
        COIN,
    } 
    private IEnumerator Start() { 
        yield return new WaitForSeconds(.5f);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _audioSource = GetComponent<AudioSource>() as AudioSource;
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.volume = AudioManager.GetVolume(AudioManager.AudioVolume.sfx);
        _isInit = true;
    } 
    protected virtual void Update() {
        if (!_isInit)
            return;
        if (MainManager.Instance.GameManager.GamePaused) {
            _audioSource.Pause();
            return;
        }
        _audioSource.UnPause();
        var dist = Vector2.Distance(_player.position, transform.position);
        if(dist < _range)
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (!_isInit)
            return;
        var amount = _amount.x;
        if (_amount.y > _amount.x)
            amount = Random.Range(_amount.x, _amount.y); 
        switch (_type) {
            case Type.EXP:
                _player.GetComponent<P_MainController>().Level.GainEXP((int)amount);
                break;
            case Type.COIN:
                MainManager.Instance.EventManager.RunOnCoinChange((int)amount);
                break;
            default:
                break;
        }
        AudioManager.PlaySound(_collectedClip);
        Destroy(gameObject);
    } 
    protected virtual void OnTriggerStay2D(Collider2D collision) {
        if (!_isInit)
            return;
        var amount = _amount.x;
        if (_amount.y > _amount.x)
            amount = Random.Range(_amount.x, _amount.y);
        switch (_type) {
            case Type.EXP:
                _player.GetComponent<P_MainController>().Level.GainEXP((int)amount);
                break;
            case Type.COIN:
                MainManager.Instance.EventManager.RunOnCoinChange((int)amount);
                break;
            default:
                break;
        }
        AudioManager.PlaySound(_collectedClip);
        Destroy(gameObject);
    }
}
