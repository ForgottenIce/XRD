using System.Collections;
using UnityEngine;

public class HeartBeatPlayer : MonoBehaviour
{
    [SerializeField] private Transform wardenTransform;
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource _audioSource;
    
    [Header("Heart Beat Parameters")]
    [SerializeField] private float maxVolume = 0.6f;
    [SerializeField] private float volumeMultiplier = 1f;
    [SerializeField] private float activationDistance = 10f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float minSecondsToWait = 0.3f;
    
    private bool _isSoundPlaying = false;


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        var distanceToWarden = Vector3.Distance(transform.position, wardenTransform.position);
        Debug.Log(distanceToWarden);
        if (!_isSoundPlaying && distanceToWarden < activationDistance)
        {
            _isSoundPlaying = true;
            StartCoroutine(PlayHeartBeatSound(distanceToWarden));
        }
    }
    
    private IEnumerator PlayHeartBeatSound(float distanceToWarden)
    {
        var heartBeatVolume = maxVolume / distanceToWarden * volumeMultiplier;
        if (heartBeatVolume > 0.6f) heartBeatVolume = 0.6f;
        _audioSource.volume = heartBeatVolume;
        _audioSource.pitch = Random.Range(0.94f, 1.08f);
        _audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        var secondsToWait = distanceToWarden * speedMultiplier;
        if (secondsToWait < minSecondsToWait) secondsToWait = minSecondsToWait;
        yield return new WaitForSeconds(secondsToWait);
        _isSoundPlaying = false;
    }
}
