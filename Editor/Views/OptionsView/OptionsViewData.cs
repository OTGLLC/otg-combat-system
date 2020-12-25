
using UnityEngine;

namespace OTG.CombatSystem.Editor
{
    public class OptionsViewData : ScriptableObject
    {
        [SerializeField] private string m_characterDataPath;
        public string CharacterDataPath { get { return m_characterDataPath; } }

        [SerializeField] private string m_savedGraphsPath;
        public string SavedGraphsPath { get { return m_savedGraphsPath; } }

        [SerializeField] private string m_actionsPath;
        public string ActionsPath { get { return m_actionsPath; } }

        [SerializeField] private string m_transitionsPath;
        public string TransitionsPath { get { return m_transitionsPath; } }

    }
}
