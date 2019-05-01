using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    CharacterController controller;
    public float forwardMoveSpeed;
    float speed = 0.0f;
    Vector3 horizontalMove = Vector3.right;
    Vector3 verticalMove = Vector3.forward;
    Vector3 moveDir = Vector3.zero;
    bool canMove;
    public int numCoins = 0;
    public GameObject sparks;
    public Transform spawnPoint;
    public float sizeDec = .01f;
    Vector3 sizeDecVect;
    public FollowPlayer cam;

    Animator anim;
    bool initHit = true;


    // Input
    bool moveLeft = false;
    bool moveRight = false;
    bool moveForward = false;
    bool moveBackward = false;

    // Start is called before the first frame update
    void Start ( ) {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        anim = GetComponentInChildren<Animator>();
        transform.position = spawnPoint.position;
        cam.RespawnCamera();
        controller = GetComponent<CharacterController>();
        sizeDecVect = new Vector3(sizeDec, sizeDec, sizeDec);
        //moveDir = -transform.right * forwardMoveSpeed;
    }

    // Update is called once per frame
    void Update ( ) {
        if (moveLeft && canMove) {
            anim.SetBool("Move", true);
            canMove = false;
            transform.forward = -Vector3.right;
            speed = 0.0f;

        }
        if (moveRight && canMove) {
            canMove = false;
            anim.SetBool("Move", true);
            transform.forward = Vector3.right;
            speed = 0.0f;

        }
        if (moveForward && canMove) {
            canMove = false;
            anim.SetBool("Move", true);
            transform.forward = Vector3.forward;
            speed = 0.0f;

        }
        if (moveBackward && canMove) {
            canMove = false;
            anim.SetBool("Move", true);
            transform.forward = -Vector3.forward;
            speed = 0.0f;

        }
        anim.SetFloat("Velocity", controller.velocity.magnitude);
        PlayerInput();

        if(speed > 0f) {
            if (Physics.Raycast(transform.position, transform.forward * 2, 1 << LayerMask.NameToLayer("Wall"))) {
                anim.SetBool("Move", false);
            }
        }

        if (!canMove) {
            
            speed += 100f * Time.deltaTime;
        } else {
            speed = 0.0f;
        }
        
        //transform.forward = controller.velocity.normalized;
        controller.Move(transform.forward * speed * Time.deltaTime);
    }

    private void SwipeDetector_OnSwipe(SwipeData data ) {
        moveRight = data.Direcion == SwipeDirection.Right;
        moveLeft = data.Direcion == SwipeDirection.Left;
        moveForward = data.Direcion == SwipeDirection.Up;
        moveBackward = data.Direcion == SwipeDirection.Down;
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
        RaycastHit hit;
        int layerMask = LayerMask.NameToLayer("Wall"); //Player Layer Mask
        layerMask = ~layerMask;
        
        if (other.CompareTag("Wall")) {
            
            if (controller.velocity.magnitude == 0){
                canMove = true;
                sparks.SetActive(false);
            } else {
                
                if(sparks.activeSelf == false) {
                    sparks.SetActive(true);
                }

                if (Physics.Raycast(transform.position, transform.right, out hit, 1f, layerMask)) {
                    sparks.transform.position = hit.point;
                } else if (Physics.Raycast(transform.position, -transform.right, out hit, 1f, layerMask)) {
                    sparks.transform.position = hit.point;
                } else if (Physics.Raycast(transform.position, transform.forward, out hit, 1f, layerMask)) {
                    sparks.transform.position = hit.point;
                } else if (Physics.Raycast(transform.position, -transform.forward, out hit, 1f, layerMask)) {
                    sparks.transform.position = hit.point;
                } else {
                    sparks.SetActive(false);
                }
            }
            
        }
    }

    private Vector3 GetCenterOfFace(Collider coll, Vector3 hitPoint) {
        return coll.bounds.center;
    }

    private void OnTriggerExit ( Collider other ) {
        if (other.CompareTag("Wall")) {
            initHit = true;

            canMove = false;
        }
    }

}