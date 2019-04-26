using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tilemap3))]
public class Tilemap3Editor : Editor
{
    Tilemap3 tilemap;
    GameObject scriptObject;
    Tilemap3.modeEnum mode;
    int hashCode;

    private void OnEnable ( ) {
       
    }

    public override void OnInspectorGUI ( ) {
        hashCode = GetHashCode();

        base.OnInspectorGUI();
        tilemap = (Tilemap3)target;
        scriptObject = tilemap.tile;
        mode = tilemap.mode;
    }

    private void OnSceneGUI ( ) {
        Event currentEvent = Event.current;
        if (Event.current.type == EventType.Layout) {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));
        }
        if ((Event.current.type == EventType.MouseDrag && Event.current.button == 0) || (Event.current.type == EventType.MouseDown && Event.current.button == 0)) {
            Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(worldRay, out hitInfo)) {
                GameObject hitObj = hitInfo.collider.gameObject;
                if (hitObj.CompareTag("3DTilemap") && mode == Tilemap3.modeEnum.Draw) {
                    Vector3 hitPoint = hitInfo.point;
                    hitPoint = new Vector3(Mathf.RoundToInt(hitPoint.x), 0, Mathf.RoundToInt(hitPoint.z));
                    if(!Physics.CheckBox(hitPoint, new Vector3(.4f, .4f, .4f))) {
                        GameObject obj = PrefabUtility.InstantiatePrefab(scriptObject as GameObject) as GameObject;
                        obj.transform.position = hitPoint;
                        obj.transform.parent = hitObj.transform;
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
