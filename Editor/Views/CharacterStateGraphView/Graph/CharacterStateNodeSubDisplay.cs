
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace OTG.CombatSystem.Editor
{
    public abstract class CharacterStateNodeSubDisplay
    {
        protected VisualElement m_ownerElement;
        protected SerializedObject m_owner;

        public CharacterStateNodeSubDisplay(VisualElement _ownerElement, SerializedObject _targetState)
        {
            m_ownerElement = _ownerElement;
            m_owner = _targetState;
            
        }

        public virtual void CleanupSubDisplay()
        {
            m_owner = null;
            m_ownerElement = null;
        }

        protected void PopulateReorderableList(SerializedProperty _listItems, string _listName,string _targetElement)
        {
            OTGReorderableListViewElement roList = new OTGReorderableListViewElement(m_owner, _listItems, _listName);
            roList.Bind(m_owner);

            m_ownerElement.Q<VisualElement>(_targetElement).Add(roList);
        }
        protected void PopulateTransitionList(SerializedProperty _listItems, string _listName, string _targetElement)
        {
            OTGStateTransitionListView transList = new OTGStateTransitionListView(m_owner, _listItems, _listName);
            transList.Bind(m_owner);
            m_ownerElement.Q<VisualElement>(_targetElement).Add(transList);
        }
        protected void ToggleDisplay(ref VisualElement _targetDisplay, ref VisualElement _displayContents, ref bool _expandedState)
        {
            if (_expandedState)
            {
                _targetDisplay.Remove(_displayContents);

            }
            else if (!_expandedState)
            {
                _targetDisplay.Add(_displayContents);
            }
            _expandedState = !_expandedState;
        }
    }
}
