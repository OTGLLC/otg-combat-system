
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace OTG.CombatSystem.Editor
{
    public class CharacterStateGraphView : BaseView
    {

        #region Fields
        private CharacterLibrary m_characterLibrary;
        #endregion

        #region Buttons
        private Button m_displayCharsInProjectButton;
        private Button m_displayCharsInSceneButton;
        private Button m_deleteCharacterButton;
        private Button m_newCharacterButton;
        #endregion

        #region Other Elements
        private ListView m_characterListView;

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
            PopulateListViewWithProjectCharacters();
        }
        protected override void HandlerViewLostFocus()
        {
            CleanupCharacterLibrary();
            CleanupAllButtons();
            CleanupListViews();
        }
        #endregion

        #region Utilities
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
        }
        
        private void InitializeListViews()
        {
            m_characterListView = ContainerElement.Q<ListView>("character-list-view");
            m_characterListView.onSelectionChange += OnCharacterListViewSelectionChanged;
        }
        private void CleanupListViews()
        {
            m_characterListView.onSelectionChange -= OnCharacterListViewSelectionChanged;
            m_characterListView = null;
        }
        private void PopulateListViewWithProjectCharacters()
        {
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_characterListView, ref m_containerElement, m_characterLibrary.CharactersInProject, "character-list-view");
        }
        private void PopulateListViewWithSceneCharacters()
        {

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
        private void OnCharacterListViewSelectionChanged(IEnumerable<object> _obj)
        {

        }
        #endregion

    }

}
