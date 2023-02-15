using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class UpdateGraph : MonoBehaviour
{
    private Transform lastKnownTransform;
    [SerializeField] private bool reScan;

    // Update is called once per frame
    void Update()
    {
        /*if (!transform == lastKnownTransform)
        {
            AstarPath.active.Scan();
            Debug.Log("scanned");
        }*/

        if (reScan)
        {
            AstarPath.active.Scan();
            Debug.Log("scanned");
        }

        lastKnownTransform = transform;
    }
}
