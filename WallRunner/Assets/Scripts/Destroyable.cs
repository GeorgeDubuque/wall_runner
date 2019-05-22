using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    Transform destroyPoint;
    Rigidbody rb;
    bool frozen = true;
    float currColor = 0.0f;
    public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        destroyPoint = GameObject.Find("DestroyLevelPoint").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.z < destroyPoint.position.z && frozen) {
            frozen = false;
            UnFreeze();
        }
        if (!frozen) {
            if (transform.position.y < -50) {
                Destroy(gameObject);
            }
            
        }
    }

    void UnFreeze ( ) {
        transform.parent = null;
        rb.useGravity = true;
        rb.constraints = ~RigidbodyConstraints.FreezePositionY;
    }
}

