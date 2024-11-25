using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent PlayerWon;
    private InputAction _resetButton;
    private bool HasWon = false;

    private void Start() {
        _resetButton = InputSystem.ListEnabledActions().Find(a => a.name == "SpawnSnowBallLeft");
    }

    private void Update() {
        if (_resetButton.triggered && HasWon) {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider collider) {
        Debug.Log("test");
        if (collider.gameObject.CompareTag("Finish")) {
            HasWon = true;
            PlayerWon.Invoke();
        }
    }
}
