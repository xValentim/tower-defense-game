using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour
{
    [Tooltip("List of next waypoints that can be reached from this waypoint.")]
    public List<Waypoint> nextWaypoints = new();

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var waypoint in nextWaypoints)
        {
            if (waypoint != null)
            {
                Gizmos.DrawLine(transform.position, waypoint.transform.position);
            }
        }
    }
}
