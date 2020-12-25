
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace OTG.CombatSystem.Editor
{
    public class OptionsView : BaseView
    {
        #region Constants
        private const string k_optionsDataPath = "Assets/Submodules/otg-combat-system/Editor";
        #endregion

        #region Fields
        private Label m_characterPath;
        private Label m_savedGraphPath;
        private Label m_actionsPath;
        private Label m_transitionsPath;

        private OptionsViewData m_optionsViewData;
        private ToolbarButton m_characterDataButton;
        private ToolbarButton m_savedGraphsButton;
        private ToolbarButton m_actionsButton;
        private ToolbarButton m_transitionsButton;
        #endregion


        #region base class Implementation
        protected override void SetStrings()
        {
            m_templatePath = "Assets/Submodules/otg-combat-system/Editor/Views/OptionsView/OptionsViewTemplate.uxml";
            m_stylePath = "Assets/Submodules/otg-combat-system/Editor/Views/OptionsView/OptionsViewStyle.uss";
            ContainerStyleName = "options-view";
        }
        protected override void HandleViewFocused()
        {
            GatherToolbarButtons();
            GatherLabels();
            FindOptionsData();
            
        }
        protected override void HandlerViewLostFocus()
        {
            CleanupToolbarButtons();
            CleanupOptionsViewData();
            CleanupLabels();
        }
        #endregion

        #region Utility
        private void FindOptionsData()
        {
            string[] optionsDataGuid = AssetDatabase.FindAssets("t:OptionsViewData");

            if(optionsDataGuid.Length == 0)
            {
                m_optionsViewData = ScriptableObject.CreateInstance<OptionsViewData>();
                AssetDatabase.CreateAsset(m_optionsViewData, k_optionsDataPath + "/OptionsViewData.asset");
                return;
            }

            m_optionsViewData = AssetDatabase.LoadAssetAtPath<OptionsViewData>(k_optionsDataPath + "/OptionsViewData.asset");


            SerializedObject optionsObj = new SerializedObject(m_optionsViewData);
            m_characterPath.text = optionsObj.FindProperty("m_characterDataPath").stringValue;
            m_savedGraphPath.text = optionsObj.FindProperty("m_savedGraphsPath").stringValue;
            m_actionsPath.text = optionsObj.FindProperty("m_actionsPath").stringValue;
             m_transitionsPath.text = optionsObj.FindProperty("m_transitionsPath").stringValue;


        }
        private void CleanupOptionsViewData()
        {
            m_optionsViewData = null;
        }
        private void GatherToolbarButtons()
        {
            m_characterDataButton = ContainerElement.Q<ToolbarButton>("character-data-path-button");
            m_characterDataButton.clickable.clicked += OnCharacterDataFolderButton;

            m_savedGraphsButton = ContainerElement.Q<ToolbarButton>("saved-graphs-data-path-button");
            m_savedGraphsButton.clickable.clicked += OnSavedGraphFolderButton;

            m_actionsButton = ContainerElement.Q<ToolbarButton>("actions-data-path-button");
            m_actionsButton.clickable.clicked += OnActionFolderButton;

            m_transitionsButton = ContainerElement.Q<ToolbarButton>("transitions-data-path-button");
            m_transitionsButton.clickable.clicked += OnTransitionFolderButton;

        }
        private void CleanupToolbarButtons()
        {
            m_characterDataButton.clickable.clicked -= OnCharacterDataFolderButton;
            m_savedGraphsButton.clickable.clicked -= OnSavedGraphFolderButton;
            m_actionsButton.clickable.clicked -= OnActionFolderButton;
            m_transitionsButton.clickable.clicked -= OnTransitionFolderButton;

            m_characterDataButton = null;
            m_savedGraphsButton = null;
            m_actionsButton = null;
            m_transitionsButton = null;
        }
        private void GatherLabels()
        {
            m_characterPath = ContainerElement.Q<Label>("character-data-path");
            m_savedGraphPath = ContainerElement.Q<Label>("saved-graphs-data-path");
            m_actionsPath = ContainerElement.Q<Label>("actions-data-path");
            m_transitionsPath = ContainerElement.Q<Label>("transitions-data-path");

        }
        private void CleanupLabels()
        {
            m_transitionsPath = null;
            m_actionsPath = null;
            m_characterPath = null;
            m_savedGraphPath = null;
        }
        private void ApplyPathSelection(string _path,string _property,Label _targetLabel)
        {
            SerializedObject optionsObj = new SerializedObject(m_optionsViewData);
            _targetLabel.text = _path;
            optionsObj.FindProperty(_property).stringValue = _path;
            optionsObj.ApplyModifiedProperties();
        }
        #endregion

        #region Callbacks
        private void OnActionFolderButton()
        {
            string path = OTGCombatEditorUtilis.GetAssetFolderPath(EditorUtility.OpenFolderPanel("Select Actions Root Folder", "", ""));
            ApplyPathSelection(path, "m_actionsPath",m_actionsPath);
        }
        private void OnTransitionFolderButton()
        {
            string path = OTGCombatEditorUtilis.GetAssetFolderPath(EditorUtility.OpenFolderPanel("Select Transitions Root Folder", "", ""));
            ApplyPathSelection(path, "m_transitionsPath",m_transitionsPath);
        }
        private void OnCharacterDataFolderButton()
        {
            string path = OTGCombatEditorUtilis.GetAssetFolderPath(EditorUtility.OpenFolderPanel("Select Character Data Root Folder", "", ""));
            ApplyPathSelection(path, "m_characterDataPath",m_characterPath);
        }
        private void OnSavedGraphFolderButton()
        {
            string path = OTGCombatEditorUtilis.GetAssetFolderPath(EditorUtility.OpenFolderPanel("Select Character Graph Data Root Folder", "", ""));
            ApplyPathSelection(path, "m_savedGraphsPath",m_savedGraphPath);
        }
        #endregion
    }

}
