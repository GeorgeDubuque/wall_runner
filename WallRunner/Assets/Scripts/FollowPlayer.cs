using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Player player;
    CharacterController controller;
    public Transform target;
    public float zOffset;
    public float yOffset;
    public float pitch;
    public float forwardSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Quaternion rot = Quaternion.Euler(pitch, 0.0f, 0.0f);
        transform.rotation = rot;
        transform.position = new Vector3(0.0f, target.position.y + yOffset, target.position.z - zOffset);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = (target.forward * player.forwardMoveSpeed);
        controller.Move(moveDir * Time.deltaTime);
    }

    public void RespawnCamera()
    {
        transform.position = new Vector3(0.0f, target.position.y + yOffset, target.position.z - zOffset);
    }
}
