
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace OTG.CombatSystem.Editor
{
    public class CharacterStateGraphView : BaseView
    {

        #region Fields
        private CharacterLibrary m_characterLibrary;
        private SelectedCharacterData m_selectedCharacterData;
        private CharacterStateGraph m_graph;
        private string m_newStateName;
        #endregion

        #region Buttons
        private Button m_displayCharsInProjectButton;
        private Button m_displayCharsInSceneButton;
        private Button m_deleteCharacterButton;
        private Button m_newCharacterButton;
        private ToolbarButton m_newStateButton;
        #endregion

        #region List Views
        private ListView m_characterListView;
        private VisualElement m_graphArea;

        private ListView m_availableActions;
        private ListView m_availableTransitions;
        private ListView m_availableAnimations;
        private ListView m_availableStates;
        #endregion

        #region TextFields
        private TextField m_newStateNameTextField;
        private TextField m_filterActionsTextField;
        private TextField m_filterAnimationsTextField;
        private TextField m_filterTransitionsTextField;
        private TextField m_filterStatesTextField;
        #endregion

        #region Fields
        private List<string> m_availableAnimationsPaths;
        private List<string> m_availableActionsPaths;
        private List<string> m_availableTransitionsPaths;
        private List<string> m_availableStatesPaths;
        #endregion

        #region base class Implementation

        protected override void SetStrings()
        {
            m_templatePath = "Assets/Submodules/otg-combat-system/Editor/Views/CharacterStateGraphView/CharacterStateGraphViewTemplate.uxml";
            m_stylePath = "Assets/Submodules/otg-combat-system/Editor/Views/CharacterStateGraphView/CharacterStateGraphViewStyle.uss";
            ContainerStyleName = "character-stategraph-view";
        }
        protected override void HandleViewFocused()
        {
            GetCharacterLibrary();
            InitializeAllButtons();
            InitializeListViews();
            InitializeTextFields();
            PopulateListViewWithProjectCharacters();

            CreateSelectedCharacterData();
            CreateStateGraph();

            PopulateListViews();
        }
        protected override void HandleProjectChanged()
        {
            PopulateListViews();
        }
        protected override void HandlerViewLostFocus()
        {
            CleanupStateGraph();
            CleanupSelectedCharacterData();
            CleanupCharacterLibrary();
            CleanupAllButtons();
            CleanupListViews();
            CleanupTextFields();
        }
        #endregion

        #region Utilities
        private void CreateStateGraph()
        {
            m_graphArea = ContainerElement.Q<VisualElement>("graph-area");
            m_graph = new CharacterStateGraph();

            m_graphArea.Add(m_graph);
            m_graph.OnViewFocused();
        }
        private void CleanupStateGraph()
        {
            m_graph.OnViewLostFocus();
            m_graph.Cleanup();
            m_graph = null;
            m_graphArea.Clear();
            m_graphArea = null;
        }
        private void CreateSelectedCharacterData()
        {
            m_selectedCharacterData = new SelectedCharacterData();
        }
        private void CleanupSelectedCharacterData()
        {
            m_selectedCharacterData.Cleanup();
            m_selectedCharacterData = null;
        }
        private void GetCharacterLibrary()
        {
            m_characterLibrary = OTGCombatEditorUtilis.GetCharacterLibrary();
        }
        private void CleanupCharacterLibrary()
        {
            m_characterLibrary = null;
        }
        private void InitializeAllButtons()
        {
            m_displayCharsInProjectButton = ContainerElement.Q<Button>("project-list-button");
            m_displayCharsInProjectButton.clickable.clicked += OnProjectButtonClicked;

            m_displayCharsInSceneButton = ContainerElement.Q<Button>("scene-list-button");
            m_displayCharsInSceneButton.clickable.clicked += OnSceneButtonClicked;

            m_deleteCharacterButton = ContainerElement.Q<Button>("delete-character-button");
            m_deleteCharacterButton.clickable.clicked += OnDeleteCharacterButtonClicked;

            m_newCharacterButton = ContainerElement.Q<Button>("new-character-button");
            m_newCharacterButton.clickable.clicked += OnNewCharacterButtonClicked;

            m_newStateButton = ContainerElement.Q<ToolbarButton>("new-state-button");
            m_newStateButton.clickable.clicked += OnNewStateClicked;
        }
        private void CleanupAllButtons()
        {
            m_displayCharsInProjectButton.clickable.clicked -= OnProjectButtonClicked;
            m_displayCharsInProjectButton = null;

            m_displayCharsInSceneButton.clickable.clicked -= OnSceneButtonClicked;
            m_displayCharsInSceneButton = null;

            m_deleteCharacterButton.clickable.clicked -= OnDeleteCharacterButtonClicked;
            m_deleteCharacterButton = null;

            m_newCharacterButton.clickable.clicked -= OnNewCharacterButtonClicked;
            m_newCharacterButton = null;

            m_newStateButton.clickable.clicked -= OnNewStateClicked;
            m_newStateButton = null;
        }
        
        private void InitializeListViews()
        {
            m_characterListView = ContainerElement.Q<ListView>("character-list-view");
            m_characterListView.onSelectionChange += OnCharacterListViewSelectionChanged;

            m_availableAnimations = ContainerElement.Q<ListView>("animations-list-view");
            m_availableAnimations.onSelectionChange += OnAvailableAnimationListViewSelectionChanged;
            AddCallbacksToListView(ref m_availableAnimations);


            m_availableTransitions = ContainerElement.Q<ListView>("transitions-list-view");
            m_availableTransitions.onSelectionChange += OnAvailableTransitionListViewSelectionChanged;
            AddCallbacksToListView(ref m_availableTransitions);

            m_availableActions = ContainerElement.Q<ListView>("actions-list-view");
            m_availableActions.onSelectionChange += OnAvailableActionsListViewSelectionChanged;
            AddCallbacksToListView(ref m_availableActions);

            m_availableStates = ContainerElement.Q<ListView>("states-list-view");
            m_availableStates.onSelectionChange += OnAvailableStatesListViewSelectionChanged;
            AddCallbacksToListView(ref m_availableStates);
        }
        private void CleanupListViews()
        {
            
            m_characterListView.onSelectionChange -= OnCharacterListViewSelectionChanged;
            m_characterListView = null;

            RemoveCallbacksFromListView(ref m_availableAnimations);
            m_availableAnimations.onSelectionChange -= OnAvailableAnimationListViewSelectionChanged;
            m_availableAnimations = null;

            RemoveCallbacksFromListView(ref m_availableTransitions);
            m_availableTransitions.onSelectionChange -= OnAvailableTransitionListViewSelectionChanged;
            m_availableTransitions = null;

            RemoveCallbacksFromListView(ref m_availableActions);
            m_availableActions.onSelectionChange -= OnAvailableActionsListViewSelectionChanged;
            m_availableActions = null;

            RemoveCallbacksFromListView(ref m_availableStates);
            m_availableStates.onSelectionChange -= OnAvailableStatesListViewSelectionChanged;
            m_availableStates = null;
        }
        private void InitializeTextFields()
        {

            m_newStateNameTextField = ContainerElement.Q<TextField>("state-name-textfield");
            m_newStateNameTextField.RegisterValueChangedCallback(OnNewStateTextFieldChanged);

            m_filterActionsTextField = ContainerElement.Q<TextField>("actions-filter");
            m_filterActionsTextField.RegisterValueChangedCallback(OnActionFilterTextFieldChanged);

            m_filterTransitionsTextField = ContainerElement.Q<TextField>("transitions-filter");
            m_filterTransitionsTextField.RegisterValueChangedCallback(OnTransitionFilterTextFieldChanged);

            m_filterAnimationsTextField = ContainerElement.Q<TextField>("animations-filter");
            m_filterAnimationsTextField.RegisterValueChangedCallback(OnAnimationFilterTextFieldChanged);

        }
        private void CleanupTextFields()
        {

            m_newStateNameTextField.RegisterValueChangedCallback(OnNewStateTextFieldChanged);
            m_newStateNameTextField = null;

            m_filterActionsTextField.UnregisterValueChangedCallback(OnActionFilterTextFieldChanged);
            m_filterActionsTextField = null;

            m_filterTransitionsTextField.UnregisterValueChangedCallback(OnTransitionFilterTextFieldChanged);
            m_filterTransitionsTextField = null;

            m_filterAnimationsTextField.UnregisterValueChangedCallback(OnAnimationFilterTextFieldChanged);
            m_filterAnimationsTextField = null;
        }
        private void PopulateListViewWithProjectCharacters()
        {
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_characterListView, ref m_containerElement, m_characterLibrary.CharactersInProject, "character-list-view");
        }
        private void PopulateListViewWithSceneCharacters()
        {

        }
        private void PopulateListViews()
        {
            m_availableActionsPaths = OTGCombatEditorUtilis.GetAllActionsLocations();
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_availableActions, ref m_containerElement, m_availableActionsPaths, "actions-list-view", true);

            m_availableTransitionsPaths = OTGCombatEditorUtilis.GetAllTransitionsLocations();
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_availableTransitions, ref m_containerElement, m_availableTransitionsPaths, "transitions-list-view", true);

            m_availableAnimationsPaths = OTGCombatEditorUtilis.GetAllAnimationsLocations();
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_availableAnimations, ref m_containerElement, m_availableAnimationsPaths, "animations-list-view", true);
        }
        private void PopulateAvailableStatesListView()
        {
            m_availableStatesPaths = OTGCombatEditorUtilis.GetAllStates(m_selectedCharacterData.SelectedCharacterSMC.name);
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_availableStates, ref m_containerElement, m_availableStatesPaths, "states-list-view", true);
        }
        private List<string> ApplyFilter(string _filter, List<string> _original)
        {
            var filtered = _original.Where(x => x.ToLower().Contains(_filter.ToLower())).ToList();

            return filtered;

        }
        #endregion

        #region Callbacks
        private void OnProjectButtonClicked()
        {
            Debug.Log("Project clicked");
        }
        private void OnSceneButtonClicked()
        {
            Debug.Log("Scene clicked");
        }
        private void OnNewCharacterButtonClicked()
        {
            Debug.Log("New clicked");
        }
        private void OnDeleteCharacterButtonClicked()
        {
            Debug.Log("Delete clicked");
        }
        private void OnNewStateClicked()
        {
            OTGCombatState state = OTGCombatEditorUtilis.CreateNewState(m_selectedCharacterData.SelectedCharacterSMC.name, m_newStateName);

            //SerializedObject obj = new SerializedObject(state);
            //m_graph.AddNode(obj);

            m_newStateName = string.Empty;
            m_newStateNameTextField.value = string.Empty;
        }
        private void OnCharacterListViewSelectionChanged(IEnumerable<object> _obj)
        {
            foreach (string candidate in _obj)
            {

                if (Event.current.type == EventType.MouseDown)
                {
                    CleanupStateGraph();
                    CreateStateGraph();
                    OTGCombatSMC target = OTGCombatEditorUtilis.GetCharacterFromScene(candidate);
                    m_selectedCharacterData.OnCharacterSelected(target);
                    m_graph.OnCharacterSelected(m_selectedCharacterData);
                    PopulateAvailableStatesListView();
                    //m_draggedItems[0] = candidate;
                }
                if (Event.current.type == EventType.MouseUp)
                {
                    //m_draggedItems[0] = null;
                }
            }
        }
        private void OnAvailableActionsListViewSelectionChanged(IEnumerable<object> _obj)
        {
            foreach (string path in _obj)
            {

                if (Event.current.type == EventType.MouseDown)
                {
                    OTGCombatAction action = OTGCombatEditorUtilis.GetCombatActionFromPath(path);
                    Selection.activeObject = action;
                    m_draggedItems[0] = action;
                }
                if (Event.current.type == EventType.MouseUp)
                {
                    m_draggedItems[0] = null;
                }
            }
        }
        private void OnAvailableTransitionListViewSelectionChanged(IEnumerable<object> _obj)
        {
            foreach (string path in _obj)
            {

                if (Event.current.type == EventType.MouseDown)
                {

                    OTGTransitionDecision transition = OTGCombatEditorUtilis.GetTransitionDecisionFromPath(path);
                    Selection.activeObject = transition;
                    m_draggedItems[0] = transition;
                }
                if (Event.current.type == EventType.MouseUp)
                {
                    m_draggedItems[0] = null;
                }
            }
        }
        private void OnAvailableAnimationListViewSelectionChanged(IEnumerable<object> _obj)
        {
            foreach (string path in _obj)
            {

                if (Event.current.type == EventType.MouseDown)
                {
                    AnimationClip clip = OTGCombatEditorUtilis.GetAnimationClipFromPath(path);
                    Selection.activeObject = clip;
                    m_draggedItems[0] = clip;
                }
                if (Event.current.type == EventType.MouseUp)
                {
                    m_draggedItems[0] = null;
                }
            }
        }
        private void OnAvailableStatesListViewSelectionChanged(IEnumerable<object> _obj)
        {
            foreach (string path in _obj)
            {

                if (Event.current.type == EventType.MouseDown)
                {
                    OTGCombatState state = OTGCombatEditorUtilis.GetCombatStateFromPath(path);
                    Selection.activeObject = state;
                    m_draggedItems[0] = state;
                }
                if (Event.current.type == EventType.MouseUp)
                {
                    m_draggedItems[0] = null;
                }
            }
        }
        private void OnNewStateTextFieldChanged(ChangeEvent<string> _ev)
        {
            m_newStateName = _ev.newValue;
        }
        private void OnActionFilterTextFieldChanged(ChangeEvent<string> _ev)
        {
            List<string> filteredActions = ApplyFilter(_ev.newValue, m_availableActionsPaths);
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_availableActions, ref m_containerElement, m_availableActionsPaths, "actions-list-view", true);

        }
        private void OnTransitionFilterTextFieldChanged(ChangeEvent<string> _ev)
        {
            List<string> filteredTransitions = ApplyFilter(_ev.newValue, m_availableTransitionsPaths);
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_availableTransitions, ref m_containerElement, filteredTransitions, "transitions-list-view", true);

        }
        private void OnAnimationFilterTextFieldChanged(ChangeEvent<string> _ev)
        {
            List<string> filteredAnimations = ApplyFilter(_ev.newValue, m_availableAnimationsPaths);
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_availableAnimations, ref m_containerElement, filteredAnimations, "animations-list-view", true);
        }
        private void OnStateFilterTextFieldChanged(ChangeEvent<string> _ev)
        {
            List<string> filteredstates = ApplyFilter(_ev.newValue, m_availableStatesPaths);
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_availableStates, ref m_containerElement, filteredstates, "states-list-view", true);
        }
        #endregion



    }

}
