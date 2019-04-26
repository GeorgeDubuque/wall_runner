using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    CharacterController controller;
    public float forwardMoveSpeed;
    public float horizonMoveSpeed;
    Vector3 horizontalMove = Vector3.right;
    Vector3 verticalMove = Vector3.forward;
    Vector3 moveDir = Vector3.zero;
    bool canMove;
    public int numCoins = 0;
    public GameObject sparks;
    public Transform spawnPoint;
    public float sizeDec = .01f;
    Vector3 sizeDecVect;

    Animator anim;
    bool initHit = true;


    // Input
    bool moveLeft = false;
    bool moveRight = false;
    bool moveForward = false;
    bool moveBackward = false;

    // Start is called before the first frame update
    void Start ( ) {
        anim = GetComponentInChildren<Animator>();
        transform.position = spawnPoint.position;
        controller = GetComponent<CharacterController>();
        sizeDecVect = new Vector3(sizeDec, sizeDec, sizeDec);
        //moveDir = -transform.right * forwardMoveSpeed;
    }

    // Update is called once per frame
    void Update ( ) {
        PlayerInput();
        if(controller.velocity.magnitude > 0.01) {
            anim.SetBool("Move", true);
        } else {
            anim.SetBool("Move", false);
        }
        if (moveLeft && canMove) {
            initHit = true;
            //anim.SetBool("Move",true);
            canMove = false;
            transform.forward = -Vector3.right;
        }
        if (moveRight && canMove) {
            canMove = false;
            initHit = true;
            //anim.SetBool("Move", true);
            transform.forward = Vector3.right;
        }
        if(moveForward && canMove) {
            canMove = false;
            initHit = true;
            //anim.SetBool("Move", true);
            transform.forward = Vector3.forward;
        }
        if (moveBackward && canMove) {
            canMove = false;
            initHit = true;
            //anim.SetBool("Move", true);
            transform.forward = -Vector3.forward;
        }
        //transform.forward = controller.velocity.normalized;
        controller.Move(transform.forward * forwardMoveSpeed * Time.deltaTime);
    }

    void PlayerInput ( ) {
        moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        moveForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        moveBackward = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }

    private void OnTriggerEnter ( Collider other ) {
        if (other.CompareTag("Coin")) {
            numCoins++;
        }
        
    }

    private void OnTriggerStay ( Collider other ) {
        RaycastHit rightHit;
        RaycastHit leftHit;
        RaycastHit frontHit;
        RaycastHit backHit;
        int layerMask = 1 << 9; //Player Layer Mask
        layerMask = ~layerMask;
        
        if (other.CompareTag("Wall")) {
            if(controller.velocity.magnitude == 0){
                if (initHit) {
                    anim.SetBool("HitWall",true);
                    initHit = false;
                }
                canMove = true;
                sparks.SetActive(false);
            } else {
                if(sparks.activeSelf == false) {
                    sparks.SetActive(true);
                }
                if (Physics.Raycast(transform.position, transform.right, out rightHit, 1f, layerMask)) {
                    sparks.transform.position = rightHit.point;
                } else if (Physics.Raycast(transform.position, -transform.right, out leftHit, 1f, layerMask)) {
                    sparks.transform.position = leftHit.point;
                } else if (Physics.Raycast(transform.position, transform.forward, out frontHit, 1f, layerMask)) {
                    sparks.transform.position = frontHit.point;
                } else if (Physics.Raycast(transform.position, -transform.forward, out backHit, 1f, layerMask)) {
                    sparks.transform.position = backHit.point;
                } else {
                    sparks.SetActive(false);
                }
            }
            
        }
    }

    private void OnTriggerExit ( Collider other ) {
        if (other.CompareTag("Wall")) {
            canMove = false;
        }
    }

}