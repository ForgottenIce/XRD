using UnityEngine;
using UnityEngine.Events;

public class LeverHandler : MonoBehaviour
{
    [SerializeField] private HingeJoint joint;
    [SerializeField] private float ReleasedSnapThreshold = 0;
    [SerializeField] private bool ContinuousOutputValues = false;
    [SerializeField] private float NonContinuousOutputThreshold = 0;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> LeverReleased;

    public void HandleLeverReleased() {
        var spring = joint.spring;
        if (!ContinuousOutputValues) {
            if (joint.angle > ReleasedSnapThreshold) {
                spring.targetPosition = 45;
                joint.spring = spring;
            } else {
                spring.targetPosition = -45;
                joint.spring = spring;
            }
        }

        if (ContinuousOutputValues) {
            LeverReleased?.Invoke((joint.angle + 45) / 90);
        } else {
            LeverReleased?.Invoke(joint.angle < NonContinuousOutputThreshold ? 1 : 0);
        }
    }
}
