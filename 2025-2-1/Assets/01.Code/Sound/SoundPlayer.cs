using DG.Tweening;
using RuddnjsPool;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour, IPoolable
{
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private AudioMixerGroup _sfxGroup, _musicGroup;
    [SerializeField] private string _poolName;
    public string PoolName => _poolName;

    private AudioSource _audioSource;
    public GameObject ObjectPrefab => gameObject;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundSO data)
    {
        if (data.audioType == AudioType.SFX)
        {
            _audioSource.outputAudioMixerGroup = _sfxGroup; ;
        }
        else if (data.audioType == AudioType.Music)
        {
            _audioSource.outputAudioMixerGroup = _musicGroup;
        }

        _audioSource.volume = data.volume;
        _audioSource.pitch = data.basePitch;

        if (data.randomizePitch)
        {
            _audioSource.pitch += Random.Range(-data.randomPitchModifier, data.randomPitchModifier);
        }

        _audioSource.clip = data.clip;

        _audioSource.loop = data.loop;

        if (!data.loop)
        {
            float time = _audioSource.clip.length + 0.2f;
            DOVirtual.DelayedCall(time, () => poolManager.Push(this));
        }

        _audioSource.Play();
    }

    public void StopAndGoToPool()
    {
        _audioSource.Stop();
        poolManager.Push(this);
    }
    
    #region Pool

    [field:SerializeField] public PoolTypeSO PoolType { get; set; }
    public GameObject GameObject => gameObject;

    private Pool _myPool;
    
    public void SetUpPool(Pool pool)
    {
        _myPool = pool;
    }

    public void ResetItem()
    {
    }
    #endregion
}
