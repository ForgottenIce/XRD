using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMeshProTarget;
    [SerializeField] private string _targetKey;

    public void DisplayTextOnTarget(string tagetKey, string value) {
        if (_targetKey == tagetKey) {
            _textMeshProTarget.text = value;
        }
    }
}
