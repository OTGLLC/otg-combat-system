
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditorInternal;

namespace OTG.CombatSystem.Editor
{
    public class OTGStateTransitionListView : VisualElement
    {
        private SerializedObject m_obj;
        private SerializedProperty m_items;
        private string m_listName;

        private ReorderableList m_reorderableList;
        private IMGUIContainer m_container;

        public OTGStateTransitionListView()
        {
            m_obj = null;
            m_items = null;
        }
        public OTGStateTransitionListView(SerializedObject _owner, SerializedProperty _list, string _listName)
        {
            m_obj = _owner;
            m_items = _list;
            m_listName = _listName;

            m_container = new IMGUIContainer(() => OnGUIHandler()) { name = "TransitionListContainer" };
            Add(m_container);
        }

        private void OnGUIHandler()
        {
            if (m_reorderableList == null)
            {
                CreateReorderableList();
                AddListCallBacks();
            }
            m_reorderableList.DoLayoutList();
        }
        private void CreateReorderableList()
        {
            m_reorderableList = new ReorderableList(m_obj, m_items, true, true, true, true);
        }
        private void AddListCallBacks()
        {
            m_reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                var labelRect = new Rect(rect.x, rect.y, rect.width - 10, rect.height);
                EditorGUI.LabelField(labelRect, m_listName);
            };
            m_reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                EditorGUI.BeginChangeCheck();

                DrawTransitionSubView(rect, index, isActive, isFocused);

                if (EditorGUI.EndChangeCheck())
                {
                    m_obj.ApplyModifiedProperties();
                }

            };
            m_reorderableList.elementHeightCallback = (int index) =>
            {
                float proertyHeight = EditorGUI.GetPropertyHeight(m_reorderableList.serializedProperty.GetArrayElementAtIndex(index), true);

                float spacing = EditorGUIUtility.singleLineHeight / 2;

                return proertyHeight + spacing;
            };
            m_reorderableList.onChangedCallback = (ReorderableList _reorderList) =>
            {
                m_obj.ApplyModifiedProperties();
            };
        }
        private void DrawTransitionSubView(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = m_reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty nextStateProp = element.FindPropertyRelative("m_nextState");

            EditorGUI.PropertyField(new Rect(rect.x += 10, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, new GUIContent("Transition"), true);
        }
    }

}
