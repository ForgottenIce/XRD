using UnityEngine;

public class GateHandler : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float descendAmount = -2.999f;
    private bool _descend = false;
    // Update is called once per frame
    void Update()
    {
        if (_descend && transform.position.y > descendAmount) {
            transform.position = transform.position + Vector3.down * Time.deltaTime * speed;
        }
    }

    public void HandleMazeCreated() {
        _descend = true;
    }
}
