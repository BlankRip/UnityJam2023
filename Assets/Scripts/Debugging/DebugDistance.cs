using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDistance : MonoBehaviour
{
    [SerializeField] Transform tA, tB;
    [SerializeField] float testValue;

    [Header("Only for viewing")]
    [SerializeField] float distanceValue;
    [SerializeField] float testAgains;

    private void Update() {
        distanceValue = (tA.position - tB.position).sqrMagnitude;
        testAgains = testValue * testValue;
    }
}
