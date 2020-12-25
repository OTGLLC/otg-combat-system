using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace OTG.CombatSystem.Editor
{
    public class OTGCombatSystemEditor : EditorWindow
    {
        #region COnstants
        private const string k_templatePath = "Assets/Submodules/otg-combat-system/Editor/MainWindows/OTGCombatSystemEditorTemplate.uxml";
        private const string k_stylePath = "Assets/Submodules/otg-combat-system/Editor/MainWindows/OTGCombatSystemEditorStyle.uss";
        private const string k_characterLibraryPath = "Assets/Submodules/otg-combat-system/Editor";
        #endregion

        #region Views
        private BaseView m_currentView;
        private NewCharacterView m_newCharacterView;
        private CharacterStateGraphView m_characterStateGraphView;
        private OptionsView m_optionsView;
        #endregion

        #region Menu Items
        private Toolbar m_menuToolbar;
        private ToolbarButton m_newCharViewButton;
        private ToolbarButton m_stateGraphViewButton;
        private ToolbarButton m_optionsViewButton;
        #endregion

        [MenuItem("OTG Tools/Combatant Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow<OTGCombatSystemEditor>();
            window.titleContent = new GUIContent("Combatant Editor");
            window.minSize = new Vector2(600, 800);
        }

        #region Unity API
        private void OnEnable()
        {

            InitializeLayout();
            InitializeStyleSheet();
            CreateViews();
            BuildToolbarMenu();
            InitializeCharacterLibrary();

            SwitchViews(m_characterStateGraphView);
        }
        private void OnGUI()
        {
            UpdateHeightOftheMainContainer();
            UpdateViewHeight();
        }
        private void OnDisable()
        {
            CleanupToolbarMenu();
            CleanupViews();
        }
        #endregion

        #region Utility
        private void InitializeCharacterLibrary()
        {
            OTGCombatEditorUtilis.InitializeCharacterLibrary(k_characterLibraryPath);

        }
        private void InitializeLayout()
        {
            VisualTreeAsset layout = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(k_templatePath);
            TemplateContainer treeAsset = layout.CloneTree();
            rootVisualElement.Add(treeAsset);

        }
        private void InitializeStyleSheet()
        {
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(k_stylePath);
            rootVisualElement.styleSheets.Add(styleSheet);
        }
        private void UpdateHeightOftheMainContainer()
        {
            rootVisualElement.Q<VisualElement>("main-window").style.height = new
            StyleLength(position.height);
        }
        private void UpdateViewHeight()
        {
            if (m_currentView == null)
                return;

            m_currentView.UpdateViewHeight(position.height);
        }
        private void CreateViews()
        {
            m_newCharacterView = new NewCharacterView();
            m_characterStateGraphView = new CharacterStateGraphView();
            m_optionsView = new OptionsView();
        }
        private void CleanupViews()
        {
            m_currentView = null;
            m_newCharacterView = null;
            m_characterStateGraphView = null;
            m_optionsView = null;
        }
        private void SwitchViews(BaseView _newView)
        {
            if (m_currentView != null)
            {
                m_currentView.OnViewLostFocus();
                rootVisualElement.Q<VisualElement>("main-window").Remove(m_currentView.ContainerElement);
            }

            m_currentView = _newView;

            rootVisualElement.Q<VisualElement>("main-window").Add(m_currentView.ContainerElement);
            m_currentView.OnViewFocused();
        }
        private void BuildToolbarMenu()
        {
            m_newCharViewButton = new ToolbarButton();
            m_newCharViewButton.text = "New Character View";
            m_newCharViewButton.clickable.clicked += () => { SwitchViews(m_newCharacterView); };
           

            m_stateGraphViewButton = new ToolbarButton();
            m_stateGraphViewButton.text = "State Graph View";
            m_stateGraphViewButton.clickable.clicked += () => { SwitchViews(m_characterStateGraphView); };

            m_optionsViewButton = new ToolbarButton();
            m_optionsViewButton.text = "Options";
            m_optionsViewButton.clickable.clicked += () => { SwitchViews(m_optionsView); };


            m_menuToolbar = rootVisualElement.Q<Toolbar>("editor-menu-toolbar");
            m_menuToolbar.Add(m_newCharViewButton);
            m_menuToolbar.Add(m_stateGraphViewButton);
            m_menuToolbar.Add(m_optionsViewButton);
        }
        private void CleanupToolbarMenu()
        {
            m_newCharViewButton.clickable.clicked -= () => { SwitchViews(m_newCharacterView); };
            m_stateGraphViewButton.clickable.clicked -= () => { SwitchViews(m_characterStateGraphView); };
            m_optionsViewButton.clickable.clicked -= () => { SwitchViews(m_optionsView); };
        }
        #endregion
    }

}
