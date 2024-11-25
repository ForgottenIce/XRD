using GLTFast.Schema;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{
    
   [SerializeField] private Transform wardenTransform;
   [SerializeField] private GameObject locomotionGameObject;
   [SerializeField] private GameObject snowBallHandlerGameObject;
   [SerializeField] private UnityEvent deathEvent;
   [SerializeField] private float deathRadius = 3f;
   [SerializeField] private AudioClip deathSound;
   private AudioSource _audioSource;
   private bool _isDead = false;
   private InputAction _restartAction;

   private void Start()
   {
      _audioSource = GetComponent<AudioSource>();
      _restartAction = InputSystem.ListEnabledActions().Find(a => a.name == "RestartGame");
   }

   private void Update()
   {
      if (_isDead && _restartAction.triggered)
      {
         SceneManager.LoadScene(0);
      }
      
      if (wardenTransform == null) return;
      
      var distanceToWarden = Vector3.Distance(transform.position, wardenTransform.position);
      if (!_isDead && distanceToWarden < deathRadius && !Physics.Linecast(transform.position + Vector3.up, wardenTransform.position, out _, LayerMask.GetMask("Default")))
      {
         _audioSource.PlayOneShot(deathSound);
         locomotionGameObject.SetActive(false);
         snowBallHandlerGameObject.SetActive(false);
         deathEvent.Invoke();
         _isDead = true;
      }
   }
}
