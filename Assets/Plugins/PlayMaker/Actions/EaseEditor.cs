// #define DEBUG_EASEEDITOR

#if UNITY_EDITOR

using HutongGames.PlayMaker;
using UnityEditor;
using UnityEngine;

namespace HutongGames
{
	public class EaseEditor
	{
        /* Cache if needed for perf
	    private static Dictionary<Vector3[], EasingFunction.Ease> pointCache;
	    private int cachedStepCount;

	    private static void BuildPointCache()
	    {
	    }
        */

	    private static EasingFunction.Ease debugged;

        private const int maxCurvePoints = 200;
        private static readonly Vector3[] pointBuffer = new Vector3[maxCurvePoints];

        /* WIP edit time scrub bar and play button
         Maybe move to own class so can re-use (e.g. ActionHelpers)
        private static readonly GUIContent scrubLabel = new GUIContent("Preview", "Preview tween in the Scene View.");

	    private static Texture _playIcon;

	    private static Texture playIcon
	    {
	        get
	        {
	            if (_playIcon == null)
	            {
	                _playIcon = EditorGUIUtility.IconContent("IN foldout act").image;
	            }

	            return _playIcon;
	        }
	    }

	    private static GUIStyle _buttonStyle;

	    private static GUIStyle buttonStyle
	    {
	        get
	        {
	            if (_buttonStyle == null)
	            {
                    _buttonStyle = new GUIStyle("Button");
                    _buttonStyle.padding = new RectOffset(2,2,0,0);
	            }

	            return _buttonStyle;
	        }
	    }
        */

	    /// <summary>
	    /// Draws scrubbing controls and a preview curve for EasingFunctions
	    /// </summary>
	    /// <param name="ease">The  Ease function to preview</param>
	    /// <param name="playPreview">Did the user hit Play in the scrubbing slider</param>
	    /// <param name="currentTime">Used to draw the current time on the preview</param>
	    /// <param name="currentTimeColor">Color used to draw current time</param>
	    public static void DrawPreviewCurve(EasingFunction.Ease ease, ref bool playPreview, ref float currentTime, Color currentTimeColor)
	    {
            /* WIP edit time preview
	        if (!EditorApplication.isPlaying)
	        {
                GUILayout.BeginHorizontal();

	            EditorGUILayout.PrefixLabel(scrubLabel);

	            playPreview = GUILayout.Toggle(playPreview, playIcon, buttonStyle, GUILayout.Height(15f), GUILayout.Width(17f));

                EditorGUI.BeginChangeCheck();
	            currentTime = EditorGUILayout.Slider(currentTime, 0, 1);
	            if (EditorGUI.EndChangeCheck())
	                playPreview = false;

                GUILayout.EndHorizontal();
	        }*/

	        if (ease == EasingFunction.Ease.CustomCurve) return;

	        var area = ActionHelpers.GetControlPreviewRect(40f);

            // some ease functions overshoot 0 and 1
            // so leave some margins at top/bottom

	        const float overflow = .25f;
	        var yRange = area.height * (1 - 2 * overflow);

            // setup the range

            var origin = new Vector2(area.x, area.yMax - area.height * overflow);
            var yMax = new Vector2(area.x, area.y + area.height * overflow);

            // draw axis (horizontal lines at 0 and 1)

	        var axisColor = EditorStyles.label.normal.textColor;
	        axisColor.a = 0.2f;
	        Handles.color = axisColor;

            Handles.DrawLine(origin, new Vector3(area.xMax, origin.y));
	        Handles.DrawLine(yMax, new Vector3(area.xMax, yMax.y));

            // draw curve

	        Handles.color = EditorStyles.label.normal.textColor;

	        var func = EasingFunction.GetEasingFunction(ease);
	        var derivFunc = EasingFunction.GetEasingFunctionDerivative(ease);

	        var numSampleSteps = (int) area.width / 2;

            // add start point: (0,0)

	        pointBuffer[0] = new Vector3(origin.x, origin.y);

	        // only add points to the curve when curve threshold is exceeded
            // e.g. so a linear ease will only have 2 points

	        const float curveThreshold = 0.05f;
	        var prevSlope = 0f;

	        var pointIndex = 1;
	        for (var i = 1; i < numSampleSteps && pointIndex < maxCurvePoints-1 ; i++)
	        {
	            var step = i / (float) numSampleSteps;
	            var y = func(0, 1, step);
	            var slope = derivFunc(0, 1, step);

	            if (Mathf.Abs(slope - prevSlope) > curveThreshold ||
                    Mathf.Sign(slope) != Mathf.Sign(prevSlope))
	            {
	                prevSlope = slope;
	                pointBuffer[pointIndex++] = new Vector3(origin.x + step * area.width, origin.y - y * yRange);
	            }
	        }

            // add last point: (1,1)

            pointBuffer[pointIndex] = new Vector3(area.xMax, origin.y - yRange);
	        
	        // draw the curve

	        Handles.DrawAAPolyLine(2, pointIndex+1, pointBuffer);

            // draw current time
                    
	        if (Application.isPlaying && currentTime > 0)
	        {
	            var x = area.x + area.width * currentTime;
	            var y = origin.y -func(0, 1, currentTime) * yRange;

	            var guiColor = GUI.color;
	            GUI.color = currentTimeColor;
                GUI.DrawTexture(new Rect(x-1,y-1,2,2), EditorGUIUtility.whiteTexture);
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 0.2f);
	            GUI.DrawTexture(new Rect(x-1,area.y,2,area.height), EditorGUIUtility.whiteTexture);
	            GUI.color = guiColor;

	            //Handles.DrawLine(new Vector3(x, area.y), new Vector3(x, area.yMax));
	        }

            // need to do this?
	        Handles.color = Color.white;


#if DEBUG_EASEEDITOR

// debug the number of points generated when sampling this curve

            if (debugged != ease && pointIndex > 1)
	        {	            
	            Debug.Log(pointIndex);
	            debugged = ease;
	        }
#endif

	    }
	}
}

#endif