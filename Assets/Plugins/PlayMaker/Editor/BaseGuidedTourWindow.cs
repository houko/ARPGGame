#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0)
#define UNITY_PRE_5_1
#endif

using System;
using UnityEngine;
using UnityEditor;
using HutongGames.Editor;

namespace HutongGames.PlayMakerEditor
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for a Guided Tour Window 
    /// Uses GuidedTour to organize topics
    /// Uses HtmlText for help text with links etc.
    /// TODO: Remove some dependencies and move to HutongGames.Editor so we can use in other projects
    /// </summary>
    public abstract class BaseGuidedTourWindow : BaseEditorWindow
    {
        protected GuidedTour guidedTour = new GuidedTour();
        
        // Main scroll view

        protected Vector2 scrollPosition;
        private float scrollViewHeight;
        private Rect selectedRect;
        private bool autoScroll;

        // Help panel

        private Vector2 helpScrollPosition;     
        private GUIStyle helpStyle;

        // Splitter between topics and help panel

        private Rect splitterRect = new Rect();
        private const float minHelpHeight = 50f;
        private const float minTopicHeight = 100f;
        private float helpHeight = 100f;
        private bool draggingSplitter;

        public override void Initialize()
        {
            // First time?

            if (position.height < 300)
            {
                position = new Rect(position.x, position.y, position.width, 600);
            }

            minSize = new Vector2(200, 300);

            // Refresh highlights created in repaint
            FsmEditor.RepaintAll();

            wantsMouseMove = true;

            guidedTour.OnSelectionChanged = RepaintAll;
        }

        private void RepaintAll()
        {
            Repaint();
            FsmEditor.RepaintAll();
        }

        private void InitStyles()
        {
            if (helpStyle == null)
            {
                helpStyle = new GUIStyle(EditorStyles.label)
                {
                    richText = true,
                    wordWrap = true
                };
            }
        }

        public GuidedTour.Topic AddWindow(Type window,
            string label,
            string tooltip = "",
            string helpText = "",
            string highlightID = "",
            string helpUrl = "")
        {
            return guidedTour.AddWindow(window, label, tooltip, helpText, highlightID, helpUrl);
        }

        public GuidedTour.Topic AddTopic(GuidedTour.Topic parent,
            string label,
            string tooltip = "",
            string helpText = "",
            string highlightID = "",
            string helpUrl = "")
        {
            return guidedTour.AddTopic(parent, label, tooltip, helpText, highlightID, helpUrl);
        }

        /// <summary>
        /// Render the guidedTour
        /// </summary>
        protected void DoGuidedTourGUI()
        {
            InitStyles();

            HandleKeyboardInput();
            DoSplitter();

            // Hints

            if (FsmEditorSettings.ShowHints)
            {
                GUILayout.Box("Click Topics to highlight UI and show more info.\nClick Help Button to open online help.", FsmEditorStyles.HintBox);
            }

            DoTopicsList();

            GUILayout.FlexibleSpace();

            DoBottomPanel();

            // mouse event not used by other UI 
            // so must have clicked on empty area
            if (Event.current.type == EventType.MouseDown && 
                Event.current.mousePosition.y < position.height - helpHeight)
            {
                ClearHighlight();
            }
        }

        private void DoTopicsList()
        {
            GUILayout.Space(5f);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            if (Event.current.type == EventType.Layout)
            {
                guidedTour.UpdateTopicTree();
            }

            var windows = guidedTour.GetWindows();
            foreach (var window in windows)
            {
                FsmEditorGUILayout.LightDivider();
                var topics = guidedTour.GetTopics(window);
                foreach (var topic in topics)
                {
                    DoTopicGUI(topic);
                }
            }

            FsmEditorGUILayout.LightDivider();

            EditorGUILayout.EndScrollView();

            DoAutoScroll();
        }

        // Help panel for selected topic
        private void DoBottomPanel()
        {
            GUILayout.BeginVertical(FsmEditorStyles.BottomBarBG);

            FsmEditorGUILayout.Divider();

            GUILayout.BeginVertical(EditorStyles.inspectorDefaultMargins, GUILayout.Height(helpHeight));

            helpScrollPosition = EditorGUILayout.BeginScrollView(helpScrollPosition);

            if (guidedTour.SelectedTopic != null)
            {
                var topic = guidedTour.SelectedTopic;

                GUILayout.BeginHorizontal();
                GUILayout.Label(topic.Label.text, EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                if (!string.IsNullOrEmpty(topic.HelpUrl))
                {
                    if (FsmEditorGUILayout.HelpButton())
                    {
                        Application.OpenURL(topic.HelpUrl);
                    }
                }

                GUILayout.EndHorizontal();

                if (!string.IsNullOrEmpty(topic.Label.tooltip))
                {
                    EditorGUILayout.Space();
                    GUILayout.Label(topic.Label.tooltip, helpStyle);
                }

                EditorGUILayout.Space();
                topic.HelpText.DoGUILayout(position.width);
            }

            EditorGUILayout.EndScrollView();
            GUILayout.Space(5);
            GUILayout.EndVertical();

            GUILayout.EndVertical();
        }

        protected void HandleKeyboardInput()
        {
            switch (Event.current.keyCode)
            {
                case KeyCode.Escape:
                    ClearHighlight();
                    break;
            }
        }

        protected void DoTopicGUI(GuidedTour.Topic topic)
        {
            if (!guidedTour.IsVisible(topic))
            {
                return;
            }

            // setup selected styles
            var isSelected = guidedTour.IsHighlighted(topic);
            var rowStyle = isSelected ? FsmEditorStyles.ActionItemSelected : FsmEditorStyles.ActionItem;
            var labelStyle = isSelected ? FsmEditorStyles.ActionLabelSelected : FsmEditorStyles.ActionLabel;

            GUILayout.BeginHorizontal(rowStyle);

            GUILayout.Space(topic.Indent * 10f);

            if (!string.IsNullOrEmpty(topic.HelpUrl))
            {
                if (FsmEditorGUILayout.HelpButton())
                {
                    Application.OpenURL(topic.HelpUrl);
                }
            }
            else
            {
                GUILayout.Space(18f);
            }

            if (GUILayout.Button(topic.Label, labelStyle))
            {
                autoScroll = true;
                guidedTour.HighlightTopic(topic);
                FsmEditor.RepaintAll();
                GUIUtility.ExitGUI();
            }

            GUILayout.Space(20); // reserve space for scrollbar
            GUILayout.EndHorizontal();

            // Store selectedRect for auto scroll

            if (isSelected && Event.current.type == EventType.Repaint)
            {
                selectedRect = GUILayoutUtility.GetLastRect();
                selectedRect.y -= scrollPosition.y + 20;
                selectedRect.height += 20; // pad to scroll a little further
            }
        }

        private void DoAutoScroll()
        {
            // Auto scroll the main panel to show the selectedRect
            // Should be called right after GUILayout.EndScrollView();
            // Set autoScroll = true

            if (Event.current.type == EventType.Repaint)
            {
                //Debug.Log("AutoScroll");

                scrollViewHeight = GUILayoutUtility.GetLastRect().height;

                if (autoScroll)
                {
                    if (selectedRect.y < 0)
                    {
                        scrollPosition.y += selectedRect.y;
                        Repaint();
                    }
                    else if ((selectedRect.y + selectedRect.height) > scrollViewHeight)
                    {
                        scrollPosition.y += (selectedRect.y + selectedRect.height) - scrollViewHeight;
                        Repaint();
                    }
                    autoScroll = false;
                }
            }
        }

        private void DoSplitter()
        {
            if (draggingSplitter)
            {
                // set to size of window so it stays selected no matter how fast you drag
                splitterRect.Set(0, 0, position.width, position.height);
            }
            else
            {
                splitterRect.Set(0, position.height - helpHeight - 4, position.width, 8);
            }

            EditorGUIUtility.AddCursorRect(splitterRect, MouseCursor.ResizeVertical);

            if (splitterRect.Contains(currentEvent.mousePosition) && eventType == EventType.MouseDown)
            {
                draggingSplitter = true;
                currentEvent.Use();
            }

            if (draggingSplitter)
            {
                if (eventType == EventType.MouseDrag)
                {
                    helpHeight -= currentEvent.delta.y;
                    helpHeight = Mathf.Clamp(helpHeight, minHelpHeight, position.height - minTopicHeight);
                    currentEvent.Use();
                }

                if (currentEvent.rawType == EventType.MouseUp)
                {
                    draggingSplitter = false;
                }

                Repaint();
            }
        }

        public void ClearHighlight()
        {
            guidedTour.ClearHighlight();
            Repaint();
        }

        /// <summary>
        /// Shorter command for convenience (used a lot)
        /// </summary>
        protected string GetUrl(WikiPages page)
        {
            return DocHelpers.GetWikiPageUrl(page);
        }

        protected virtual void OnDisable()
        {
            ClearHighlight();
        }

    }

}


