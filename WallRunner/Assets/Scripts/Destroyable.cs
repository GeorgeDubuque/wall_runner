using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    Transform destroyPoint;
    Rigidbody rb;
    bool frozen = true;
    float colorChangeOffset = 5;
    Gradient colorGradient;
    public Color destroyColor = Color.red;
    float currColor = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        destroyPoint = GameObject.Find("DestroyLevelPoint").transform;
        //colorGradient = new Gradient();
        //Color matColor = transform.GetComponent<Renderer>().material.color;
        //GradientColorKey startColor = new GradientColorKey(matColor, 0);
        //GradientColorKey endColor = new GradientColorKey(destroyColor, 1);
        //colorGradient.colorKeys = new GradientColorKey[] { startColor, endColor };
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.z < destroyPoint.position.z && frozen) {
            frozen = false;
            //UnFreeze();   
        }
        //if (transform.position.z < (destroyPoint.position.z + colorChangeOffset)) {
        //    if (currColor < 1.0f) {
        //        currColor += 0.5f * Time.deltaTime;
        //        transform.GetComponent<Renderer>().material.SetColor("_Color", colorGradient.Evaluate(currColor));

        //    }
        //}
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

