using System.Collections;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    
    [SerializeField] private SoundEventEmitter soundEventEmitter;
   
    private void OnCollisionEnter(Collision other)
    {
        soundEventEmitter.EmitSoundEvent(new SoundEvent(transform.position));
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(DelayedDestroy());
    }
    
    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
