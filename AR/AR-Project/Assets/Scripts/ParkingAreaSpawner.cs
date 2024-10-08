using System;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class ParkingAreaSpawner : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager m_TrackedImageManager;
    [SerializeField] GameObject parckingAreaPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnEnable() => m_TrackedImageManager.trackablesChanged.AddListener(OnChanged);

    private void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        if (m_TrackedImageManager.trackables.count > 0 && eventArgs.added.FirstOrDefault()?.transform.childCount == 0)
        {
            var theParckingArea = Instantiate(parckingAreaPrefab);
            theParckingArea.transform.SetParent(eventArgs.added.FirstOrDefault()?.transform, false);
        }
    }
}
