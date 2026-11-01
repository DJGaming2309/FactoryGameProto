using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class Conveyerbelt : MonoBehaviour
{
    [Range(0, 1)][SerializeField] private float lerpValue;
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private Transform item;

    private void Update()
    {
        item.transform.position = Vector3.Lerp(a: _lineRenderer.GetPosition(index: 0), b: _lineRenderer.GetPosition(index: 1), lerpValue);
    }
}
