using System;
using UnityEngine;

public class Sense : MonoBehaviour
{
    [SerializeField]
    private float checkRadius = 5;
    [SerializeField]
    private LayerMask checkLayers;

    public Collider[] Check()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
        Array.Sort(colliders, new DistanceComparer(transform));

        return colliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
