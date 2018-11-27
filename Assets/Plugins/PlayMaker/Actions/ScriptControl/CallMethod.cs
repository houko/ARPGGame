// (c) copyright Hutong Games, LLC 2010-2012. All rights reserved.

using System;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Call a method in a component on a GameObject.")]
    public class CallMethod : FsmStateAction
    {
        [ObjectType(typeof(Component))]
        [Tooltip("The behaviour on a GameObject that has the method you want to call. " +
                 "Drag the script component from the Unity inspector into this slot. " +
                 "HINT: Use Lock if the script is on another GameObject." +
                 "\n\nNOTE: Unity Object fields only show the GameObject name, " +
                 "so for clarity we show the Behaviour name in a readonly field below.")]
        public FsmObject behaviour;

        //[UIHint(UIHint.Method)]
        [Tooltip("Name of the method to call on the component")]
        public FsmString methodName;

        [Tooltip("Method parameters. NOTE: these must match the method's signature!")]
        public FsmVar[] parameters;

        [ActionSection("Store Result")]

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the result of the method call.")]
        public FsmVar storeResult;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [Tooltip("Use the old manual editor UI.")]
        public bool manualUI;

        private FsmObject cachedBehaviour;
        private FsmString cachedMethodName;
        private Type cachedType;
        private MethodInfo cachedMethodInfo;
        private ParameterInfo[] cachedParameterInfo;
        private object[] parametersArray;
        private string errorString;

        public override void Reset()
        {
            behaviour = null;
            methodName = null;
            parameters = null;
            storeResult = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            parametersArray = new object[parameters.Length];

            DoMethodCall();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoMethodCall();
        }

        private void DoMethodCall()
        {
            if (behaviour.Value == null)
            {
                Finish();
                return;
            }

            if (NeedToUpdateCache())
            {
                if (!DoCache())
                {
                    Debug.LogError(errorString);
                    Finish();
                    return;
                }
            }

            object result;
            if (cachedParameterInfo.Length == 0)
            {
                result = cachedMethodInfo.Invoke(cachedBehaviour.Value, null);
            }
            else
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    parameter.UpdateValue();
                    if (parameter.Type == VariableType.Array)
                    {
                        parameter.UpdateValue();
                        var objectArray = parameter.GetValue() as object[];
                        var realType = cachedParameterInfo[i].ParameterType.GetElementType();
                        var convertedArray = Array.CreateInstance(realType, objectArray.Length);
                        for (int index = 0; index < objectArray.Length; index++)
                            convertedArray.SetValue(objectArray[index], index);
                        parametersArray[i] = convertedArray;
                    }
                    else
                    {
                        parameter.UpdateValue();
                        parametersArray[i] = parameter.GetValue();
                    }             
                }

                result = cachedMethodInfo.Invoke(cachedBehaviour.Value, parametersArray);
            }

            if (storeResult != null && !storeResult.IsNone && storeResult.Type != VariableType.Unknown)
            {
                storeResult.SetValue(result);
            }
        }

        // TODO: Move tests to helper function in core
        private bool NeedToUpdateCache()
        {
            return cachedBehaviour == null || cachedMethodName == null || // not cached yet
                cachedBehaviour.Value != behaviour.Value ||     // behavior value changed
                cachedBehaviour.Name != behaviour.Name ||       // behavior variable name changed
                cachedMethodName.Value != methodName.Value ||   // methodName value changed
                cachedMethodName.Name != methodName.Name;       // methodName variable name changed
        }

        private void ClearCache()
        {
            cachedBehaviour = null;
            cachedMethodName = null;
            cachedType = null;
            cachedMethodInfo = null;
            cachedParameterInfo = null;
        }

        private bool DoCache()
        {
            //Debug.Log("DoCache");
            
            ClearCache();
            errorString = string.Empty;
            cachedBehaviour = new FsmObject(behaviour);
            cachedMethodName = new FsmString(methodName);

            if (cachedBehaviour.Value == null)
            {
                if (behaviour.UsesVariable && !Application.isPlaying)
                {
                    // Value might be set at runtime
                    // Display/Log this info...?
                }
                else
                {
                    errorString += "Behaviour is invalid!\n";
                }
                Finish();
                return false;
            }

            cachedType = behaviour.Value.GetType();

            var types = new List<Type>(parameters.Length);
            foreach (var each in parameters)
            {
                types.Add(each.RealType);
            }

#if NETFX_CORE
            var methods = cachedType.GetTypeInfo().GetDeclaredMethods(methodName.Value);
            foreach (var method in methods)
            {
                if (TestMethodSignature(method, types))
                {
                    cachedMethodInfo = method;
                }
            }
#else
            cachedMethodInfo = cachedType.GetMethod(methodName.Value, types.ToArray());
#endif
            if (cachedMethodInfo == null)
            {
                errorString += "Invalid Method Name or Parameters: " + methodName.Value + "\n";
                Finish();
                return false;
            }              

            cachedParameterInfo = cachedMethodInfo.GetParameters();

            return true;
        }

#if NETFX_CORE
        private bool TestMethodSignature(MethodInfo method, List<Type> parameterTypes)
        {
            if (method == null) return false;
            var methodParameters = method.GetParameters();
            if (methodParameters.Length != parameterTypes.Count) return false;
            for (var i = 0; i < methodParameters.Length; i++)
            {
                if (!ReferenceEquals(methodParameters[i].ParameterType, parameterTypes[i]))
                {
                    return false;
                }
            }
            return true;
        }
#endif

        public override string ErrorCheck()
        {
            /* We could only error check if when we re-cache,
             * however NeedToUpdateCache() is not super robust
             * So for now we just re cache every frame in editor
             * Need to test editor perf...
            if (!NeedToUpdateCache())
            {
                return errorString; // last error message
            }*/

            if (Application.isPlaying)
            {
                return errorString; // last error message
            }

            if (!DoCache())
            {
                return errorString;
            }

            if (parameters.Length != cachedParameterInfo.Length)
            {
                return "Parameter count does not match method.\nMethod has " + cachedParameterInfo.Length + " parameters.\nYou specified " + parameters.Length + " paramaters.";
            }

            for (var i = 0; i < parameters.Length; i++)
            {
                var p = parameters[i];
                var paramType = p.RealType;
                var paramInfoType = cachedParameterInfo[i].ParameterType;
                if (!ReferenceEquals(paramType, paramInfoType))
                {
                    return "Parameters do not match method signature.\nParameter " + (i + 1) + " (" + paramType + ") should be of type: " + paramInfoType;
                }
            }

            if (ReferenceEquals(cachedMethodInfo.ReturnType, typeof(void)))
            {
                if (!string.IsNullOrEmpty(storeResult.variableName))
                {
                    return "Method does not have return.\nSpecify 'none' in Store Result.";
                }
            }
            else if (!ReferenceEquals(cachedMethodInfo.ReturnType, storeResult.RealType))
            {
                return "Store Result is of the wrong type.\nIt should be of type: " + cachedMethodInfo.ReturnType;
            }

            return string.Empty;
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            var name = methodName + "(";
            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                name += ActionHelpers.GetValueLabel(param.NamedVar);
                if (i < parameters.Length - 1)
                {
                    name += ",";
                }
            }
            name += ")";

            if (!storeResult.IsNone)
            {
                name = storeResult.variableName + "=" + name;
            }

            return name;
        }
#endif
    }
}
