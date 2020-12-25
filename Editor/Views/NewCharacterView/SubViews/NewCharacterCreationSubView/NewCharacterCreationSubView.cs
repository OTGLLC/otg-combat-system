
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEditor;

namespace OTG.CombatSystem.Editor
{
    public class NewCharacterCreationSubView : SubView
    {

        #region Fields
        private NewCharacterCreationData m_charCreationData;

        private Label m_characterModelText;
        private Label m_characterAvatarText;
        private TextField m_characterNameField;
        private EnumField m_characterTypeEnumField;
        private ListView m_modelsListView;
        private VisualElement m_objectDropBox;
        #endregion

       

        #region base class Implementation
        public NewCharacterCreationSubView(string _completionButton) : base(_completionButton) { }
        protected override void SetStrings()
        {
            m_templatePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/SubViews/NewCharacterCreationSubView/NewCharacterCreationSubViewTemplate.uxml";
            m_stylePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/SubViews/NewCharacterCreationSubView/NewCharacterCreationSubViewStyle.uss";
            ContainerStyleName = "new-character-creation-subview";
        }
        
        protected override void HandleViewFocused() 
        {
            m_charCreationData = new NewCharacterCreationData();
            GatherDataEntryFields();
            PopulateListViews();
            AttachCallbaks();
            AddSelectionChangedToListViews();
        }
        protected override void HandlerViewLostFocus()
        {
            CleanupDataEntryFields();
            RemoveCallbacks();
            RemoveSelectionChangedFromListViews();

            m_charCreationData.Cleanup();
            m_charCreationData = null;
        }
        protected override bool PerformCompletedEventActions()
        {
            m_charCreationData.CreateCharacter();
            return true;
        }
        #endregion

        #region DataSheet Functions
        private void GatherDataEntryFields()
        {
            m_characterNameField = ContainerElement.Q<TextField>("name-text-box");
            m_characterTypeEnumField = ContainerElement.Q<EnumField>("character-type-enum");
            m_modelsListView = ContainerElement.Q<ListView>("models-list-view");
            m_characterModelText = ContainerElement.Q<Label>("model-label-text");
            m_characterAvatarText = ContainerElement.Q<Label>("avatar-label-text");
            m_objectDropBox = ContainerElement.Q<VisualElement>("object-import-dropbox");
        }

        private void CleanupDataEntryFields()
        {
            m_characterNameField = null;
            m_characterTypeEnumField = null;
            m_modelsListView = null;

            m_objectDropBox = null;
        }
        private void PopulateListViews()
        {
            OTGCombatEditorUtilis.PopulateListView<string>(ref m_modelsListView, ref m_containerElement, m_charCreationData.AvailableModelsPaths, "models-list-view", true);
        }
        private void AttachCallbaks()
        {
            AddCallbacksToListView(ref m_modelsListView);
            m_objectDropBox.RegisterCallback<DragEnterEvent>(OnDragEnterEvent);
            m_characterNameField.RegisterValueChangedCallback(OnCharacterNameChanged);
            m_characterTypeEnumField.RegisterValueChangedCallback(OnCharacterTypeSelected);
        }
        private void RemoveCallbacks()
        {
            RemoveCallbacksFromListView(ref m_modelsListView);
            m_objectDropBox.UnregisterCallback<DragEnterEvent>(OnDragEnterEvent);
            m_characterNameField.UnregisterValueChangedCallback(OnCharacterNameChanged);
            m_characterTypeEnumField.UnregisterValueChangedCallback(OnCharacterTypeSelected);
        }
       private void AddSelectionChangedToListViews()
        {
            m_modelsListView.onSelectionChange += OnModelSelected;
           

        }
        private void RemoveSelectionChangedFromListViews()
        {
            m_modelsListView.onSelectionChange -= OnModelSelected;
           
        }
        private void SetCharacterModel(Object _obj)
        {
            m_charCreationData.OnCharacterModelSelected(_obj);
        }
       
        #endregion

        #region Callbacks
        private void OnModelSelected(IEnumerable<object> _obj)
        {
            foreach (string actionCandidate in _obj)
            {

                if (Event.current.type == EventType.MouseDown)
                {
                    Object clip = AssetDatabase.LoadAssetAtPath<Object>(actionCandidate.ToString());
                    Selection.activeObject = clip;
                    m_draggedItems[0] = clip;
                }
                if (Event.current.type == EventType.MouseUp)
                {
                    m_draggedItems[0] = null;
                }
            }
        }
       
        void OnDragEnterEvent(DragEnterEvent e)
        {
            
            Object[] draggedItem = DragAndDrop.objectReferences;
            if (draggedItem != null && draggedItem.Length > 0)
            {
                SetCharacterModel(draggedItem[0]);
                m_characterModelText.text = m_charCreationData.CharacterModel.name;
               
            }



        }
        private void OnCharacterNameChanged(ChangeEvent<string> _val)
        {
            m_charCreationData.OnCharacterName(_val.newValue);
        }
        private void OnCharacterTypeSelected(ChangeEvent<System.Enum> _val)
        {
            m_charCreationData.OnCharacterTypeSelected((e_CombatantType)_val.newValue);
        }
        #endregion
    }

}
