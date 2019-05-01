using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Animator anim;

    private void Start ( ) {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -100) {
            Destroy(gameObject);
        }
    }
}
