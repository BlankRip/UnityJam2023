using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Gameplay.LevelGeneration;

public class OffsetTester : EditorWindow
{
    private Transform offsetFrom;
    private ObjectPlacementConstrains constrainsObj;
    private bool updateContiniousFromInspector;
    private bool setContiniousOffsetFromScene;

    [MenuItem("Window/Offset Tester")]
    public static void ShowWindow() {
        OffsetTester window = GetWindow<OffsetTester>("Constrain Offset Tester");
        window.maxSize = new Vector2(window.maxSize.x, 150);
        window.minSize = new Vector2(250, 130);
    }

    private void OnGUI() {
        GUILayout.Space(15);
        offsetFrom = (Transform)EditorGUILayout.ObjectField("Test Offset From", offsetFrom, typeof(Transform),true);
        GUILayout.Space(3);
        constrainsObj = (ObjectPlacementConstrains)EditorGUILayout.ObjectField("Constrains Obj", constrainsObj, typeof(ObjectPlacementConstrains),true);
        GUILayout.Space(10);
        updateContiniousFromInspector = EditorGUILayout.Toggle("Update from offset: ", updateContiniousFromInspector);
        setContiniousOffsetFromScene = EditorGUILayout.Toggle("Set Offset from scene: ", setContiniousOffsetFromScene);

        
        if(GUILayout.Button("Update"))
            ShowUpdatedOffset();
        if(GUILayout.Button("Find & Set Offset"))
            SetCurrentAsOffset();
    }

    void OnInspectorUpdate()
    {
        if(updateContiniousFromInspector)
            ShowUpdatedOffset();
        if(setContiniousOffsetFromScene)
            SetCurrentAsOffset();
    }

    private void ShowUpdatedOffset()
    {
        constrainsObj.transform.position = offsetFrom.position;
        constrainsObj.transform.position += constrainsObj.placementOffset;
    }

    private void SetCurrentAsOffset()
    {
        constrainsObj.placementOffset = constrainsObj.transform.position - offsetFrom.position;
    }
}
