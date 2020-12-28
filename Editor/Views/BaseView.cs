
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEngine;

namespace OTG.CombatSystem.Editor
{
    public abstract class BaseView : VisualElement
    {
        #region Properties
        public VisualElement ContainerElement { get { return m_containerElement; } }
        public string ContainerStyleName { get; protected set; }
        #endregion

        #region Fields
        protected string m_templatePath;
        protected string m_stylePath;
        protected VisualElement m_containerElement;
        protected SubView m_currentSubView;
        protected Object[] m_draggedItems = new Object[1];
        protected bool m_GotMouseDown;
        #endregion


        #region Public API
        public BaseView()
        {
            SetStrings();

            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(m_templatePath);
            StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>(m_stylePath);

            m_containerElement = new VisualElement();
            visualTree.CloneTree(m_containerElement);
            m_containerElement.styleSheets.Add(style);

        }
        public virtual void UpdateViewHeight(float _height)
        {
            m_containerElement.Q<VisualElement>(ContainerStyleName).style.height = new StyleLength(_height);
        }
        public void OnProjectChanged()
        {
            HandleProjectChanged();
        }
        public void OnViewLostFocus()
        {
            HandlerViewLostFocus();
        }
        public void OnViewFocused()
        {
            RemoveCurrentSubView();
            HandleViewFocused();
        }
        protected virtual void HandleProjectChanged()
        {

        }
        protected void SwitchSubViews(SubView _newView)
        {
            if (m_currentSubView != null)
            {
                m_currentSubView.OnViewLostFocus();
                m_currentSubView.CompletedEvent -= HandleSubviewCompletedEvent;
                ContainerElement.Q<VisualElement>(ContainerStyleName).Remove(m_currentSubView.ContainerElement);
            }

            m_currentSubView = _newView;

            ContainerElement.Q<VisualElement>(ContainerStyleName).Add(m_currentSubView.ContainerElement);
            m_currentSubView.OnViewFocused();
            m_currentSubView.CompletedEvent += HandleSubviewCompletedEvent;
        }
         private void RemoveCurrentSubView()
        {
            if(m_currentSubView != null)
            {
                m_currentSubView.OnViewLostFocus();
                m_currentSubView.CompletedEvent -= HandleSubviewCompletedEvent;
                ContainerElement.Q<VisualElement>(ContainerStyleName).Remove(m_currentSubView.ContainerElement);

                m_currentSubView = null;
            }
        }
        protected virtual void HandleSubviewCompletedEvent()
        {

        }
        protected void AddCallbacksToListView(ref ListView _targetList)
        {
            if (_targetList == null)
                return;

            _targetList.RegisterCallback<MouseDownEvent>(OnMouseDownEvent);
            _targetList.RegisterCallback<MouseMoveEvent>(OnMouseMoveEvent);
            _targetList.RegisterCallback<MouseUpEvent>(OnMouseUpEvent);
        }
        protected void RemoveCallbacksFromListView(ref ListView _targetList)
        {
            if (_targetList == null)
                return;

            _targetList.UnregisterCallback<MouseDownEvent>(OnMouseDownEvent);
            _targetList.UnregisterCallback<MouseMoveEvent>(OnMouseMoveEvent);
            _targetList.UnregisterCallback<MouseUpEvent>(OnMouseUpEvent);
        }
        protected void OnMouseDownEvent(MouseDownEvent e)
        {
            m_GotMouseDown = true;
        }
        protected void OnMouseMoveEvent(MouseMoveEvent e)
        {
            if (m_GotMouseDown && e.pressedButtons == 1)
            {

                DragAndDrop.PrepareStartDrag();
                DragAndDrop.objectReferences = m_draggedItems;
                DragAndDrop.StartDrag("ActionDrag");
            }
        }
        protected void OnMouseUpEvent(MouseUpEvent e)
        {
            m_GotMouseDown = false;
        }
        #endregion



        #region abstract methods
        protected abstract void SetStrings();
        protected abstract void HandleViewFocused();
        protected abstract void HandlerViewLostFocus();
        #endregion
    }

}
