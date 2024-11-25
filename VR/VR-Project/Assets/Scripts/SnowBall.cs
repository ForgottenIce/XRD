using System.Collections;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    
    [SerializeField] private SoundEventEmitter soundEventEmitter;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            soundEventEmitter.EmitSoundEvent(new SoundEvent(transform.position));
            Destroy(gameObject);
        }
    }

    public void StartDespawnTimer()
    {
        StartCoroutine(DelayedDespawn());
    }

    IEnumerator DelayedDespawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
