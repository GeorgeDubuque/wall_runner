using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEngine;

[ExecuteInEditMode]
public class Tilemap3 : MonoBehaviour
{
    public GameObject tile;
    public enum modeEnum {
        Draw,
        Erase
    }
    public Vector3 snap;

    public modeEnum mode;
    
}
#endif