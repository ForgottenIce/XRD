using System.Collections;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    
    [SerializeField] private SoundEventEmitter soundEventEmitter;
    
    private bool isAlive = true;
   
    private void OnCollisionEnter(Collision other)
    {
        if (isAlive)
        {
            soundEventEmitter.EmitSoundEvent(new SoundEvent(transform.position));
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<AudioSource>().Play();
            isAlive = false;
            StartCoroutine(DelayedDestroy());
        }
    }
    
    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
