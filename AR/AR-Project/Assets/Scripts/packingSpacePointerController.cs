using System;
using System.Collections;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

public class packingSpacePointerController : MonoBehaviour
{

    [SerializeField] GameObject XROrigin;
    [SerializeField] GameObject pointer;
    [SerializeField] TMP_InputField inputField;
    private GameObject parckingArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputField.onValueChanged.AddListener(SearchForParkingSpace);
    }

    private void SearchForParkingSpace(string arg0) {
        StartCoroutine(GetCylinder());
    }

    private IEnumerator GetCylinder() {
        yield return new WaitForEndOfFrame();
        parckingArea = XROrigin.GetNamedChild("SpotPointer");
    }

    // Update is called once per frame
    void Update()
    {
        if (parckingArea != null && parckingArea.activeSelf) {
            pointer.SetActive(true);
            pointer.transform.LookAt(parckingArea.transform.position);
        } else {
            pointer.SetActive(false);
        }
    }
}
