using UnityEngine;

public class PlaySoundEvent : MonoBehaviour // DO NOT USE THIS COMPONENT OUTSIDE OF TESTING
{
    [SerializeField] private SoundEventEmitter SoundEventEmitter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        SoundEventEmitter.EmitSoundEvent(new(gameObject.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
