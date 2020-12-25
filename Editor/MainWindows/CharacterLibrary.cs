
using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSystem.Editor
{
    public class CharacterLibrary : ScriptableObject
    {

        [SerializeField]private List<string> m_characterNames;
        public List<string> CharactersInProject { get { return m_characterNames; } }
        public void AddCharacter(string _name)
        {
            if (!m_characterNames.Contains(_name))
                m_characterNames.Add(_name);
        }
        public void RemoveCharacter(string _name)
        {
            if (m_characterNames.Contains(_name))
                m_characterNames.Remove(_name);
        }
        public void StartNewProject()
        {
            m_characterNames.Clear();
        }
    }

   
}
