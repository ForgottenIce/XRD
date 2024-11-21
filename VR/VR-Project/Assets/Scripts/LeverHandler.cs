using UnityEngine;
using UnityEngine.Events;

public class LeverHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent LeverReleased;
    [SerializeField] private HingeJoint joint;

    public void HandleLeverReleased() {
        Debug.Log(joint.angle);
        var spring = joint.spring;
        if (joint.angle > 0) {
            spring.targetPosition = 45;
            joint.spring = spring;
        } else {
            spring.targetPosition = -45;
            joint.spring = spring;
        }
        LeverReleased?.Invoke();
    }
}
