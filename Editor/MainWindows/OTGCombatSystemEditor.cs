using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
namespace OTG.CombatSystem.Editor
{
    public class OTGCombatSystemEditor : EditorWindow
    {
        #region COnstants
        private const string k_templatePath = "Assets/Submodules/otg-combat-system/Editor/MainWindows/OTGCombatSystemEditorTemplate.uxml";
        private const string k_stylePath = "Assets/Submodules/otg-combat-system/Editor/MainWindows/OTGCombatSystemEditorStyle.uss";
        #endregion

        [MenuItem("OTG Tools/Combatant Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow<OTGCombatSystemEditor>();
            window.titleContent = new GUIContent("Combatant Editor");
            window.minSize = new Vector2(800, 800);
        }

        #region Unity API
        private void OnEnable()
        {
            InitializeLayout();
            InitializeStyleSheet();
        }
        #endregion

        #region Utility
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
        #endregion
    }

}
