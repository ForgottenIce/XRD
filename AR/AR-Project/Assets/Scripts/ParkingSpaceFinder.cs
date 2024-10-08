using TMPro;
using UnityEngine;
using System;

public class ParkingSpaceFinder : MonoBehaviour
{
    [SerializeField] private Vector3[] parkingSpots;
    [SerializeField] private GameObject marker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var obj = GameObject.Find("ParkingSpaceInput");
        if (obj != null) {
            obj.GetComponent<TMP_InputField>().onValueChanged.AddListener(SearchForParkingSpace);
        }
    }

    void SearchForParkingSpace(string parkingSpace)
    {
        if (int.TryParse(parkingSpace, out int number))
        {
            if (number <= parkingSpots.Length && number > 0)
            {
                marker.SetActive(true);
                marker.transform.localPosition = parkingSpots[number-1];
            } else {
                marker.SetActive(false);
            }
        } else {
            marker.SetActive(false);
        }
    }

    private void OnDrawGizmos() {
        foreach (var item in parkingSpots)
        {
            Gizmos.DrawSphere(item, .1f);
        }
    }
}
