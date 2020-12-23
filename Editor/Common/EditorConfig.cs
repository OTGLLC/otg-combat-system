using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OTG.CombatSystem.Editor
{
   
    [CreateAssetMenu]
    public class EditorConfig : ScriptableObject
    {
        [SerializeField] private string m_editorConfigsPath;
        public string EditorConfigsPath { get { return m_editorConfigsPath; } }
        [SerializeField] private string m_combatActionsPath;
        public string CombatActionsPath { get { return m_combatActionsPath; } }
        [SerializeField] private string m_combatTransitionsPath;
        public string CombatTransitionsPath { get { return m_combatTransitionsPath; } }
        [SerializeField] private string m_characterPAthRoot;
        public string CharacterPathRoot { get { return m_characterPAthRoot; } }
        [SerializeField] private string m_templatesPath;
        public string TemplatesPaths { get { return m_templatesPath; } }
        [SerializeField] private string m_characterSavedGraphsPath;
        public string CharacterSavedGraphsPath { get { return m_characterSavedGraphsPath; } }

        //[SerializeField] private OTGGlobalCombatConfig m_globalCombatConfig;
        //public OTGGlobalCombatConfig GlobalCombatConfig { get { return m_globalCombatConfig; } }
        
    }

}
