using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSystem.Editor
{
    public class CharacterSavedGraph : ScriptableObject
    {
        private Dictionary<string, Vector2> m_nodePositionLookup;


        [HideInInspector]
        [SerializeField] private List<CharacterStateNodePositionData> m_nodePositionData;

        private void OnEnable()
        {
            if (m_nodePositionData == null)
                m_nodePositionData = new List<CharacterStateNodePositionData>();

            m_nodePositionLookup = new Dictionary<string, Vector2>();
            for (int i = 0; i < m_nodePositionData.Count; i++)
            {
                if (!m_nodePositionLookup.ContainsKey(m_nodePositionData[i].NodeName))
                {
                    m_nodePositionLookup.Add(m_nodePositionData[i].NodeName, m_nodePositionData[i].NodePosition);
                }
            }
        }

        /**
        public Vector2 GetNodePosition(StateNode _nodeData)
        {
            Vector2 position = new Vector2();

            if (!m_nodePositionLookup.TryGetValue(_nodeData.OwnerState.name, out position))
            {
                Vector2 pos = GetRawNodePosition(_nodeData);
                m_nodePositionLookup.Add(_nodeData.OwnerState.name, pos);
            }


            return position;
        }
        **/

        public void SaveNodePosition(string _stateName, Vector2 _position)
        {
            InsertEntryToLookup(_stateName, _position);
            SerializeItems();
        }

        /**
        private Vector2 GetRawNodePosition(StateNode _nodeData)
        {
            Vector2 position = new Vector2((_nodeData.Level * 200) + 150, (_nodeData.Order * 150));
            return position;
        }
        **/
        private void InsertEntryToLookup(string _stateName, Vector2 position)
        {
            if (m_nodePositionLookup.ContainsKey(_stateName))
                m_nodePositionLookup[_stateName] = position;
        }
        private void SerializeItems()
        {
            m_nodePositionData.Clear();
            foreach (KeyValuePair<string, Vector2> pair in m_nodePositionLookup)
            {
                CharacterStateNodePositionData data = new CharacterStateNodePositionData()
                {
                    NodeName = pair.Key,
                    NodePosition = pair.Value
                };
                m_nodePositionData.Add(data);
            }
        }

    }
    [System.Serializable]
    public struct CharacterStateNodePositionData
    {

        public string NodeName;
        public Vector2 NodePosition;
    }
}