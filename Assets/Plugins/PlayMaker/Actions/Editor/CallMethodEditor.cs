using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMakerEditor
{
    /// <summary>
    /// TODO: 
    /// Keep parameters when changing method signature?
    /// Copy/paste parameter values?
    /// Ability to add custom menu item to Action Settings menu?
    /// </summary>
    [CustomActionEditor(typeof(HutongGames.PlayMaker.Actions.CallMethod))]
    public class CallMethodEditor : CustomActionEditor
    {
        private CallMethod callMethod;
        private Type cachedType;
        private string cachedTypeName;
        private MethodInfo cachedMethod;
        private string cachedMethodSignature;
        private ParameterInfo[] cachedParameters;
        private GUIContent[] cachedParameterLabels;
        private GUIContent cachedReturnLabel;
        private object[] parameterAttributes;
        private object[] returnAttributes;

        private string methodSignature;
        private bool initialized;

        public override void OnEnable()
        {
            //Debug.Log("OnEnable");
            
            Initialize();
        }

        private void Initialize()
        {
            if (initialized) return;

            UpdateCache();

            parameterAttributes = new object[] { new HideTypeFilter() };
            returnAttributes = new object[] { new UIHintAttribute(UIHint.Variable), new HideTypeFilter() };

            methodSignature = GetMethodSignatureFromSettings();
            //Debug.Log(methodSignature);
            var method = TypeHelpers.FindMethod(cachedType, methodSignature);
            InitMethod(method);

            initialized = true;
        }

        private void UpdateCache()
        {
            callMethod = target as CallMethod;
            if (callMethod != null)
            {
                if (callMethod.parameters == null)
                {
                    callMethod.parameters = new FsmVar[0];
                }

                if (callMethod.methodName == null)
                {
                    callMethod.methodName = new FsmString();
                }

                if (callMethod.behaviour != null)
                {
                    cachedType = callMethod.behaviour.ObjectType;
                    cachedTypeName = cachedType != null ? cachedType.FullName : "";
                    //Debug.Log(cachedTypeName);
                }
                else
                {
                    //Debug.Log("callMethod.behaviour == null");
                }
            }
        }

        private void CheckCache()
        {
            if (callMethod.behaviour != null)
            {
                if (cachedType != callMethod.behaviour.ObjectType)
                {
                    ClearCache();
                    UpdateCache();
                    return;
                }
                
                // NOTE: Temp fix. Permanent fix in VariableEditor.cs in 1.8.3
                if (callMethod.behaviour.Value != null && callMethod.behaviour.Value.GetType() != callMethod.behaviour.ObjectType)
                {
                    callMethod.behaviour.ObjectType = callMethod.behaviour.Value.GetType();
                    ClearCache();
                    UpdateCache();
                }

                // NOTE: Temp fix. Permanent fix in VariableEditor.cs in 1.8.3
                if ((!callMethod.behaviour.UseVariable && callMethod.behaviour.Value == null) || 
                    callMethod.behaviour.IsNone)
                {
                    if (callMethod.behaviour.ObjectType != typeof (Component))
                    {
                        callMethod.behaviour.ObjectType = typeof (Component);
                        callMethod.methodName = "";
                        ClearCache();
                        UpdateCache();
                    }
                }

                /*
                // User Reset the action
                if (!callMethod.behaviour.UseVariable && callMethod.behaviour.Value == null && !string.IsNullOrEmpty(cachedTypeName))
                {
                    ClearCache();
                }*/
            }            
        }

        public override bool OnGUI()
        {
            if (callMethod.manualUI)
            {
                return DrawDefaultInspector();
            }

            EditField("behaviour");
            FsmEditorGUILayout.ReadonlyTextField(cachedTypeName);

            CheckCache();

            EditorGUILayout.BeginHorizontal();
            FsmEditorGUILayout.PrefixLabel("Method");
            var buttonRect = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.popup);
            var methodName = callMethod.methodName != null ? callMethod.methodName.Value : "";
            if (GUI.Button(buttonRect, methodName, EditorStyles.popup))
            {
                TypeHelpers.GenerateMethodMenu(cachedType, SelectMethod).DropDown(buttonRect);
            }
            EditorGUILayout.EndHorizontal();

            FsmEditorGUILayout.ReadonlyTextField(cachedMethodSignature);


            //EditorGUI.indentLevel++;

            if (cachedParameters != null && cachedParameters.Length > 0)
            {
                GUILayout.Label("Parameters", EditorStyles.boldLabel);

                for (int i = 0; i < cachedParameters.Length; i++)
                {
                    //GUILayout.Label(callMethod.parameters[i].RealType.ToString());

                    callMethod.parameters[i] = FsmEditor.ActionEditor.EditFsmVar(cachedParameterLabels[i],
                        callMethod.parameters[i], parameterAttributes);
                }
            }

            if (cachedMethod != null && cachedMethod.ReturnType != typeof (void))
            {
                GUILayout.Label("Return", EditorStyles.boldLabel);
                
                callMethod.storeResult = FsmEditor.ActionEditor.EditFsmVar(cachedReturnLabel, callMethod.storeResult, returnAttributes);
            }

            //EditorGUI.indentLevel--;

            FsmEditorGUILayout.LightDivider();

            EditField("everyFrame");

            FsmEditorGUILayout.LightDivider();

            EditField("manualUI");

            if (GUI.changed)
            {
                UpdateCache();
            }

            return GUI.changed;
        }

        // NOTE: This should match format generated by TypeHelpers.
        private string GetMethodSignatureFromSettings()
        {
            if (callMethod.methodName == null || callMethod.parameters == null ||  callMethod.storeResult == null) return "";
            return TypeHelpers.GetMethodSignature(callMethod.methodName.Value, callMethod.parameters, callMethod.storeResult);
        }

        private void ClearCache()
        {
            //Debug.Log("ClearCache");
            cachedMethod = null;
            cachedMethodSignature = null;
            cachedParameters = null;
            cachedParameterLabels = null;
        }

        private void InitMethod(MethodInfo method)
        {
            if (method == null)
            {
                //Debug.Log("InitMethod: None");
                
                // TODO: select none
                ClearCache();
            }
            else
            {
                //Debug.Log("InitMethod: " + method.Name);

                cachedMethod = method;
                cachedMethodSignature = TypeHelpers.GetMethodSignature(method);
                cachedParameters = method.GetParameters();
                cachedParameterLabels = new GUIContent[cachedParameters.Length];
                callMethod.methodName.Value = method.Name;

                for (int i = 0; i < cachedParameters.Length; i++)
                {
                    var parameter = cachedParameters[i];
                    cachedParameterLabels[i] = new GUIContent(Labels.NicifyVariableName(parameter.Name));
                }

                cachedReturnLabel = new GUIContent("Store Result", Labels.GetTypeTooltip(method.ReturnType));
            }

        }

        private void SelectMethod(object userdata)
        {
            var method = userdata as MethodInfo;
            if (method == null)
            {
                //Debug.Log("Select Method: None");

                // TODO: select none
                ClearCache();
            }
            else
            {
                //Debug.Log("Select Method: " + method.Name);
                
                InitMethod(method);

                callMethod.parameters = new FsmVar[cachedParameters.Length];
                for (int i = 0; i < cachedParameters.Length; i++)
                {
                    callMethod.parameters[i] = new FsmVar(cachedParameters[i].ParameterType);
                }

                callMethod.storeResult = new FsmVar(method.ReturnType);

                FsmEditor.SaveActions();
            }
        }
    }
}
