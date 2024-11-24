using System.Collections;
using UnityEngine;

public class FootStepPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] audioClips;
    
    [Header("Foot Step Parameters")]
    [SerializeField] private float velocityThreshold = 1f;
    
    private bool _isSoundPlaying = false;
    private Vector3 _previousPosition;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _previousPosition = transform.position;
    }

    
    private void Update()
    {
        var currentPosition = transform.position;
        if (!_isSoundPlaying && Vector3.Distance(currentPosition, _previousPosition) > velocityThreshold)
        {
            _isSoundPlaying = true;
            StartCoroutine(PlayFootStepSound());
        }
        _previousPosition = currentPosition;
    }

    private IEnumerator PlayFootStepSound()
    {
        _audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        yield return new WaitForSeconds(0.4f);
        _isSoundPlaying = false;
    }
}
