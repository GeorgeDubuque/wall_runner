using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public GameObject paintTexture;
    public GameObject paintSplatter;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Wall");
        if (Physics.Raycast(transform.position, transform.right, out hit, 1f, layerMask)) {
            if (hit.collider.CompareTag("Wall")) {
                if (hit.transform.childCount == 0) {
                    Instantiate(paintTexture, hit.collider.bounds.center, Quaternion.FromToRotation(Vector3.forward, hit.normal), hit.transform);
                }
            }
        }
        if (Physics.Raycast(transform.position, -transform.right, out hit, 1f, layerMask)) {
            if (hit.collider.CompareTag("Wall")) {
                if (hit.transform.childCount == 0) {
                    Instantiate(paintTexture, hit.collider.bounds.center, Quaternion.FromToRotation(Vector3.forward, hit.normal), hit.transform);
                }
            }
        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 1f, layerMask)) {
            if (hit.collider.CompareTag("Wall")) {
                if (hit.transform.childCount == 0) {
                    Instantiate(paintTexture, hit.collider.bounds.center, Quaternion.FromToRotation(Vector3.forward, hit.normal), hit.transform);
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f, layerMask)) {
            if (hit.collider.CompareTag("Wall")) {
                if (hit.transform.childCount == 0) {
                    Instantiate(paintSplatter, hit.collider.bounds.center, Quaternion.FromToRotation(Vector3.forward, hit.normal), hit.transform);
                }
            }
        }
    }
}
