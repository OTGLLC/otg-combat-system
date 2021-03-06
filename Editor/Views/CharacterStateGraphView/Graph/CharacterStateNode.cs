using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;


namespace OTG.CombatSystem.Editor
{
    public class CharacterStateNode : Node
    {
        #region SubDisplays
        private SerializedObject m_ownerState;
        private ActionsSubDisplay m_actionsSubDisplay;
        private TransitionsSubDisplay m_transitionsSubDisplay;
        private AnimationsSubDisplay m_animationSubDisplay;
        #endregion

        #region Elements
        private Button m_detailsPanelButton;
        private Button m_actionsExpansionButton;
        private Button m_transitionsExpansionButton;
        private Button m_animationsExpansionButton;
        private Button m_refreshPortsButton;

        private VisualElement m_detailsHolder;
        private VisualElement m_actionsHolder;
        private VisualElement m_transitionsHolder;
        private VisualElement m_animationsHolder;

        private VisualElement m_detailsDisplay;
        private VisualElement m_actionsDisplayArea;
        private VisualElement m_transitionsDisplayArea;
        private VisualElement m_animationsDisplayArea;
        
        #endregion

        #region Fields
        private bool m_detailsExpanded;
        private bool m_actionsExpanded;
        private bool m_transitionsExpanded;
        private bool m_animationsExpanded;
        #endregion

        #region Properties
        public Port InputPort { get; private set; }
        public List<Port> OutputPorts { get; private set; }
        #endregion

        #region Public API
        public CharacterStateNode(SerializedObject _state)
        {
            m_ownerState = _state;
            title = _state.targetObject.name;
            InitializeStyleSheet();
            CreateInputPort();
            CreateRefreshPortButton();

            InitializeSubDisplay();

            m_detailsExpanded = true;
            m_actionsExpanded = true;
            m_transitionsExpanded = true;
            m_animationsExpanded = true;

            InitializeOutputPorts();
            CreateOutputPorts();


            OnDetailsExpandClick();
            OnActionsExpandClicked();
            OnTransitionsExpandClick();
            OnAnimationExpandClick();

        }
        public void Cleanup()
        {
            CleanupRefreshPortsButton();
            CleanupActionsHolder();
            CleanupTransitionsHolder();
            CleanupSubdisplay();
        }
        #endregion

        #region Utility
        private void InitializeStyleSheet()
        {
           
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Submodules/otg-combat-system/Editor/Views/CharacterStateGraphView/Graph/CharacterStateNodeTemplate.uxml");
            visualTree.CloneTree(mainContainer);
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Submodules/otg-combat-system/Editor/Views/CharacterStateGraphView/Graph/CharacterStateNodeStyle.uss"));
        }
        private void CreateInputPort()
        {
            InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(OTGCombatState));
            InputPort.portName = "Input";

            this.inputContainer.Add(InputPort);
            RefreshExpandedState();
            RefreshPorts();
        }
        private void CreateRefreshPortButton()
        {
            m_refreshPortsButton = new Button();
            m_refreshPortsButton.text = "Refresh Ports";
            m_refreshPortsButton.clickable.clicked += OnRefreshPorts;

            titleContainer.Add(m_refreshPortsButton);

            
        }
        private void CleanupRefreshPortsButton()
        {
            m_refreshPortsButton.clickable.clicked -= OnRefreshPorts;
            titleContainer.Remove(m_refreshPortsButton);
            m_refreshPortsButton = null;
        }
        private void InitializeOutputPorts()
        {
            OutputPorts = new List<Port>();
        }
        private void InitializeSubDisplay()
        {
            m_detailsHolder = mainContainer.Q<VisualElement>("details-panel");
            m_detailsPanelButton = mainContainer.Q<Button>("details-panel-expand");
            m_detailsPanelButton.clickable.clicked += OnDetailsExpandClick;
            m_detailsDisplay = mainContainer.Q<VisualElement>("details-panel-holder");

            InitializeActionsHolder();

            InitializeTransitionsHolder();

            InitializeAnimationHolder();

        }
        private void CleanupSubdisplay()
        {
            m_detailsHolder = null;
            m_detailsPanelButton.clickable.clicked -= OnDetailsExpandClick;
            m_detailsPanelButton = null;
            m_detailsDisplay = null;

            CleanupActionsHolder();

            CleanupTransitionsHolder();

            CleanupAnimationsHolder();
        }
        private void InitializeActionsHolder()
        {
            m_actionsHolder = mainContainer.Q<VisualElement>("actions-holder");

            m_actionsExpansionButton = mainContainer.Q<Button>("actions-expand-button");
            m_actionsExpansionButton.clickable.clicked += OnActionsExpandClicked;

            m_actionsDisplayArea = mainContainer.Q<VisualElement>("actions-display-area");
            m_actionsSubDisplay = new ActionsSubDisplay(m_actionsDisplayArea, m_ownerState);
        }
        private void CleanupActionsHolder()
        {
            m_actionsHolder = null;
            m_actionsExpansionButton.clickable.clicked -= OnActionsExpandClicked;
            m_actionsExpansionButton = null;
            m_actionsDisplayArea = null;
        }
        private void InitializeTransitionsHolder()
        {
            m_transitionsHolder = mainContainer.Q<VisualElement>("transitions-holder");

            m_transitionsExpansionButton = mainContainer.Q<Button>("transitions-expand-button");
            m_transitionsExpansionButton.clickable.clicked += OnTransitionsExpandClick;

            m_transitionsDisplayArea = mainContainer.Q<VisualElement>("transitions-display-area");
            m_transitionsSubDisplay = new TransitionsSubDisplay(m_transitionsDisplayArea, m_ownerState);
        }
        private void CleanupTransitionsHolder()
        {
            m_transitionsHolder = null;
            m_transitionsExpansionButton.clickable.clicked -= OnTransitionsExpandClick;
            m_transitionsExpansionButton = null;
            m_transitionsDisplayArea = null;
        }
        private void InitializeAnimationHolder()
        {
            m_animationsHolder = mainContainer.Q<VisualElement>("animations-holder");

            m_animationsExpansionButton = mainContainer.Q<Button>("animations-expand-button");
            m_animationsExpansionButton.clickable.clicked += OnAnimationExpandClick;

            m_animationsDisplayArea = mainContainer.Q<VisualElement>("animations-display-area");
            m_animationSubDisplay = new AnimationsSubDisplay(m_animationsDisplayArea, m_ownerState);
        }
        private void CleanupAnimationsHolder()
        {
            m_animationsHolder = null;
            m_animationsExpansionButton.clickable.clicked -= OnAnimationExpandClick;
            m_animationsExpansionButton = null;
            m_animationsDisplayArea = null;
        }
        private void ToggleDisplay(ref VisualElement _targetDisplay,ref VisualElement _displayContents,ref bool _expandedState)
        {
            if(_expandedState)
            {
                _targetDisplay.Remove(_displayContents);
                
            }
            else if(!_expandedState)
            {
                _targetDisplay.Add(_displayContents);
            }
            _expandedState = !_expandedState;
        }
        private void CreateOutputPorts()
        {
            int transitionCount = m_ownerState.FindProperty("m_stateTransitions").arraySize;
            for(int i = 0; i < transitionCount; i++)
            {
                SerializedProperty prop = m_ownerState.FindProperty("m_stateTransitions").GetArrayElementAtIndex(i);
                OutputPorts.Add(CreateOutputPort(prop));
            }

        }
        private Port CreateOutputPort(SerializedProperty _prop)
        {
            Port p = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(OTGCombatState));
            Object nextState = _prop.FindPropertyRelative("m_nextState").objectReferenceValue;

            if (nextState == null)
                return p;

            p.portName = GetOutputPortName(nextState.name);
            this.outputContainer.Add(p);
            RefreshExpandedState();
            RefreshPorts();
           
            return p;
        }
       private string GetOutputPortName(string _longName)
        {
            string[] parts = _longName.Split('_');
            string middle = parts[1];
            return middle;
        }
        #endregion

        #region Callbacks
        private void OnDetailsExpandClick()
        {
            ToggleDisplay(ref m_detailsHolder, ref m_detailsDisplay, ref m_detailsExpanded);
            if (m_detailsExpanded)
                m_detailsPanelButton.text = "-";
            else
                m_detailsPanelButton.text = "<";
        }
        private void OnActionsExpandClicked()
        {
            ToggleDisplay(ref m_actionsHolder, ref m_actionsDisplayArea, ref m_actionsExpanded);
            if (m_actionsExpanded)
                m_actionsExpansionButton.text = "Actions _";
            else
                m_actionsExpansionButton.text = "Actions <";
        }
        private void OnTransitionsExpandClick()
        {
            ToggleDisplay(ref m_transitionsHolder, ref m_transitionsDisplayArea, ref m_transitionsExpanded);
            if (m_transitionsExpanded)
                m_transitionsExpansionButton.text = "Transitions _";
            else
                m_transitionsExpansionButton.text = "Transitions <";
        }
        private void OnAnimationExpandClick()
        {
            ToggleDisplay(ref m_animationsHolder, ref m_animationsDisplayArea, ref m_animationsExpanded);
            if (m_animationsExpanded)
                m_animationsExpansionButton.text = "Animation _";
            else
                m_animationsExpansionButton.text = "Animation <";
        }
        private void OnRefreshPorts()
        {
            outputContainer.Clear();
            OutputPorts.Clear();
            CreateOutputPorts();
        }
        #endregion
    }

    public class ActionsSubDisplay : CharacterStateNodeSubDisplay
    {
        public ActionsSubDisplay(VisualElement _ownerElement, SerializedObject _ownerState) : base(_ownerElement,_ownerState)
        {
            PopulateReorderableList(m_owner.FindProperty("m_onEnterActions"), "On Enter Actions", "OnEnterActions");
            PopulateReorderableList(m_owner.FindProperty("m_onUpdateActions"), "On Update Actions","OnUpdateActions");
            PopulateReorderableList(m_owner.FindProperty("m_animUpdateActions"), "On Animator Move Actions", "OnAnimatorMoveActions");
            PopulateReorderableList(m_owner.FindProperty("m_onExitActions"), "On Exit Actions", "OnExitActions");
        }
        
    }
    public class TransitionsSubDisplay:CharacterStateNodeSubDisplay
    {
        public TransitionsSubDisplay(VisualElement _ownerElement, SerializedObject _ownerState) : base(_ownerElement, _ownerState)
        {
            PopulateTransitionList(m_owner.FindProperty("m_stateTransitions"), "Transitions", "transitions");
        }
    }
    public class AnimationsSubDisplay:CharacterStateNodeSubDisplay
    {
        private VisualElement m_animationDataHolder;

        private VisualElement m_animationDataDisplayArea;
        private VisualElement m_animPropertyField;


        private SerializedProperty m_animationDataProp;
        private SerializedObject m_animationClipDataProp;


        private Button m_dataExpansionButton;

        private bool m_dataExpanded;

        public AnimationsSubDisplay(VisualElement _ownerElement, SerializedObject _ownerState) : base(_ownerElement, _ownerState)
        {

            m_dataExpanded = true;

            InitializeExpansionButtons(_ownerElement);
            InitializeDataHolders(_ownerElement);

            InitializeAnimationClipArea(_ownerElement, _ownerState);
            InitializeAnimationDataProp(_ownerState);
        }

        public override void CleanupSubDisplay()
        {
            Cleanup();
            base.CleanupSubDisplay();
        }
        private void InitializeAnimationClipArea(VisualElement _ownerElement,SerializedObject _ownerState)
        {
            m_animPropertyField = _ownerElement.Q<VisualElement>("animation-property-field");
            m_animationDataProp = _ownerState.FindProperty("m_combatAnim").FindPropertyRelative("m_animClip");

            PropertyField field = new PropertyField(m_animationDataProp);
            field.Bind(_ownerState);

            m_animPropertyField.Add(field);
        }
        private void InitializeAnimationDataProp(SerializedObject _ownerState)
        {
            m_animationDataProp = _ownerState.FindProperty("m_combatAnim").FindPropertyRelative("m_animData");
            PropertyField field = new PropertyField(m_animationDataProp);
            field.Bind(_ownerState);

            m_animationDataDisplayArea.Add(field);
        }
        private void InitializeExpansionButtons(VisualElement _ownerElement)
        {
            m_dataExpansionButton= _ownerElement.Q<Button>("animation-data-expand-button");
            m_dataExpansionButton.clickable.clicked += OnDataExpansionButtonClicked;
        }
        private void InitializeDataHolders(VisualElement _ownerElement)
        {
            m_animationDataHolder = _ownerElement.Q<VisualElement>("animation-data-display-holder");
            m_animationDataDisplayArea = _ownerElement.Q<VisualElement>("animation-data-display-area");
        }
        private void Cleanup()
        {
            m_dataExpansionButton.clickable.clicked -= OnDataExpansionButtonClicked;
            m_dataExpansionButton = null;

            m_animationDataProp = null;
        }

        #region Callbacks
        private void OnDataExpansionButtonClicked()
        {
            ToggleDisplay(ref m_animationDataHolder, ref m_animationDataDisplayArea, ref m_dataExpanded);
        }
        #endregion
    }
}
