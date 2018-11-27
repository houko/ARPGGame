// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0)
#define UNITY_PRE_5_1
#endif

//#define DEV_MODE

using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace HutongGames.PlayMakerEditor
{
    public class PreUpdateChecker : EditorWindow
    {
        // Categories used to report results
	    private readonly string[] checkCategories =
	    {
		    "Mecanim Animator Add-on", 
		    "Physics2D Add-on",
		    "Trigonometry Actions",
		    "Vector2 Actions",
		    "Quaternion Actions",
            "RectTransform Actions"
	    };

        // Where official actions are installed
        private readonly string[] officialPaths =
	    {
		    "PlayMaker/Actions/Animator", 
		    "PlayMaker/Actions/Physics2D", 
		    "PlayMaker/Actions/Trigonometry", 
		    "PlayMaker/Actions/Vector2", 
		    "PlayMaker/Actions/Quaternion",
            "PlayMaker/Actions/RectTransform"
	    };
	    
        
        // Files to check for in each category
        // dos cmd: dir *.cs /b >files.txt
        private readonly string[] checkFiles =
        {
            "AnimatorCrossFade.cs,AnimatorInterruptMatchTarget.cs,AnimatorMatchTarget.cs,AnimatorPlay.cs,AnimatorStartPlayback.cs,AnimatorStartRecording.cs,AnimatorStopPlayback.cs,AnimatorStopRecording.cs,GetAnimatorApplyRootMotion.cs,GetAnimatorBody.cs,GetAnimatorBoneGameObject.cs,GetAnimatorBool.cs,GetAnimatorCullingMode.cs,GetAnimatorCurrentStateInfo.cs,GetAnimatorCurrentStateInfoIsName.cs,GetAnimatorCurrentStateInfoIsTag.cs,GetAnimatorCurrentTransitionInfo.cs,GetAnimatorCurrentTransitionInfoIsName.cs,GetAnimatorCurrentTransitionInfoIsUserName.cs,GetAnimatorDelta.cs,GetAnimatorFeetPivotActive.cs,GetAnimatorFloat.cs,GetAnimatorGravityWeight.cs,GetAnimatorHumanScale.cs,GetAnimatorIKGoal.cs,GetAnimatorInt.cs,GetAnimatorIsHuman.cs,GetAnimatorIsLayerInTransition.cs,GetAnimatorIsMatchingTarget.cs,GetAnimatorIsParameterControlledByCurve.cs,GetAnimatorLayerCount.cs,GetAnimatorLayerName.cs,GetAnimatorLayersAffectMassCenter.cs,GetAnimatorLayerWeight.cs,GetAnimatorLeftFootBottomHeight.cs,GetAnimatorNextStateInfo.cs,GetAnimatorPivot.cs,GetAnimatorPlayBackSpeed.cs,GetAnimatorPlaybackTime.cs,GetAnimatorRightFootBottomHeight.cs,GetAnimatorRoot.cs,GetAnimatorSpeed.cs,GetAnimatorTarget.cs,NavMeshAgentAnimatorSynchronizer.cs,SetAnimatorApplyRootMotion.cs,SetAnimatorBody.cs,SetAnimatorBool.cs,SetAnimatorCullingMode.cs,SetAnimatorFeetPivotActive.cs,SetAnimatorFloat.cs,SetAnimatorIKGoal.cs,SetAnimatorInt.cs,SetAnimatorLayersAffectMassCenter.cs,SetAnimatorLayerWeight.cs,SetAnimatorLookAt.cs,SetAnimatorPlayBackSpeed.cs,SetAnimatorPlaybackTime.cs,SetAnimatorSpeed.cs,SetAnimatorStabilizeFeet.cs,SetAnimatorTarget.cs,SetAnimatorTrigger.cs,AnimatorFrameUpdateSelector.cs,GetAnimatorAnimatorRootActionEditor.cs,GetAnimatorBoneGameObjectActionEditor.cs,GetAnimatorBoolActionEditor.cs,GetAnimatorCurrentStateInfoActionEditor.cs,GetAnimatorCurrentStateInfoIsNameActionEditor.cs,GetAnimatorCurrentStateInfoIsTagActionEditor.cs,GetAnimatorCurrentTransitionInfoActionEditor.cs,GetAnimatorCurrentTransitionInfoIsNameActionEditor.cs,GetAnimatorCurrentTransitionInfoIsUserNameActionEditor.cs,GetAnimatorDeltaActionEditor.cs,GetAnimatorFloatActionEditor.cs,GetAnimatorGravityWeightActionEditor.cs,GetAnimatorIKGoalActionEditor.cs,GetAnimatorIntActionEditor.cs,GetAnimatorIsLayerInTransitionActionEditor.cs,GetAnimatorIsMatchingTargetActionEditor.cs,GetAnimatorLayerWeightActionEditor.cs,GetAnimatorNextStateInfoActionEditor.cs,GetAnimatorPivotActionEditor.cs,GetAnimatorSpeedActionEditor.cs,GetAnimatorTargetActionEditor.cs,SetAnimatorBoolActionEditor.cs,SetAnimatorFloatActionEditor.cs,SetAnimatorIntActionEditor.cs,OnAnimatorUpdateActionEditorBase.cs"
            ,"SetCollider2dIsTrigger.cs,AddForce2d.cs,AddTorque2d.cs,Collision2dEvent.cs,GetCollision2dInfo.cs,GetMass2d.cs,GetNextLineCast2d.cs,GetNextOverlapArea2d.cs,GetNextOverlapCircle2d.cs,GetNextOverlapPoint2d.cs,GetNextRayCast2d.cs,GetRayCastHit2dInfo.cs,GetSpeed2d.cs,GetTrigger2dInfo.cs,GetVelocity2d.cs,IsFixedAngle2d.cs,IsKinematic2d.cs,IsSleeping2d.cs,LineCast2d.cs,LookAt2d.cs,LookAt2dGameObject.cs,MousePick2d.cs,MousePick2dEvent.cs,RayCast2d.cs,ScreenPick2d.cs,SetGravity2d.cs,SetGravity2dScale.cs,SetHingeJoint2dProperties.cs,SetIsFixedAngle2d.cs,SetIsKinematic2d.cs,SetMass2d.cs,SetVelocity2d.cs,SetWheelJoint2dProperties.cs,Sleep2d.cs,SmoothLookAt2d.cs,Touch Object 2d Event.cs,Trigger2dEvent.cs,WakeAllRigidBodies2d.cs,WakeUp2d.cs"
            ,"GetACosine.cs,GetASine.cs,GetAtan.cs,GetAtan2.cs,GetAtan2FromVector2.cs,GetAtan2FromVector3.cs,GetCosine.cs,GetSine.cs,GetTan.cs"
            ,"DebugVector2.cs,GetVector2Length.cs,GetVector2XY.cs,SelectRandomVector2.cs,SetVector2Value.cs,SetVector2XY.cs,Vector2Add.cs,Vector2AddXY.cs,Vector2ClampMagnitude.cs,Vector2HighPassFilter.cs,Vector2Interpolate.cs,Vector2Invert.cs,Vector2Lerp.cs,Vector2LowPassFilter.cs,Vector2MoveTowards.cs,Vector2Multiply.cs,Vector2Normalize.cs,Vector2Operator.cs,Vector2PerSecond.cs,Vector2RotateTowards.cs,Vector2Substract.cs"
            ,"QuaternionCompare.cs,GetQuaternionEulerAngles.cs,GetQuaternionFromRotation.cs,GetQuaternionMultipliedByQuaternion.cs,GetQuaternionMultipliedByVector.cs,QuaternionAngleAxis.cs,QuaternionBaseAction.cs,QuaternionEuler.cs,QuaternionInverse.cs,QuaternionLerp.cs,QuaternionLookRotation.cs,QuaternionLowPassFilter.cs,QuaternionRotateTowards.cs,QuaternionSlerp.cs,GetQuaternionEulerAnglesCustomEditor.cs,GetQuaternionFromRotationCustomEditor.cs,GetQuaternionMultipliedByQuaternionCustomEditor.cs,GetQuaternionMultipliedByVectorCustomEditor.cs,QuaternionAngleAxisCustomEditor.cs,QuaternionCustomEditorBase.cs,QuaternionEulerCustomEditor.cs,QuaternionInverseCustomEditor.cs,QuaternionLerpCustomEditor.cs,QuaternionLookRotationCustomEditor.cs,QuaternionLowPassFilterCustomEditor.cs,QuaternionRotateTowardsCustomEditor.cs,QuaternionSlerpCustomEditor.cs"
            ,"RectTransformContainsScreenPoint.cs,RectTransformFlipLayoutAxes.cs,RectTransformGetAnchoredPosition.cs,RectTransformGetAnchorMax.cs,RectTransformGetAnchorMin.cs,RectTransformGetAnchorMinAndMax.cs,RectTransformGetLocalPosition.cs,RectTransformGetLocalRotation.cs,RectTransformGetOffsetMax.cs,RectTransformGetOffsetMin.cs,RectTransformGetPivot.cs,RectTransformGetRect.cs,RectTransformGetSizeDelta.cs,RectTransformPixelAdjustPoint.cs,RectTransformPixelAdjustRect.cs,RectTransformScreenPointToLocalPointInRectangle.cs,RectTransformScreenPointToWorldPointInRectangle.cs,RectTransformSetAnchoredPosition.cs,RectTransformSetAnchorMax.cs,RectTransformSetAnchorMin.cs,RectTransformSetAnchorMinAndMax.cs,RectTransformSetAnchorRectPosition.cs,RectTransformSetLocalPosition.cs,RectTransformSetLocalRotation.cs,RectTransformSetOffsetMax.cs,RectTransformSetOffsetMin.cs,RectTransformSetPivot.cs,RectTransformSetSizeDelta.cs,RectTransformWorldToScreenPoint.cs"    
        };

        /* TODO use these
        // official GUIDs for each file (generated by LogOfficialGUIDs)
        private readonly string[] fileGUIDs =
        {
            "c3264db07da4c49ffb5fe199371b98bb,18376cdddd26341f7b809912db503610,03cf67d8b733d4b37952890811925998,b66532287fa3a4997ae5dcfb967ff46c,57b6a2622136741beb45f95f75cf1433,a042efdd5fb4944abb7edadab645941a,ba34314c2df504f919e13699c71aafaf,199b0066830504bb88c7de83af154bda,7fa0f84fa8d7d48309bb7731893655b9,c22054fd1dd374c9a9c987d5cc6221f7,7666d8beb51fe44558679a70236fe940,6167f16993b5f498fbaa142e60ac1b8b,ca51ebe2658064289907fa675fcd5b5e,581a9005f2e8f49fb991e3b721ecc35a,3b6569490c2174e38b25f4763b428a89,07687adfe28594b449945d9abc9c3c29,0fe9a3bd420ec47b2bc6bdae6cdaa4ab,c30469b50b14848858f52b7fe7107982,df0ac08a28fbe4862874b2aaa02851ab,59828d3af79b24d328e2f0dfa5b4a3a7,e13fdcd155d24432ea5ddb9a289ddaef,7de2cc8e8be55430bb4b2c9a08001b9b,e3c384ee62d14458d8c070bbd88b0232,3e4e2701c656149199f7b424b0725b7d,3c5d635656f584a028aeaaa9cea3e773,b1565390c702f46aaa5cf4fa60f9e509,10472a7cdeeae4ec1b7a0a2958ad14ca,e5040ac0bba964af88c58865c193d634,1bdecd480242c4aca8287c38279d2fef,cb08474726d4c46db9951e90042f7e9a,acf2623f6aced45c9b16e208719d85cb,cdbcdbe70991c4b9f979da57c134c086,855199c246a444b1f9e5869cfd91d3c8,b217fe31f1ae848f9914c4fdbfbfdd01,9330353483e924d69b2fa7703fa61ca7,b38c867aa06344cb1a93127e55fd5dff,9f881f5e879b242e9bc5b7f5da438aec,252f9f13fe96d4f5b8ee1a309ee2e5db,7d22254eb077f4acba9bf302e62ef769,d19d957c781f6493fbbaed91c968c8ec,f6a5f9d7ea5f64549b2fc34b6b0cd5c7,cfa38da0ff4864b20be3fb1ec58d58bc,dc36092a0e28842d4bd974422ff9b206,e7921319b56514e1594bc135e632c4ea,533022f83905747cb8e71215824cadd4,0f372e08801dc40c890e1599595eaad9,84fa9e58ece094db8afb9d4f089d064a,b9d6946d83b0341ad8d4da88ed1f94f2,7e73917538b3a4e038a4970124142a06,8c086636265c24eb685a7a38abf3f843,331b242ab74d74798b8018c088452631,dd4e8c6c3305a4c848416eaf9f6dfbff,7f5b24930e6a94b16adf1f4b1f1d968d,1da53ccd0ab244a78be13e014adc7604,337fd0519278b4a4abfa114d7da5f03c,12ce8d3e805754f859045452539301b1,d08de24228e9d48f6966aef92918c88a,f883c091fa768400c9c82bbedff68386,5ca00c3f794764e0884a2e7e8bcb99fb,2d24ab33144b5435fbc2d271727badf4,e6766ded0ae1f47bdbb920e2e0a11be4,18313b45aff604fdfae21005cba51867,c01181456b38a445a9d46b1adb204fef,7016201662a0f43ef90cdc27ef28d2f3,c319a624316ec43e58ce312e218643d7,f46514705a56a44b18a3aaefe044562a,cb96adea5998a47ad856307f387542b2,20ecab07b14f24a31ad6aa95afe7b5f8,b88f6ceb1c984410fbd5b41563b9d2a1,a6ad0469f3b65455585bd6a67ace5231,b82f868d8d79e4446963165d7b7fe368,ca44151bd6a0241aa9306e42e12e4412,f2a733af2f8c941318e512d00865682f,97c1adb99041345a2b706883a0aa29f2,747e6c1106178455a98bb07727307e54,a3ce5496c052a4d51a21cd2d7a6b4ff6,dc486d0004d9c4ff0b33b013672e2819,b7b668bf47f0d4026a9a939be6dc69ec,1f0565b694e32469c9c06d051c31a156,f4d701619cd324b4f909bff1810e1aad,da454073bf02d49d48a28cb6ce4c0fc1,ad0e5154a3a6c46edafdfbec49bbb27d,e6467d12fbec648acbfe19a0ba022a0c,6927c99ea76df47c2b10e89e8b1e3f6a,d509be4bb6b01482f8d036452d23b539,3346f4d545f7340a4936b72e2e4c528d,ac8ba452b0457403caa4c60b0713a991,",
            "ea55526fdaa943b4bba87169f67a12f7,eaf0f3ed6b5d242d9af3ee4525911244,b6dd6adf8d6ac4bb6ad3d0021c9b8aa0,3fe37d4f2887249fbacdb30745b10d5f,63a94df6a55a542188e33a6ea28a9bde,dbc5681b28fff4b5782c9d86facecc4a,a70b105d5eff843e49ed438d206f1c1c,c018c087b7b4a4ef3a62da064be8c749,53e4c920ba9794755b18dd5cca83846b,a5fdbda89cf314991b58ea3e6b4eee19,58c2549eaa2174377a282d8c32fc1a35,b2e6f1fa5f4be42e58970f5beda35efc,75c2e65656a6142e19b0a37ad0ed8611,4720828ffbc044f9aad6b1b164bc38eb,c6c7f18245c1945cd87f8f62e48df25f,13aca478d9cc64a5998d7c7f459f5016,3326f7bf3c61f489ba5b2a7a817bd9bd,9ad0f181793d744abbfb5f25ef0cf183,a70b105d5eff843e49ed438d206f1c1c,d4400294ba87f4057a2bc118f8ec8ca2,15bd8590a698546aab33e8cd4ec9006b,da62defbd3fe34f0d979fbf900a1a9ba,19bec85c3f4fe43bd9365e5ee05b1cb8,58c2549eaa2174377a282d8c32fc1a35,b5c41daa984094cecbcfd973cad60d26,2e993b15a1ec247ca83fa651a184812a,8813d3367d00848619fb51a65822460b,0f35a67104d8a4d4aa91cf73117249f9,163a4c6c323834651b53b6165c36392e,203a12a4280db43fb9cb2650b4c470dd,6b165ecceb37e450e82a58ba6bd21853,58252178b9646486b9dda57c812ce9e1,369d94b9cb1275c41a665a7f3171689d,0660988017b964055a4f2c8f4ea48a2c,d6a02eab34e764d49930524cabac32c6,a6627f26893aa4ef2a2e9b51d93efbda,195f05ad3f0fd4ccb9fa0e9e2607aefe,3173d61ebbbc544d3964fd6488da8c4d,0599ede9318874bb8806f153794ea3db,",
            "6fda114c8a66e4296aed4e2a26a0611b,a8937798f5f704e5fa51b286a972dbe2,688a06197f3c54d278275cf084d6768e,2bf63cc97e56e4dcc8d616ebe3a04768,689ca023c18264a8eb07e17d24e4d3de,ed68244e96b0a47d7a0f089398480ef7,8ca5ca87ca84c4a94a2112392a257755,8e28a11b97806405eac27c765a5e86cd,858ac03f63eae483e95d68159febdf6b,",
            "a3dc9b6acbb9b463c894f6c5ee606989,a82ba85a9bb434407a8665e3affe57f8,60c8dd0dc3fd745a5841ca1d02a74726,285043e5e312a42df9ca1edcaa9c7c47,67463002770f84436ab893f31706f1ef,a42688746f7b64ebab25853041141133,e024d98d40a79474a954ede4f742f8b8,5755e237ef15847b1b87b8857345d126,78f25a93b238b4689a94fc24a8a3a596,eea0562bc1a9a4c11a0f5ad88c520337,4fd6cc0501d42495ca65aca36d58c666,955756b52dd7b425dbaaf0a3ab82d5ef,dde1f65f330694b05892a2430c3f0617,928126332a67546beb573aeaa2151c81,9d8d5cde65adb4dc7bf3f1f966af402a,e21c897c65f2b43da815643505c5445b,8a628084b2b4f4d19a367cedaa12154e,5c24d9da34967454482cc211b5f63d3a,c6a98fe858b7744a99c35a29a9fbc937,6174d235c04e64c728c5ea669181ef44,a3f7aef663ddf402a8e4843206f8368b,",
            "109faef134f274d3c8f0073c027ec212,04df43e98caae47479af52af6f7382aa,1a4862db90df44585aea2b190369fecb,4b09c7437bf4b46f7a7b0a2e0784df2b,b934f16194b9842e29683059db082a5a,e7b252bc184b64a9daacfb682ba75c3f,1ec09b8a624db4d78b78fa83e6ba57b2,1dfae9aec90344b698eb3a1bb52c7e7a,d981ac06313624fb3a1002f6e2b6af5f,c8fb9a75de503405bafbc360167a41f3,1ad91f05541104be0b4db97046df4dff,43aeb9060224147eda2dede205adf166,2eb0ae435013f4b73a5eade1acd952c7,4f3507d7651cd4beab8dfd4bf8a35aa8,3be6a1c071c024b41ba9f6177d1fe2c0,389626e75a012491dafdc591f89663ec,a15bb934be2514486aaba8aab6bad343,253be90cf8caf4bc2bd90962a721c804,e5cf62fd8a18e40bf98c7088582e1118,f90ab0e4f75b040fb805a1dfa7e4e070,9cfe22725f30c4b0795eab8d9abad7cf,273e324b1102f47538c3229d5d630b8f,f03cae33101aa45ffa317a40a938c8ab,648f82c14055f4d71936cfb3c0306ee5,b3c7d1bd8c46a45e387e06ee5ecedba7,6059b292f53004a1ab91e6f1bacc9df0,fc3d3cdb1536b4608b848887535f5b8e,",
        };*/

        private readonly List<string> allFilesInProject = new List<string>();
        private readonly List<string> allFilenames = new List<string>();
        private readonly List<string> failedCategories = new List<string>();
	    
	    private bool logCheckFindingToggle;
	    
        private Vector2 scrollPosition;

        [MenuItem("PlayMaker/Tools/Pre-Update Check", false, 66)]
        public static void Open()
        {
            GetWindow<PreUpdateChecker>(true);
        }

        private void OnEnable()
        {
            var titleText = string.Format("Pre-Update Check: PlayMaker {0}", PlayMakerWelcomeWindow.InstallCurrentVersion);

#if UNITY_PRE_5_1
            title = titleText;
#else
            titleContent = new GUIContent(titleText);
#endif
            minSize = new Vector2(400,400);

            DoCheck();
        }

        private void OnGUI()
        {
#if DEV_MODE
            if (GUILayout.Button("Log GUIDs"))
            {
                LogOfficialGUIDs();
            }


#endif
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            GUILayout.Label("PROJECT SCAN", EditorStyles.boldLabel);
            if (failedCategories.Count > 0)
            {
                var output = "The scan found these add-ons in your project:\n";
                foreach (var category in failedCategories)
                {
                    output += "\n- " + category;
                }
                output += "\n\nThese add-ons are now included in the main install. See Update Notes below for more info.\n";

                ConsoleTextArea(output);
            }
            else
            {
                ConsoleTextArea("The scan did not find any known conflicts in your project.\n");
            }

            GUILayout.Label("UPDATE NOTES", EditorStyles.boldLabel);

            EditorGUILayout.HelpBox("Always BACKUP projects before updating!" +
                                    "\nUse Version Control to track changes.", MessageType.Warning);

            // Version 1.9.0

            GUILayout.Label("Version 1.9.0", EditorStyles.boldLabel);

            EditorGUILayout.HelpBox(
                "\nPlayMaker 1.9.0 integrates UI actions and events." +
                "\n",
                MessageType.Info);

            // Version 1.8.6

            GUILayout.Label("Version 1.8.6", EditorStyles.boldLabel);

            EditorGUILayout.HelpBox(
                "\nPlayMaker 1.8.6 is more strict about changes allowed in Prefab Instances: "+
                "If a Prefab Instance is modified in a way that is incompatible with the Prefab Parent it will be disconnected. " +
                "You can reconnect Instances using Apply or Revert." +
                "\n",
                MessageType.Warning);

            // Version 1.8.5

            GUILayout.Label("Version 1.8.5", EditorStyles.boldLabel);
            
            EditorGUILayout.HelpBox(
                "\nPlayMaker 1.8.5 moved LateUpdate handling to an optional component automatically added as needed.\n" +
                "\nIf you have custom actions that use LateUpdate you must add this to OnPreprocess:\n" +
                "\nFsm.HandleLateUpdate = true;\n" +
                "\nSee Rotate.cs for an example.\n",
                MessageType.Info);

            // Version 1.8.2

            GUILayout.Label("Version 1.8.2", EditorStyles.boldLabel);

            EditorGUILayout.HelpBox("\nPlayMaker 1.8.2 added the following system events:\n" +
                                    "\nMOUSE UP AS BUTTON, JOINT BREAK, JOINT BREAK 2D, PARTICLE COLLISION." +
                                    "\n\nPlease remove any custom proxy components you used before to send these events.\n",
                                    MessageType.Info);

            // Version 1.8.1

            GUILayout.Label("Version 1.8.1", EditorStyles.boldLabel);

            var notes181 = "\nPlayMaker 1.8.1 integrated the following add-ons and actions:\n" +
                           "\n- Physics2D Add-on" +
                           "\n- Mecanim Animator Add-on" +
                           "\n- Vector2 Actions\n- Quaternion Actions\n- Trigonometry Actions\n";

            if (failedCategories.Count > 0)
            {
                notes181 +=
                    "\nIf you imported these actions from official unitypackages the update should replace them automatically." +
                    "\n\nIf you downloaded these actions from the Ecosystem or Forums you might get errors from duplicate files after updating." +
                    "\n\nYou can either remove these files before updating, or remove duplicate files to fix any errors after updating.\n"
                    ;
            }
            else
            {
                notes181 += "\nThe Update Check did not find any of these files in your project. " +
                            "\n\nIf you think you have some of these actions in your project, " +
                            "you can either remove them before updating, " +
                            "or remove duplicate files to fix any errors after updating.\n";
            }

            notes181 += "\nThe updated files will be located under:\nAssets/PlayMaker/Actions" +
                        "\n\nOlder files are most likely under:\nAssets/PlayMaker Custom Actions\n";

            EditorGUILayout.HelpBox(notes181, failedCategories.Count > 0 ? MessageType.Warning : MessageType.Info);

            GUILayout.FlexibleSpace();
            GUILayout.EndScrollView();
	        
	        GUILayout.BeginHorizontal();
            if (GUILayout.Button("Run Pre-Update Check Again"))
            {
                DoCheck();
            }
	        logCheckFindingToggle = GUILayout.Toggle(logCheckFindingToggle,"Log Check Findings",GUILayout.Width(130));
	        GUILayout.EndHorizontal();
        }

        private static GUIStyle consoleStyle;

        private static void InitConsoleStyle()
        {
            if (consoleStyle == null)
            {
                consoleStyle = new GUIStyle(EditorStyles.textArea)
                {
                    normal = {textColor = Color.green}
                };
            }
        }

        private static void ConsoleTextArea(string text)
        {
            InitConsoleStyle();

            text = "Scanning project...\n\n" + text;

            var bgColor = GUI.backgroundColor;
            GUI.backgroundColor = new Color(0.3f,0.5f,0.3f);
            GUILayout.Label(text, consoleStyle);
            GUI.backgroundColor = bgColor;
        }

        public void DoCheck()
        {
            ScanProject();
            
            // check each category

            for (var i = 0; i < checkCategories.Length; i++)
            {
                if (ProjectHasAnyOfTheseFiles(checkFiles[i].Split(','), officialPaths[i]))
                {
                    failedCategories.Add(checkCategories[i]);
                }
            }
        }

        private void ResetCheck()
        {
            allFilesInProject.Clear();
            allFilenames.Clear();
            failedCategories.Clear();
        }

        private void ScanProject()
        {
            ResetCheck();

            //var playmakerActionsPath = Application.dataPath + "/PlayMaker/Actions/";

            // get all script files in project
            allFilesInProject.AddRange(Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories));
            for (var i = 0; i < allFilesInProject.Count; i++)
            {
                allFilesInProject[i] = allFilesInProject[i].Replace('\\','/');
            }

            /*
            // remove files under PlayMaker/Actions folder
            foreach (var file in allFilesInProject.ToArray())
            {
                //Debug.Log(file);
                if (file.Contains(playmakerActionsPath))
                {
                    Debug.Log("Remove: " + file);
                    allFilesInProject.Remove(file);
                }
            }*/

            // process paths
            
            allFilenames.AddRange(allFilesInProject);

            for (var i = 0; i < allFilesInProject.Count; i++)
            {
                // get filename for easy comparison                
                allFilenames[i] = Path.GetFileName(allFilesInProject[i]);

                // make paths relative to project
                allFilesInProject[i] = allFilesInProject[i].Remove(0, Application.dataPath.Length - 6);
            }
        }

        private bool ProjectHasAnyOfTheseFiles(IEnumerable<string> files, string excludePath)
	    {	    
		    var foundFile = false;

            foreach (var file in files)
            {
                foreach (var checkFile in allFilesInProject)
                {
                    if (checkFile.Contains(excludePath)) continue;

                    if (checkFile.Contains(file))
                    {
                        if (logCheckFindingToggle)
                	    {
                		    Debug.Log(
	                		    "PlayMaker Pre-UpdateCheck Found the following file (click to Ping):\n" + checkFile,
	                		    AssetDatabase.LoadAssetAtPath(checkFile, typeof(Object))
                		    );
                	    }

	                    foundFile =  true;
                    }
                }

                /*
                if (allFilenames.Contains(file))
                {
                	if (logCheckFindingToggle)
                	{
                		var _filePath = allFilesInProject[allFilenames.IndexOf(file)];
                		Debug.Log(
	                		"PlayMaker Pre-UpdateCheck Found the following file (click to Ping):\n"+_filePath,
	                		AssetDatabase.LoadAssetAtPath(_filePath,typeof(Object))
                		);
                	}
	                _foundFile =  true;
                }*/
            }
	        
		    return foundFile;
        }

#if DEV_MODE

        private string FindFile(string filename)
        {
            Debug.Log("FindFile: " + filename);
            foreach (var path in allFilesInProject)
            {
                //Debug.Log(path);
                if (path.Contains(filename)) return path;
            }
            return string.Empty;
        }

        // used to generate fileGUIDs
        // the official GUIDs for updated files
        private void LogOfficialGUIDs()
        {
            ScanProject();

            var output = "        private readonly string[] fileGUIDs =\n        {";

            for (var i = 0; i < checkCategories.Length; i++)
            {
                output += "\n            \"";

                var files = checkFiles[i].Split(',');
                for (var j = 0; j < files.Length; j++)
                {
                    var assetPath = FindFile(files[j]);
                    if (!string.IsNullOrEmpty(assetPath))
                    {
                        if (assetPath.Contains("Assets/PlayMaker/Actions/"))
                        {
                            output += AssetDatabase.AssetPathToGUID(assetPath) + ",";
                        }
                    }
                }

                output += "\",";
            }

            output += "\n        };\n\n";

            Debug.Log(output);
        }
#endif
    }
}

