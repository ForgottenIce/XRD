using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SnowBallHandler : MonoBehaviour
{
    private InputAction _leftSpawnSnowBall;
    private InputAction _rightSpawnSnowBall;
    private GameObject _currentSnowBall;

    public Transform leftHandTransform;
    public NearFarInteractor leftHandInteractor;
    public Transform rightHandTransform;
    public NearFarInteractor rightHandInteractor;
    
    public GameObject snowBallPrefab;
    
    private void Start()
    {
        _leftSpawnSnowBall = InputSystem.ListEnabledActions().Find(a => a.name == "SpawnSnowBallLeft");
        _rightSpawnSnowBall = InputSystem.ListEnabledActions().Find(a => a.name == "SpawnSnowBallRight");
    }
    
    private void Update()
    {
        if (_leftSpawnSnowBall.triggered && _currentSnowBall == null)
        {
            _currentSnowBall = Instantiate(snowBallPrefab, leftHandTransform.position, leftHandTransform.rotation, transform);
            var snowBallInteractable = _currentSnowBall.GetComponent<XRGrabInteractable>();
            StartCoroutine(DelayedGrab(leftHandInteractor, snowBallInteractable));
        }

        if (_rightSpawnSnowBall.triggered && _currentSnowBall == null)
        {
            _currentSnowBall = Instantiate(snowBallPrefab, rightHandTransform.position, rightHandTransform.rotation, transform);
            var snowBallInteractable = _currentSnowBall.GetComponent<XRGrabInteractable>();
            StartCoroutine(DelayedGrab(rightHandInteractor, snowBallInteractable));
        }
    }
    
    // Have to wait one frame before performing the grab
    private IEnumerator DelayedGrab(XRBaseInteractor interactor, IXRSelectInteractable interactable)
    {
        // Wait one frame
        yield return null;
        
        interactor.interactionManager.SelectEnter(interactor, interactable);
    }
}
