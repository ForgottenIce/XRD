using System;
using TMPro;
using UnityEngine;

public class ParkingSpotFinder2 : MonoBehaviour
{
    [SerializeField] private GameObject marker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        var obj = GameObject.Find("ParkingSpaceInput");
        if (obj != null) {
            obj.GetComponent<TMP_InputField>().onValueChanged.AddListener(SearchForParkingSpace);
        }
    }

    void SearchForParkingSpace(string parkingSpace) {
        if (int.TryParse(parkingSpace, out int number)) {
            if (transform.GetChild(0) != null
                && transform.GetChild(0).Find("ParkingLine.000")
                && number > 148
                && number< 321) {
                var (index1, index2) = FindLineIndexes(number);
                var pos1 = transform.GetChild(0).Find("ParkingLine." + index1.ToString("D3")).GetComponent<Renderer>().bounds.center;
                var pos2 = transform.GetChild(0).Find("ParkingLine." + index2.ToString("D3")).GetComponent<Renderer>().bounds.center;
                marker.SetActive(true);
                marker.transform.position = pos1+((pos2-pos1)/2);

            }
            else {
                marker.SetActive(false);
            }
        }
        else {
            marker.SetActive(false);
        }
    }

    private void OnDrawGizmos() {
        if (transform.GetChild(0).Find("ParkingLine.000")) {
            Gizmos.color = Color.red;
            var (index1, index2) = FindLineIndexes(261);
            var pos1 = transform.GetChild(0).Find("ParkingLine."+ index1.ToString("D3")).GetComponent<Renderer>().bounds.center;
            var pos2 = transform.GetChild(0).Find("ParkingLine."+ index2.ToString("D3")).GetComponent<Renderer>().bounds.center;
            Gizmos.DrawSphere(pos1, .5f);
            Gizmos.DrawSphere(pos2, .5f);
            Gizmos.DrawSphere(pos1+((pos2-pos1)/2), 1f);
        }
        Gizmos.DrawLine(transform.position,transform.forward*2);
    }
    private (int, int) FindLineIndexes(int spotIndex) {
        if (spotIndex >= 149 && spotIndex <= 181) {
            return (spotIndex - 149, spotIndex - 148);
        } else if (spotIndex >= 182 && spotIndex <= 211) {
            return (spotIndex - 148, spotIndex - 147);
        } else if (spotIndex >= 212 && spotIndex <= 240) {
            return (spotIndex - 147, spotIndex - 146);
        } else if (spotIndex >= 241 && spotIndex <= 260) {
            return (spotIndex - 146, spotIndex - 145);
        } else if (spotIndex >= 261 && spotIndex <= 279) {
            return (spotIndex - 145, spotIndex - 144);
        } else if (spotIndex >= 280 && spotIndex <= 296) {
            return (spotIndex - 144, spotIndex - 143);
        } else if (spotIndex >= 297 && spotIndex <= 312) {
            return (spotIndex - 143, spotIndex - 142);
        } else if (spotIndex >= 313 && spotIndex <= 320) {
            return (spotIndex - 142, spotIndex - 141);
        }
        return (0, 0);
    }
}
