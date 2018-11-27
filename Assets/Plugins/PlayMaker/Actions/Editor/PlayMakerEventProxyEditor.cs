using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayMakerProxyBase), true)]
public class PlayMakerEventProxyEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This component is normally added automatically by PlayMaker.", MessageType.None);

        DrawDefaultInspector();

        EditorGUILayout.Space();

        // Give some info on event callbacks registered with this proxy

        GUILayout.Label("Event Callbacks Registered:", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        var proxy = (PlayMakerProxyBase) target;
        var hasCallbacks = false;

        if (proxy.HasCollisionEventDelegates())
        {
            EditorGUILayout.LabelField("Collision Events");
            hasCallbacks = true;
        }

        if (proxy.HasTriggerEventDelegates())
        {
            EditorGUILayout.LabelField("Trigger Events");
            hasCallbacks = true;
        }

        if (proxy.HasCollision2DEventDelegates())
        {
            EditorGUILayout.LabelField("Collision 2D Events");
            hasCallbacks = true;
        }

        if (proxy.HasTrigger2DEventDelegates())
        {
            EditorGUILayout.LabelField("Trigger 2D Events");
            hasCallbacks = true;
        }

        if (proxy.HasParticleCollisionEventDelegates())
        {
            EditorGUILayout.LabelField("Particle Collision Events");
            hasCallbacks = true;
        }

        if (proxy.HasControllerCollisionEventDelegates())
        {
            EditorGUILayout.LabelField("Controller Collision Events");
            hasCallbacks = true;
        }

        if (!hasCallbacks)
        {
            EditorGUILayout.LabelField("None");
        }
    }
}