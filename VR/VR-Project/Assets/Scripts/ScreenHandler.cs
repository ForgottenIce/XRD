using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private TextMeshProUGUI continueText;
    [SerializeField] private Image ScreenOverlay;
    private bool FadeIn = false;
    [SerializeField] private float FadeAmount = 0.1f;


    private void Update() {
        if (FadeIn) {
            var color = ScreenOverlay.color;
            if (color.a <= 1) {
                color.a += FadeAmount * Time.deltaTime;
                ScreenOverlay.color = color;
            } else {
                _textMeshPro.gameObject.SetActive(true);
                continueText.gameObject.SetActive(true);
            }
        }
    }
    public void HandleWin() {
        ScreenOverlay.color = new Color(0, 0, 0, .1f);
        FadeIn = true;
        _textMeshPro.SetText("Congratulations on escaping the maze");
    }
    public void HandleDeath() {
        ScreenOverlay.color = new Color(1, 0, 0, .5f);
        _textMeshPro.SetText("You Died");
        _textMeshPro.gameObject.SetActive(true);
        continueText.gameObject.SetActive(true);
    }
}
