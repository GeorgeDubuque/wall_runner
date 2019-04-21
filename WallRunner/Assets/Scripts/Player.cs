using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    CharacterController controller;
    public float forwardMoveSpeed;
    public float horizonMoveSpeed;
    Vector3 horizontalMove = Vector3.right;
    bool canMove;
    public int numCoins = 0;
    public GameObject sparks;

    // Input
    bool moveLeft = false;
    bool moveRight = false;

    // Start is called before the first frame update
    void Start ( ) {
        controller = GetComponent<CharacterController>();
        Debug.Log(controller.collisionFlags);
    }

    // Update is called once per frame
    void Update ( ) {
        PlayerInput();
        if (moveLeft && canMove) {
            horizontalMove = -transform.right;
        }
        if (moveRight && canMove) {
            horizontalMove = transform.right;
        }
        Vector3 moveDir = (transform.forward * forwardMoveSpeed) + (horizontalMove * horizonMoveSpeed);
        controller.Move(moveDir * Time.deltaTime);
    }

    void PlayerInput ( ) {
        moveRight = Input.GetKey(KeyCode.D);
        moveLeft = Input.GetKey(KeyCode.A);
    }

    private void OnTriggerEnter ( Collider other ) {
        if (other.CompareTag("Coin")) {
            numCoins++;
        }
    }

    private void OnTriggerStay ( Collider other ) {
        RaycastHit rightHit;
        RaycastHit leftHit;
        int layerMask = 1 << 9; //Player Layer Mask
        layerMask = ~layerMask;
        
        if (other.CompareTag("Wall")) {
            sparks.SetActive(true);
            canMove = true;
            if (Physics.Raycast(transform.position, transform.right, out rightHit, 1f, layerMask)) {
                sparks.transform.position = rightHit.point;
                Debug.Log("Hit Right Wall");
            }
            if (Physics.Raycast(transform.position, -transform.right, out leftHit, 1f, layerMask)) {
                sparks.transform.position = leftHit.point;
                Debug.Log("Hit Left Wall");
            }
        }
    }

    private void OnTriggerExit ( Collider other ) {
        if (other.CompareTag("Wall")) {
            sparks.SetActive(false);
            canMove = false;
        }
    }

}