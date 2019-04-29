using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Tilemap3))]
public class Tilemap3Editor : Editor
{
    Tilemap3 tilemap;
    GameObject scriptObject;
    Tilemap3.modeEnum mode;
    int hashCode;

    private void OnEnable ( ) {
        hashCode = GetHashCode();

        tilemap = (Tilemap3)target;
        scriptObject = tilemap.tile;
        mode = tilemap.mode;
    }

    public override void OnInspectorGUI ( ) {
        base.OnInspectorGUI();

        hashCode = GetHashCode();

        tilemap = (Tilemap3)target;
        scriptObject = tilemap.tile;
        mode = tilemap.mode;
    }

    private void OnSceneGUI ( ) {
        mode = tilemap.mode;
        scriptObject = tilemap.tile;
        Event currentEvent = Event.current;
        if (Event.current.type == EventType.Layout) {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));
        }
        if ((Event.current.type == EventType.MouseDrag && Event.current.button == 0) || (Event.current.type == EventType.MouseDown && Event.current.button == 0)) {
            Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(worldRay, out hitInfo)) {
                GameObject hitObj = hitInfo.collider.gameObject;
                Debug.Log(hitObj.tag);
                if (hitObj.CompareTag("3DTilemap") && mode == Tilemap3.modeEnum.Draw) {
                    Debug.Log("hit tilemap");
                    Vector3 hitPoint = hitInfo.point;
                    hitPoint = new Vector3(Mathf.RoundToInt(hitPoint.x), Mathf.RoundToInt(hitPoint.y), Mathf.RoundToInt(hitPoint.z));
                    if(!Physics.CheckBox(center : hitPoint, halfExtents: new Vector3(.4f, .4f, .4f), Quaternion.identity, layerMask : (1<<LayerMask.NameToLayer("3DTilemap")))) {
                        GameObject obj = PrefabUtility.InstantiatePrefab(scriptObject as GameObject) as GameObject;
                        obj.transform.position = hitPoint;
                        obj.transform.parent = hitObj.transform;
                    } else {
                        Debug.Log("already a tile there?");
                    }
                    
                }else if (hitObj.CompareTag(scriptObject.tag) && mode == Tilemap3.modeEnum.Erase) {
                    hitObj.transform.parent = null;
                    DestroyImmediate(hitObj);
                }
            }

            Event.current.Use();
        }
    }
}
#endif
