using UnityEngine;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private float movementAmount;
    [SerializeField] private UnityEvent buttonPressed;
    public void HandleButtonPressed() {
        transform.position = transform.position - (Quaternion.Euler(0, -90, 0) * transform.forward) * movementAmount;
        buttonPressed.Invoke();
        Debug.Log("pressed");
    }
    public void HandleButtonReleased() {
        transform.position = transform.position + (Quaternion.Euler(0, -90, 0) * transform.forward) * movementAmount;
        Debug.Log("released");
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position+transform.forward);
    }
}
