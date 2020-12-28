using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace OTG.CombatSystem.Editor
{
    public class NewCharacterBeginningView : SubView
    {
        #region Fields
        private CharacterWorkArea m_characterWorkArea;
        private string m_selectedCharacter;
        #endregion

        #region Elements
        private ToolbarButton m_charInProjectBtn;
        private ToolbarButton m_charInSceneBtn;
        private ToolbarButton m_delectCharBtn;
        private ListView m_characterListView;
        
        #endregion


        #region base class Implementation
        public NewCharacterBeginningView(string _complettionButton) : base(_complettionButton) { }
        protected override void SetStrings()
        {
            m_templatePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/SubViews/NewCharacterBeginningView/NewCharacterBeginningViewTemplate.uxml";
            m_stylePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/SubViews/NewCharacterBeginningView/NewCharacterBeginningViewStyle.uss";
            ContainerStyleName = "new-character-beginning-subview";
        }
        protected override void HandleViewFocused() 
        {
            m_selectedCharacter = string.Empty;
            InitializeWorkArea();
            InitializeButtons();
            InitializeListView();

            PopulateListViewWithProjectCharacters();
        }
        protected override void HandlerViewLostFocus()
        {
            m_selectedCharacter = string.Empty;
            CleanupButtons();
            CleanupListView();
            CleanupWorkArea();
        }
        protected override bool PerformCompletedEventActions()
        {

            return true;
           
        }
        #endregion

        #region Utility
        private void InitializeButtons()
        {
            m_charInProjectBtn = ContainerElement.Q<ToolbarButton>("project-list-button");
            m_charInProjectBtn.clickable.clicked += OnProjectListClicked;

            m_charInSceneBtn = ContainerElement.Q<ToolbarButton>("scene-list-button");
            m_charInSceneBtn.clickable.clicked += OnSceneListClicked;

            m_delectCharBtn = ContainerElement.Q<ToolbarButton>("delete-character-button");
            m_delectCharBtn.clickable.clicked += OnDeleteCharacterClicked;
        }
        private void CleanupButtons()
        {
            m_charInProjectBtn.clickable.clicked -= OnProjectListClicked;
            m_charInSceneBtn.clickable.clicked -= OnSceneListClicked;
            m_delectCharBtn.clickable.clicked -= OnDeleteCharacterClicked;

            m_charInProjectBtn = null;
            m_charInSceneBtn = null;
            m_delectCharBtn = null;
        }
        private void InitializeListView()
        {
            m_characterListView = ContainerElement.Q<ListView>("character-list-view");
            m_characterListView.onSelectionChange += OnCharacterListViewSelectionChanged;
        }
        private void CleanupListView()
        {
            m_characterListView.onSelectionChange -= OnCharacterListViewSelectionChanged;
            m_characterListView = null;
        }
        private void InitializeWorkArea()
        {
            m_characterWorkArea = new CharacterWorkArea(
                ContainerElement.Q<VisualElement>("character-data-area")
                );

        }
        private void CleanupWorkArea()
        {
            m_characterWorkArea.Cleanup();
            m_characterWorkArea = null;
        }
        private void PopulateListViewWithProjectCharacters()
        {
            CharacterLibrary lib = OTGCombatEditorUtilis.GetCharacterLibrary();
            
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_characterListView, ref m_containerElement, lib.CharactersInProject, "character-list-view");
        }
        private void PopulateListViewWithSceneCharacters()
        {
            OTGCombatSMC[] characters = GameObject.FindObjectsOfType<OTGCombatSMC>();
            List<string> names = new List<string>();
            for(int i = 0; i < characters.Length; i++)
            {
                names.Add(characters[i].gameObject.name);
            }

            OTGCombatEditorUtilis.PopulateListView<string>(ref m_characterListView, ref m_containerElement, names, "character-list-view");
        }
        #endregion

        #region Callbacks
        private void OnProjectListClicked()
        {
            PopulateListViewWithProjectCharacters();
        }
        private void OnSceneListClicked()
        {
            PopulateListViewWithSceneCharacters();
        }
        private void OnDeleteCharacterClicked()
        {
            OTGCombatEditorUtilis.DeleteCharacter(m_selectedCharacter);
        }
        private void OnCharacterListViewSelectionChanged(IEnumerable<object> _obj)
        {
            foreach (string candidate in _obj)
            {

                if (Event.current.type == EventType.MouseDown)
                {
                    m_selectedCharacter = candidate;

                    WorkAreaData data = new WorkAreaData()
                    {
                        CharacterName = candidate

                    };

                    m_characterWorkArea.UpdateWorkArea(data);
                    //m_draggedItems[0] = candidate;
                }
                if (Event.current.type == EventType.MouseUp)
                {
                    //m_draggedItems[0] = null;
                }
            }
        }
        #endregion

        #region Classes
        private class CharacterWorkArea
        {
            public VisualElement Element { get; private set; }

            private Label m_characterNameLabel;

            #region Public API
            public CharacterWorkArea(VisualElement _owner)
            {
                Element = _owner;

                InitializeElements();
            }
            public void Cleanup()
            {
                CleanupElements();
                Element = null;
            }
            public void UpdateWorkArea(WorkAreaData _data)
            {
                m_characterNameLabel.text = _data.CharacterName;
            }

            #endregion

            #region Utility
            private void InitializeElements()
            {
                m_characterNameLabel = Element.Q<Label>("character-name");
            }
            private void CleanupElements()
            {
                m_characterNameLabel = null;
            }

            #endregion
        }
        private struct WorkAreaData
        {
            public string CharacterName;
        }
        #endregion
    }



}

