using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMakerEditor
{
    [CustomActionEditor(typeof(HutongGames.PlayMaker.Actions.MoveTowards))]
    public class MoveTowardsActionEditor : CustomActionEditor
    {
        public override bool OnGUI()
        {
            return DrawDefaultInspector();
        }

        public override void OnSceneGUI()
        {
            var moveTowardsAction = (HutongGames.PlayMaker.Actions.MoveTowards) target;

            if (moveTowardsAction.UpdateTargetPos())
            {
                var go = target.Fsm.GetOwnerDefaultTarget(moveTowardsAction.gameObject);
                var goTransform = go.transform;
                var goPosition = goTransform.position;

                var lookAtPosition = moveTowardsAction.GetTargetPos();
                var lookAtVector = lookAtPosition - goPosition;
                if (lookAtVector == Vector3.zero) return;
                var lookAtRotation = Quaternion.LookRotation(lookAtVector);

                var handleSize = HandleUtility.GetHandleSize(goPosition);
                var arrowSize = handleSize*0.2f;
                var distance = (lookAtPosition - goPosition).magnitude;

                var goTarget = moveTowardsAction.targetObject.Value;

                // Position handles

                if (!moveTowardsAction.targetPosition.IsNone)
                {
                    if (goTarget != null)
                    {
                        // Edit local offset from target object

                        var goTargetTransform = goTarget.transform;
                        var worldTargetPos = goTargetTransform.TransformPoint(moveTowardsAction.targetPosition.Value);

                    moveTowardsAction.targetPosition.Value = goTargetTransform.InverseTransformPoint(Handles.PositionHandle(worldTargetPos, goTarget.transform.rotation));
                        Handles.color = new Color(1, 1, 1, 0.2f);
                        Handles.DrawLine(goTargetTransform.position, moveTowardsAction.GetTargetPosWithVertical());
                    }
                    else
                    {
                        // Edit world position

                    moveTowardsAction.targetPosition.Value = Handles.PositionHandle(moveTowardsAction.targetPosition.Value, Quaternion.identity);
                    }
                }

                // Target vector

                Handles.DrawLine(goPosition, lookAtPosition);

#if UNITY_5_5_OR_NEWER
                Handles.ConeHandleCap(0, goPosition + lookAtVector.normalized * (distance - arrowSize * 0.7f), lookAtRotation, arrowSize, EventType.Repaint); // fudge factor to position cap correctly
#else
                Handles.ConeCap(0, goPosition + lookAtVector.normalized * (distance - arrowSize * 0.7f), lookAtRotation, arrowSize); // fudge factor to position cap correctly
#endif
                // Show vertical offset

                if (moveTowardsAction.ignoreVertical.Value)
                {
                    Handles.DrawLine(lookAtPosition, moveTowardsAction.GetTargetPosWithVertical());
                }

                if (GUI.changed)
                {
                    FsmEditor.EditingActions();
                    FsmEditor.Repaint(true);
                }
            }
        }
    }
}
