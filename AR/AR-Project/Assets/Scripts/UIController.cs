using System;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class UIController : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager m_TrackedImageManager;
    [SerializeField] GameObject scanBorder;
    [SerializeField] GameObject inputField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void OnEnable() => m_TrackedImageManager.trackablesChanged.AddListener(OnChanged);

    private void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs) {
        if (m_TrackedImageManager.trackables.count > 0 && scanBorder.activeSelf) {
            scanBorder.SetActive(false);
            inputField.SetActive(true);
        }
    }
}
