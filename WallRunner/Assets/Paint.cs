﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Paint : MonoBehaviour
{
    Vector3 direction;

    private void Update ( ) {
        Debug.DrawRay(transform.parent.transform.position, direction, Color.red);

    }
    public GameObject paintTexture;
    private void OnTriggerEnter ( Collider other ) {
        if (other.CompareTag("Wall")) {
            RaycastHit hit;
            direction = other.transform.position - transform.parent.transform.position;
            Vector3 dir = other.transform.position - other.transform.position;
            if (Physics.Raycast(transform.parent.transform.position, direction, out hit, 1 << LayerMask.NameToLayer("Wall"))) {
                GameObject obj = Instantiate(paintTexture, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            }
        }
        
    }
}