// (c) Copyright HutongGames, all rights reserved.

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
#define UNITY_PRE_5_3
#endif

#define FSM_LOG

#if !PLAYMAKER_NO_UI

using UnityEngine.UI;

#endif

using System;
using System.Collections.Generic;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace HutongGames.PlayMaker
{

    /// <summary>
    /// Helper functions to make authoring Actions simpler.
    /// </summary>
    public static class ActionHelpers
    {
        /// <summary>
        /// Get a small white texture
        /// </summary>
        public static Texture2D WhiteTexture
        {
            // Used to make a texture, but Unity added this:
            get { return Texture2D.whiteTexture; }
        }

        /// <summary>
        /// Common blend operations for colors
        /// E.g. used by TweenColor action
        /// </summary>
        /// <returns></returns>
        public static Color BlendColor(ColorBlendMode blendMode, Color c1, Color c2)
        {
            switch (blendMode)
            {
                case ColorBlendMode.Normal:
                    return Color.Lerp(c1, c2, c2.a);

                case ColorBlendMode.Multiply:
                    return Color.Lerp(c1, c1 * c2, c2.a);

                case ColorBlendMode.Screen:
                    var screen = Color.white - (Color.white - c1) * (Color.white - c2);
                    return Color.Lerp(c1, screen, c2.a);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Check the visibility of the Renderer on a GameObject
        /// </summary>
        public static bool IsVisible(GameObject go)
        {
            if (go == null)
            {
                return false;
            }
            var renderer = go.GetComponent<Renderer>();
            return renderer != null && renderer.isVisible;
        }

        /// <summary>
        /// Get the GameObject targeted by an action's FsmOwnerDefault variable
        /// </summary>
        public static GameObject GetOwnerDefault(FsmStateAction action, FsmOwnerDefault ownerDefault)
        {
            return action.Fsm.GetOwnerDefaultTarget(ownerDefault);
        }

        /// <summary>
        /// Get the first Playmaker FSM on a game object.
        /// </summary>
        public static PlayMakerFSM GetGameObjectFsm(GameObject go, string fsmName)
        {
            if (!string.IsNullOrEmpty(fsmName))
            {
                var fsmComponents = go.GetComponents<PlayMakerFSM>();

                foreach (var fsmComponent in fsmComponents)
                {
                    if (fsmComponent.FsmName == fsmName)
                    {
                        return (fsmComponent);
                    }
                }

                Debug.LogWarning("Could not find FSM: " + fsmName);
            }

            return (go.GetComponent<PlayMakerFSM>());
        }

        /// <summary>
        /// Given an array of weights, returns a randomly selected index. 
        /// </summary>
        public static int GetRandomWeightedIndex(FsmFloat[] weights)
        {
            float totalWeight = 0;

            foreach (var t in weights)
            {
                totalWeight += t.Value;
            }

            var random = Random.Range(0, totalWeight);

            for (var i = 0; i < weights.Length; i++)
            {
                if (random < weights[i].Value)
                {
                    return i;
                }

                random -= weights[i].Value;
            }

            return -1;
        }

        /// <summary>
        /// Add an animation clip to a GameObject if it has an Animation component
        /// </summary>
        public static void AddAnimationClip(GameObject go, AnimationClip animClip)
        {
            if (animClip == null) return;
            var animationComponent = go.GetComponent<Animation>();
            if (animationComponent != null)
            {
                animationComponent.AddClip(animClip, animClip.name);
            }
        }

        /// <summary>
        /// Check if an animation has finished playing.
        /// </summary>
        public static bool HasAnimationFinished(AnimationState anim, float prevTime, float currentTime)
        {
            // looping animations never finish
            if (anim.wrapMode == WrapMode.Loop || anim.wrapMode == WrapMode.PingPong)
            {
                return false;
            }

            // Default and Once reset to zero when done
            if (anim.wrapMode == WrapMode.Default || anim.wrapMode == WrapMode.Once)
            {
                if (prevTime > 0 && currentTime.Equals(0))
                {
                    return true;
                }
            }

            // Time keeps going up in other modes
            return prevTime < anim.length && currentTime >= anim.length;
        }


        // Given an FsmGameObject parameter and an FsmVector3 parameter, returns a world position.
        // Many actions let you define a GameObject and/or a Position...
        public static Vector3 GetPosition(FsmGameObject fsmGameObject, FsmVector3 fsmVector3)
        {
            Vector3 finalPos;

            if (fsmGameObject.Value != null)
            {
                finalPos = !fsmVector3.IsNone ? fsmGameObject.Value.transform.TransformPoint(fsmVector3.Value) : fsmGameObject.Value.transform.position;
            }
            else
            {
                finalPos = fsmVector3.Value;
            }

            return finalPos;
        }

        /*
        public static bool GetPosition(PositionOptions options, GameObject go, FsmGameObject target,
            FsmVector3 position, out Vector3 finalPos)
        {
            var validPos = false;
            finalPos = Vector3.zero;

            if (go == null || target == null || position == null)
                return false;

            switch (options)
            {
                case PositionOptions.CurrentPosition:
                    finalPos = go.transform.position;
                    validPos = true;
                    break;

                case PositionOptions.WorldPosition:
                    if (!position.IsNone)
                    {
                        finalPos = position.Value;
                        validPos = true;
                    }
                break;

                case PositionOptions.GameObject:
                    if (target.Value != null)
                    {
                        finalPos = target.Value.transform.position;
                        validPos = true;
                    }
                    break;

                case PositionOptions.GameObjectWithOffset:
                    if (target != null)
                    {
                        finalPos = GetPosition(target, position);
                        validPos = true;
                    }
                    break;



                case PositionOptions.WorldOffset:
                    finalPos = go.transform.position + position.Value;
                    validPos = true;
                    break;

                case PositionOptions.LocalOffset:
                    finalPos = go.transform.position + go.transform.InverseTransformPoint(position.Value);
                    validPos = true;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return validPos;
        }*/

        /// <summary>
        /// Returns a target rotation in world space given the specified parameters
        /// Some parameters are interpreted differently based on RotationOptions selected.
        /// E.g. used by TweenRotation
        /// </summary>
        /// <param name="option">Rotation options exposed to user</param>
        /// <param name="owner">The transform being rotated</param>
        /// <param name="target">A potential target transform</param>
        /// <param name="rotation">A potential target rotation</param>
        /// <returns></returns>
        public static Quaternion GetTargetRotation(RotationOptions option, Transform owner, Transform target, Vector3 rotation)
        {
            if (owner == null) return Quaternion.identity;

            switch (option)
            {
                case RotationOptions.CurrentRotation:
                    return owner.rotation;

                case RotationOptions.WorldRotation:
                    return Quaternion.Euler(rotation);

                case RotationOptions.LocalRotation: 
                    // same as world rotation if no parent
                    if (owner.parent == null) return Quaternion.Euler(rotation);
                    return owner.parent.rotation * Quaternion.Euler(rotation);

                case RotationOptions.WorldOffsetRotation:
                    // same as rotating with global in editor
                    return Quaternion.Euler(rotation) * owner.rotation;

                case RotationOptions.LocalOffsetRotation:
                    return owner.rotation * Quaternion.Euler(rotation);

                case RotationOptions.MatchGameObjectRotation:
                    if (target == null) return owner.rotation;
                    return target.rotation * Quaternion.Euler(rotation);

                default:
                    throw new ArgumentOutOfRangeException();
            }

            //return owner.rotation; // leave as is
        }

        public static bool GetTargetRotation(RotationOptions option, Transform owner, FsmVector3 rotation,
            FsmGameObject target, out Quaternion targetRotation)
        {
            targetRotation = Quaternion.identity;
            if (owner == null || !CanEditTargetRotation(option, rotation, target)) return false;
            targetRotation = GetTargetRotation(option, owner, 
                target.Value != null ? target.Value.transform : null, 
                rotation.Value);
            return true;
        }

        private static bool CanEditTargetRotation(RotationOptions option, NamedVariable rotation, FsmGameObject target)
        {
            switch (option)
            {
                case RotationOptions.CurrentRotation:
                    return false;
                case RotationOptions.WorldRotation:
                case RotationOptions.LocalRotation:
                case RotationOptions.WorldOffsetRotation:
                case RotationOptions.LocalOffsetRotation:
                    return !rotation.IsNone;
                    
                case RotationOptions.MatchGameObjectRotation:
                    return target.Value != null;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Vector3 GetTargetScale(ScaleOptions option, Transform owner, Transform target, Vector3 scale)
        {
            if (owner == null) return Vector3.one;

            switch (option)
            {
                case ScaleOptions.CurrentScale:
                    return owner.localScale;

                case ScaleOptions.LocalScale:
                    return scale;

                case ScaleOptions.MultiplyScale:
                    return new Vector3(owner.localScale.x * scale.x, owner.localScale.y * scale.y, owner.localScale.z * scale.z);

                case ScaleOptions.AddToScale:
                    return new Vector3(owner.localScale.x + scale.x, owner.localScale.y + scale.y, owner.localScale.z + scale.z);

                case ScaleOptions.MatchGameObject:
                    if (target == null) return owner.localScale;
                    return target.localScale;

                /* Useful...?
                case ScaleOptions.MatchGameObjectMultiply:
                    if (target == null) return owner.localScale;
                    if (scale == Vector3.one) return target.localScale;
                    return new Vector3(target.localScale.x * scale.x, target.localScale.y * scale.y, target.localScale.z * scale.z);
                */
            }

            return owner.localScale; // leave as is
        }

        public static bool GetTargetPosition(PositionOptions option, Transform owner, FsmVector3 position,
            FsmGameObject target, out Vector3 targetPosition)
        {
            targetPosition = Vector3.zero;
            if (owner == null || !IsValidTargetPosition(option, position, target)) return false;
            targetPosition = GetTargetPosition(option, owner, (target != null && target.Value != null) ? target.Value.transform : null, position.Value);
            return true;
        }

        private static bool IsValidTargetPosition(PositionOptions option, NamedVariable position, FsmGameObject target)
        {
            switch (option)
            {
                case PositionOptions.CurrentPosition:
                    return true;
                case PositionOptions.WorldPosition:
                case PositionOptions.LocalPosition:
                case PositionOptions.WorldOffset:
                case PositionOptions.LocalOffset:
                    return !position.IsNone;
                    
                case PositionOptions.TargetGameObject:
                    return target.Value != null;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool CanEditTargetPosition(PositionOptions option, NamedVariable position, FsmGameObject target)
        {
            switch (option)
            {
                case PositionOptions.CurrentPosition:
                    return false;
                case PositionOptions.WorldPosition:
                case PositionOptions.LocalPosition:
                case PositionOptions.WorldOffset:
                case PositionOptions.LocalOffset:
                    return !position.IsNone;
                    
                case PositionOptions.TargetGameObject:
                    return target.Value != null;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Vector3 GetTargetPosition(PositionOptions option, Transform owner, Transform target, Vector3 position)
        {
            if (owner == null) return Vector3.zero;

            switch (option)
            {
                case PositionOptions.CurrentPosition:
                    return owner.position;
                    
                case PositionOptions.WorldPosition:
                    return position;
                    
                case PositionOptions.LocalPosition:
                    if (owner.parent == null) return position;
                    return owner.parent.TransformPoint(position);
                    
                case PositionOptions.WorldOffset:
                    return owner.position + position;

                case PositionOptions.LocalOffset:
                    return owner.TransformPoint(position);
                    
                case PositionOptions.TargetGameObject:
                    if (target == null) return owner.position;
                    if (position != Vector3.zero) return target.TransformPoint(position);
                    return target.position;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Raycast helpers that cache values to minimize the number of raycasts

        #region MousePick

        public static RaycastHit mousePickInfo;
        static float mousePickRaycastTime;
        static float mousePickDistanceUsed;
        static int mousePickLayerMaskUsed;

        public static bool IsMouseOver(GameObject gameObject, float distance, int layerMask)
        {
            if (gameObject == null) return false;
            return gameObject == MouseOver(distance, layerMask);
        }

        public static RaycastHit MousePick(float distance, int layerMask)
        {
            if (!mousePickRaycastTime.Equals(Time.frameCount) ||
                mousePickDistanceUsed < distance ||
                mousePickLayerMaskUsed != layerMask)
            {
                DoMousePick(distance, layerMask);
            }

            // otherwise use cached info

            return mousePickInfo;
        }

        public static GameObject MouseOver(float distance, int layerMask)
        {
            if (!mousePickRaycastTime.Equals(Time.frameCount) ||
                mousePickDistanceUsed < distance ||
                mousePickLayerMaskUsed != layerMask)
            {
                DoMousePick(distance, layerMask);
            }

            if (mousePickInfo.collider != null)
            {
                if (mousePickInfo.distance < distance)
                {
                    return mousePickInfo.collider.gameObject;
                }
            }

            return null;
        }

        static void DoMousePick(float distance, int layerMask)
        {
            if (Camera.main == null)
            {
                return;
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out mousePickInfo, distance, layerMask);

            mousePickLayerMaskUsed = layerMask;
            mousePickDistanceUsed = distance;
            mousePickRaycastTime = Time.frameCount;
        }

        #endregion

        public static int LayerArrayToLayerMask(FsmInt[] layers, bool invert)
        {
            var layermask = 0;

            foreach (var layer in layers)
            {
                layermask |= 1 << layer.Value;
            }

            if (invert)
            {
                layermask = ~layermask;
            }

            // Unity 5.3 changed this Physics property name
            //public const int kDefaultRaycastLayers = -5;
            /*
            #if UNITY_PRE_5_3
                        return layermask == 0 ? Physics.kDefaultRaycastLayers : layermask;
            #else
                        return layermask == 0 ? Physics.DefaultRaycastLayers : layermask;
            #endif
            */
            // HACK just return the hardcoded value to avoid separate Unity 5.3 dll
            // TODO Revisit in future version
            return layermask == 0 ? -5 : layermask;
        }

        // Does a wrap mode loop? (no finished event)
        public static bool IsLoopingWrapMode(WrapMode wrapMode)
        {
            return wrapMode == WrapMode.Loop || wrapMode == WrapMode.PingPong;
        }

        public static string CheckRayDistance(float rayDistance)
        {
            return rayDistance <= 0 ? "Ray Distance should be greater than zero!\n" : "";
        }

        /// <summary>
        /// Check if a state responds to an event.
        /// Not really needed since the ErrorChecker covers this.
        /// </summary>
        public static string CheckForValidEvent(FsmState state, string eventName)
        {
            if (state == null)
            {
                return "Invalid State!";
            }

            if (string.IsNullOrEmpty(eventName))
            {
                return "";
            }

            foreach (var transition in state.Fsm.GlobalTransitions)
            {
                if (transition.EventName == eventName)
                {
                    return "";
                }
            }

            foreach (var transition in state.Transitions)
            {
                if (transition.EventName == eventName)
                {
                    return "";
                }
            }

            return "Fsm will not respond to Event: " + eventName;
        }

        #region Physics setup helpers

        //[Obsolete("Use CheckPhysicsSetup(gameObject) instead")]
        public static string CheckPhysicsSetup(FsmOwnerDefault ownerDefault)
        {
            if (ownerDefault == null) return "";

            return CheckPhysicsSetup(ownerDefault.GameObject.Value);
        }

        //[Obsolete("Use CheckPhysicsSetup(gameObject) instead")]
        public static string CheckOwnerPhysicsSetup(GameObject gameObject)
        {
            return CheckPhysicsSetup(gameObject);
        }

        public static string CheckPhysicsSetup(GameObject gameObject)
        {
            var error = string.Empty;

            if (gameObject != null)
            {
                if (gameObject.GetComponent<Collider>() == null && gameObject.GetComponent<Rigidbody>() == null)
                {
                    error += "GameObject requires RigidBody/Collider!\n";
                }
            }

            return error;
        }

        //[Obsolete("Use CheckPhysics2dSetup(gameObject) instead")]
        public static string CheckPhysics2dSetup(FsmOwnerDefault ownerDefault)
        {
            if (ownerDefault == null) return "";

            return CheckPhysics2dSetup(ownerDefault.GameObject.Value);
        }

        //[Obsolete("Use CheckPhysics2dSetup(gameObject) instead")]
        public static string CheckOwnerPhysics2dSetup(GameObject gameObject)
        {
            return CheckPhysics2dSetup(gameObject);
        }

        public static string CheckPhysics2dSetup(GameObject gameObject)
        {
            var error = string.Empty;

            if (gameObject != null)
            {
                if (gameObject.GetComponent<Collider2D>() == null && gameObject.GetComponent<Rigidbody2D>() == null)
                {
                    error += "GameObject requires a RigidBody2D or Collider2D component!\n";
                }
            }

            return error;
        }

        #endregion

        #region Logging helpers

        public static void DebugLog(Fsm fsm, LogLevel logLevel, string text, bool sendToUnityLog = false)
        {
#if FSM_LOG
            if (!FsmLog.LoggingEnabled || fsm == null)
            {
                return;
            }

            switch (logLevel)
            {
                case LogLevel.Info:
                    fsm.MyLog.LogAction(FsmLogType.Info, text, sendToUnityLog);
                    break;

                case LogLevel.Warning:
                    fsm.MyLog.LogAction(FsmLogType.Warning, text, sendToUnityLog);
                    break;

                case LogLevel.Error:
                    fsm.MyLog.LogAction(FsmLogType.Error, text, sendToUnityLog);
                    break;
            }
#endif
        }

        public static void LogError(string text)
        {
            DebugLog(FsmExecutionStack.ExecutingFsm, LogLevel.Error, text, true);
        }

        public static void LogWarning(string text)
        {
            DebugLog(FsmExecutionStack.ExecutingFsm, LogLevel.Warning, text, true);
        }

        #endregion

        #region AutoName helpers
  
        public static string GetValueLabel(INamedVariable variable)
        {
            if (variable == null) return "[null]";
            if (variable.IsNone) return "[none]";
            if (variable.UseVariable) return variable.Name;
            var rawValue = variable.RawValue;
            if (rawValue == null) return "null";
            if (rawValue is string) return "\"" + rawValue + "\"";
            if (rawValue is Array) return "Array";
#if NETFX_CORE
            if (rawValue.GetType().IsValueType()) return rawValue.ToString();
#else
            if (rawValue.GetType().IsValueType) return rawValue.ToString();
#endif
            var label = rawValue.ToString();
            var classIndex = label.IndexOf('(');
            if (classIndex > 0)
                return label.Substring(0, label.IndexOf('('));
            return label;
        }

        public static string GetValueLabel(Fsm fsm, FsmOwnerDefault ownerDefault)
        {
            if (ownerDefault == null) return "[null]";
            if (ownerDefault.OwnerOption == OwnerDefaultOption.UseOwner) return "Owner";
            return GetValueLabel(ownerDefault.GameObject);
        }


        /// <summary>
        /// ActionName : field1 field2 ...
        /// </summary>
        public static string AutoName(FsmStateAction action, params INamedVariable[] exposedFields)
        {
            return action == null ? null : AutoName(action.GetType().Name, exposedFields);
        }

        /// <summary>
        /// ActionName : field1 field2 ...
        /// </summary>
        public static string AutoName(string actionName, params INamedVariable[] exposedFields)
        {
            var autoName = actionName + " :";
            foreach (var field in exposedFields)
            {
                autoName += " " + GetValueLabel(field);
            }

            return autoName;
        }

        /// <summary>
        /// ActionName : min - max
        /// </summary>
        public static string AutoNameRange(FsmStateAction action, NamedVariable min, NamedVariable max)
        {
            return action == null ? null : AutoNameRange(action.GetType().Name, min, max);
        }

        /// <summary>
        /// ActionName : min - max
        /// </summary>
        public static string AutoNameRange(string actionName, NamedVariable min, NamedVariable max)
        {
            return actionName + " : " + GetValueLabel(min) + " - " + GetValueLabel(max);
        }

        /// <summary>
        /// ActionName : var = value
        /// </summary>
        public static string AutoNameSetVar(FsmStateAction action, NamedVariable var, NamedVariable value)
        {
            return action == null ? null : AutoNameSetVar(action.GetType().Name, var, value);
        }

        /// <summary>
        /// ActionName : var = value
        /// </summary>
        public static string AutoNameSetVar(string actionName, NamedVariable var, NamedVariable value)
        {
            return actionName + " : " + GetValueLabel(var) + " = " + GetValueLabel(value);
        }

        /// <summary>
        /// [-Convert]ActionName : fromVar to toVar
        /// </summary>
        public static string AutoNameConvert(FsmStateAction action, NamedVariable fromVariable, NamedVariable toVariable)
        {
            return action == null ? null : AutoNameConvert(action.GetType().Name, fromVariable, toVariable);
        }

        /// <summary>
        /// [-Convert]ActionName : fromVar to toVar
        /// </summary>
        public static string AutoNameConvert(string actionName, NamedVariable fromVariable, NamedVariable toVariable)
        {
            return actionName.Replace("Convert","") + " : " + GetValueLabel(fromVariable) + " to " + GetValueLabel(toVariable);
        }

        /// <summary>
        /// ActionName : property -> store
        /// </summary>
        public static string AutoNameGetProperty(FsmStateAction action, NamedVariable property, NamedVariable store)
        {
            return action == null ? null : AutoNameGetProperty(action.GetType().Name, property, store);
        }

        /// <summary>
        /// ActionName : property -> store
        /// </summary>
        public static string AutoNameGetProperty(string actionName, NamedVariable property, NamedVariable store)
        {
            return actionName + " : " + GetValueLabel(property) + " -> " + GetValueLabel(store);
        }

        #endregion

        #region Editor helpers

#if UNITY_EDITOR

        /// <summary>
        /// Gets a rect that fits in the controls column of an inspector.
        /// </summary>
        /// <param name="height">Desired height.</param>
        public static Rect GetControlPreviewRect(float height)
        {
            var rect = GUILayoutUtility.GetRect(100f, 3000f, height, height);
            var labelWidth = EditorGUIUtility.labelWidth;
            rect.x += labelWidth + 5;
            rect.width -= labelWidth + 30;
            return rect;
        }
    
                public static Vector3 DoTargetPositionHandle(Vector3 worldPos, PositionOptions option, Transform owner, FsmGameObject target)
        {
            //var worldPos = GetTargetPosition(option, owner, target, position);

            EditorGUI.BeginChangeCheck();

            var rotation = Quaternion.identity;
            var newPos = worldPos;
           
            switch (option)
            {
                case PositionOptions.CurrentPosition:
                    break;

                case PositionOptions.WorldPosition:
                    newPos = Handles.PositionHandle(worldPos, rotation);
                    break;

                case PositionOptions.LocalPosition:
                    if (owner.parent != null)
                    {
                        rotation = owner.parent.transform.rotation;
                        newPos = owner.parent.InverseTransformPoint(Handles.PositionHandle(worldPos, rotation));
                    }
                    else
                    {
                        newPos = Handles.PositionHandle(worldPos, rotation);
                    }
                    break;

                case PositionOptions.WorldOffset:
                    newPos = Handles.PositionHandle(worldPos, rotation) - owner.position;
                    break;

                case PositionOptions.LocalOffset:
                    rotation = owner.rotation;
                    newPos = owner.InverseTransformPoint(Handles.PositionHandle(worldPos, rotation)) ;
                    break;

                case PositionOptions.TargetGameObject:
                    if (target.Value == null) return worldPos;
                    rotation = target.Value.transform.rotation;
                    newPos = target.Value.transform.InverseTransformPoint(Handles.PositionHandle(worldPos, rotation));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(owner, "Move Scene Gizmo");
            }

            return newPos;
        }

        /// <summary>
        /// Draws a Position Handle in the scene using a combination of GameObject and Position values.
        /// If a GameObject is specified, the Position is a local offset.
        /// If no GameObject is specified, the Position is a world position.
        /// Many actions use this setup. 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="go"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Vector3 PositionHandle(UnityEngine.Object owner, GameObject go, Vector3 position)
        {
            EditorGUI.BeginChangeCheck();

            Transform transform = null;
            var rotation = Quaternion.identity;            
            var worldPos = GetPosition(go, position);

            if (go != null)
            {
                transform = go.transform;
                rotation = transform.rotation;
                Handles.Label(transform.position, go.name, "Box");
                Handles.DrawDottedLine(transform.position, worldPos, 2f);
            }

            worldPos = Handles.PositionHandle(worldPos, rotation);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(owner, "Move Scene Gizmo");
            }

            return transform != null ? transform.InverseTransformPoint(worldPos) : worldPos;
        }

        /// <summary>
        /// Draws an arrow in the scene.
        /// Useful for actions that set a direction.
        /// </summary>
        public static void DrawArrow(Vector3 fromPos, Vector3 toPos, Color color, float arrowScale = 0.2f)
        {
            var direction = toPos - fromPos;
            if (direction.sqrMagnitude < 0.0001f) return;

            var lookAtRotation = Quaternion.LookRotation(direction);
            var distance = Vector3.Distance(fromPos, toPos);
            var handleSize = HandleUtility.GetHandleSize(toPos);
            var arrowSize = handleSize * arrowScale;

            var originalColor = Handles.color;
            Handles.color = color;

            Handles.DrawLine(fromPos, toPos);

#if UNITY_5_5_OR_NEWER
            Handles.ConeHandleCap(0, fromPos + direction.normalized * (distance - arrowSize), lookAtRotation, arrowSize, EventType.Repaint); // fudge factor to position cap correctly
#else
            Handles.ConeCap(0, fromPos + direction.normalized * (distance - arrowSize), lookAtRotation, arrowSize); // fudge factor to position cap correctly
#endif

            Handles.color = originalColor;
        }

        /// <summary>
        /// Get a mesh that can be used by Gizmos.DrawMesh to preview the mesh while editing.
        /// E.g. to preview a GameObject moving to a target
        /// </summary>
        public static Mesh GetPreviewMesh(GameObject go)
        {
            if (go == null) return null;

            var meshFilters = go.GetComponentsInChildren<MeshFilter>(false);
            if (meshFilters.Length == 0) return null;

            var combineList = new List<CombineInstance>();
            foreach (var meshFilter in meshFilters)
            {
                var combine = new CombineInstance
                {
                    mesh = meshFilter.sharedMesh,
                    transform = meshFilter.transform.localToWorldMatrix
                };

                combineList.Add(combine);
            }

            var combinedMesh = new Mesh();
            combinedMesh.CombineMeshes(combineList.ToArray());

            return combinedMesh;
        }

        /// <summary>
        /// Single color version of Handles.ScaleHandle.
        /// Useful when you have multiple editors (e.g. TweenScale)
        /// Note, does not handle value of 0 very well (fix?)
        /// </summary>
        public static Vector3 SingleColorScaleHandle(GameObject go, Vector3 scale, float handleSize, Color color)
        {
            var matrix = Handles.matrix;
            Handles.matrix = go.transform.localToWorldMatrix;
            Handles.matrix *= Matrix4x4.Inverse(Matrix4x4.Scale(go.transform.localScale));

            var tempColor = Handles.color;
            Handles.color = color;

            var scaleX = Handles.ScaleSlider(scale.x, 
                Vector3.zero, -Vector3.left, Quaternion.identity, handleSize, 0f);
            var scaleY = Handles.ScaleSlider(scale.y, 
                Vector3.zero, -Vector3.down, Quaternion.identity, handleSize, 0f);
            var scaleZ = Handles.ScaleSlider(scale.z, 
                Vector3.zero, -Vector3.back, Quaternion.identity, handleSize, 0f);

            Handles.color = tempColor;
            Handles.matrix = matrix;

            scale.Set(scaleX, scaleY, scaleZ);
            return scale;
        }

        /// <summary>
        /// Get a local bounding box for a GameObject
        /// </summary>
        public static Bounds GetLocalBounds(GameObject gameObject)
        {
            // See  GetLocalBounds in InternalEditorUtilityBindings.gen.cs in unity c# ref projects

            var rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform)
            {
                return new Bounds(rectTransform.rect.center, rectTransform.rect.size);
            }

            var renderer = gameObject.GetComponent<Renderer>();
            if (renderer is MeshRenderer)
            {
                var filter = renderer.GetComponent<MeshFilter>();
                if (filter != null && filter.sharedMesh != null)
                    return filter.sharedMesh.bounds;
            }

            var spriteRenderer = renderer as SpriteRenderer;
            if (spriteRenderer != null)
            {
                return spriteRenderer.bounds;
            }

            return new Bounds(Vector3.zero, Vector3.zero);
        }

        /// <summary>
        /// Draw wire bounding box for Transform.
        /// Optionally scale bounding box.
        /// </summary>
        public static void DrawWireBounds(Transform transform, Vector3 scale, Color color)
        {
            var matrix = Handles.matrix;
            Handles.matrix = transform.localToWorldMatrix;

            var bounds = GetLocalBounds(transform.gameObject);
            var size = bounds.size;
            size.Set(size.x * scale.x, size.y * scale.y, size.z * scale.z);

            DrawWireCube(Vector3.zero, size, color);

            Handles.matrix = matrix;
        }

        /// <summary>
        /// Draw wireframe bounding box around object with optional rotation (for editing gizmos)
        /// </summary>
        public static void DrawWireBounds(Transform transform, Quaternion rotate, Color color)
        {
            var matrix = Handles.matrix;
            Handles.matrix = Matrix4x4.TRS(transform.position, rotate, transform.lossyScale);

            DrawWireCube(Vector3.zero, GetLocalBounds(transform.gameObject).size, color);

            Handles.matrix = matrix;
        }

        public static void DrawWireBounds(Transform transform, Vector3 position, Quaternion rotation, Color color)
        {
            var matrix = Handles.matrix;
            Handles.matrix = Matrix4x4.TRS(position, rotation, transform.lossyScale);

            DrawWireCube(Vector3.zero, GetLocalBounds(transform.gameObject).size, color);

            Handles.matrix = matrix;
        }

        // Creates a rotation matrix. Note: Assumes unit quaternion
        public static Matrix4x4 Matrix4X4Rotate(Quaternion q)
        {
            // Pre-calculate coordinate products
            var x = q.x * 2.0F;
            var y = q.y * 2.0F;
            var z = q.z * 2.0F;
            var xx = q.x * x;
            var yy = q.y * y;
            var zz = q.z * z;
            var xy = q.x * y;
            var xz = q.x * z;
            var yz = q.y * z;
            var wx = q.w * x;
            var wy = q.w * y;
            var wz = q.w * z;

            // Calculate 3x3 matrix from orthonormal basis
            Matrix4x4 m;
            m.m00 = 1.0f - (yy + zz); m.m10 = xy + wz; m.m20 = xz - wy; m.m30 = 0.0F;
            m.m01 = xy - wz; m.m11 = 1.0f - (xx + zz); m.m21 = yz + wx; m.m31 = 0.0F;
            m.m02 = xz + wy; m.m12 = yz - wx; m.m22 = 1.0f - (xx + yy); m.m32 = 0.0F;
            m.m03 = 0.0F; m.m13 = 0.0F; m.m23 = 0.0F; m.m33 = 1.0F;
            return m;
        }

        /// <summary>
        /// Draw wireframe bounding box around object with optional translate, rotate, and scale (for editing gizmos)
        /// </summary>
        public static void DrawWireBounds(Transform transform, Vector3 translate, Quaternion rotate, Vector3 scale, Color color)
        {
            var matrix = Handles.matrix;
            Handles.matrix = transform.localToWorldMatrix;
            Handles.matrix *= Matrix4x4.TRS(translate, rotate, scale);
    

            DrawWireCube(Vector3.zero, GetLocalBounds(transform.gameObject).size, color);

            Handles.matrix = matrix;
        }

        /// <summary>
        /// Similar to Gizmos.DrawWireCube but can be used in editor code.
        /// </summary>
        public static void DrawWireCube(Vector3 position, Vector3 size, Color color)
        {
            var originalColor = Handles.color;
            Handles.color = color;

            var half = size / 2;
            // draw front
            Handles.DrawLine(position + new Vector3(-half.x, -half.y, half.z), position + new Vector3(half.x, -half.y, half.z));
            Handles.DrawLine(position + new Vector3(-half.x, -half.y, half.z), position + new Vector3(-half.x, half.y, half.z));
            Handles.DrawLine(position + new Vector3(half.x, half.y, half.z), position + new Vector3(half.x, -half.y, half.z));
            Handles.DrawLine(position + new Vector3(half.x, half.y, half.z), position + new Vector3(-half.x, half.y, half.z));
            // draw back
            Handles.DrawLine(position + new Vector3(-half.x, -half.y, -half.z), position + new Vector3(half.x, -half.y, -half.z));
            Handles.DrawLine(position + new Vector3(-half.x, -half.y, -half.z), position + new Vector3(-half.x, half.y, -half.z));
            Handles.DrawLine(position + new Vector3(half.x, half.y, -half.z), position + new Vector3(half.x, -half.y, -half.z));
            Handles.DrawLine(position + new Vector3(half.x, half.y, -half.z), position + new Vector3(-half.x, half.y, -half.z));
            // draw corners
            Handles.DrawLine(position + new Vector3(-half.x, -half.y, -half.z), position + new Vector3(-half.x, -half.y, half.z));
            Handles.DrawLine(position + new Vector3(half.x, -half.y, -half.z), position + new Vector3(half.x, -half.y, half.z));
            Handles.DrawLine(position + new Vector3(-half.x, half.y, -half.z), position + new Vector3(-half.x, half.y, half.z));
            Handles.DrawLine(position + new Vector3(half.x, half.y, -half.z), position + new Vector3(half.x, half.y, half.z));

            Handles.color = originalColor;
        }

#endif

        #endregion

        #region Obsolete

        /// <summary>
        /// Actions should use this for consistent error messages.
        /// Error will contain action name and full FSM path.
        /// </summary>
        [Obsolete("Use LogError instead.")]
        public static void RuntimeError(FsmStateAction action, string error)
        {
            action.LogError(action + " : " + error);
        }

        #endregion

    }
}
