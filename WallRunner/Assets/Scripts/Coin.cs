﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(0.0f, rotateSpeed, 0.0f);
        transform.Rotate(rotation * Time.deltaTime);
    }

    private void OnTriggerEnter ( Collider other ) {
        if (other.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
