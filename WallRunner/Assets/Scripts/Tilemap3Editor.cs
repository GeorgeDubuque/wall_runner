using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Tilemap3))]
public class Tilemap3Editor : Editor
{
    Tilemap3 tilemap;
    GameObject tile;
    int hashCode;
    Collider coll;
    bool mouseDown = false;
    bool ctrlDown = false;

    private void OnEnable ( ) {
        hashCode = GetHashCode();
        tilemap = (Tilemap3)target;
        tile = tilemap.tile;
        coll = tilemap.coll;
    }

    public override void OnInspectorGUI ( ) {
        base.OnInspectorGUI();

        hashCode = GetHashCode();
        tilemap = (Tilemap3)target;
        tile = tilemap.tile;
        coll = tilemap.coll;
    }

    private void OnSceneGUI ( ) {
        tilemap = (Tilemap3)target;
        coll = tilemap.coll;
        tile = tilemap.tile;
        
        Event currentEvent = Event.current;
        if (currentEvent.type == EventType.Layout) {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));
        }

        IsMouseDown(currentEvent);
        IsCtrlHeld(currentEvent);

        if (mouseDown) {
            Ray worldRay = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
            RaycastHit hitInfo;
            if (coll.Raycast(worldRay, out hitInfo, 1000f)) {
                GameObject hitObj = hitInfo.collider.gameObject;
                if (hitObj.CompareTag("3DTilemap")) {
                    Vector3 hitPoint = hitInfo.point;
                    hitPoint = new Vector3(Mathf.RoundToInt(hitPoint.x), Mathf.RoundToInt(hitPoint.y), Mathf.RoundToInt(hitPoint.z));
                    GameObject obj;
                    bool tileHere = tilemap.tiles.TryGetValue(hitPoint.ToString(), out obj);
                    if (!tileHere && mouseDown && !ctrlDown) {
                        obj = PrefabUtility.InstantiatePrefab(tile as GameObject) as GameObject;
                        obj.transform.position = hitPoint;
                        obj.transform.parent = hitObj.transform;
                        tilemap.tiles.Add(hitPoint.ToString(), obj);
                    } else if (tileHere && mouseDown && ctrlDown) {
                        obj.transform.parent = null;
                        tilemap.tiles.Remove(hitPoint.ToString());
                        DestroyImmediate(obj);
                    }
                }
            }
            currentEvent.Use();
        }
    }

    void IsMouseDown ( Event currEvent ) {
        if(currEvent.type == EventType.MouseDown && currEvent.button == 0) {
            mouseDown = true;
        }
        if(currEvent.type == EventType.MouseUp && currEvent.button == 0) {
            mouseDown = false;
        }
    }

    void IsCtrlHeld (Event currEvent ) {
        if(currEvent.type == EventType.KeyDown && currEvent.keyCode == KeyCode.LeftControl) {
            ctrlDown = true;
        }
        if (currEvent.type == EventType.KeyUp && currEvent.keyCode == KeyCode.LeftControl) {
            ctrlDown = false;
        }
    }

}
#endif
