using UnityEngine;

public class Collectable : MonoBehaviour {
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
    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").transform; 
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = AudioManager.GetVolume(AudioManager.AudioVolume.sfx);
    }
    protected virtual void Update() {
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
        var amount = _amount.x;
        if (_amount.y > _amount.x)
            amount = Random.Range(_amount.x, _amount.y); 
        switch (_type) {
            case Type.EXP:
                _player.GetComponent<P_MainController>().Level.GainEXP((int)amount);
                break;
            case Type.COIN: 
                break;
            default:
                break;
        }
        AudioManager.PlaySound(_collectedClip);
        Destroy(gameObject);
    }
}
