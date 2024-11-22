using UnityEngine;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private float movementAmount;
    [SerializeField] private UnityEvent<string> buttonPressed;
    public void HandleButtonPressed() {
        transform.position = transform.position - (Quaternion.Euler(0, -90, 0) * transform.forward) * movementAmount;
        buttonPressed.Invoke("");
    }
    public void HandleButtonReleased() {
        transform.position = transform.position + (Quaternion.Euler(0, -90, 0) * transform.forward) * movementAmount;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position+transform.forward);
    }
}
