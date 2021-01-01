
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace OTG.CombatSystem.Editor
{
    public class SelectedCharacterData
    {
        #region Properties
        public OTGCombatSMC SelectedCharacterSMC { get; private set; }
        public SerializedObject StartingStateSO { get { return m_startingStateSO; } }
        public List<AvailableStateData> AvailableStates;
        #endregion

        #region Fields
        private SerializedObject m_selectedCharSO;
        private SerializedObject m_startingStateSO;
        #endregion

        #region Public API
        public SelectedCharacterData()
        {

        }
        public void Cleanup()
        {

        }
        public void OnCharacterSelected(OTGCombatSMC _selectedCharacter)
        {
            SelectedCharacterSMC = _selectedCharacter;
            GetStartingStateSO(ref m_selectedCharSO, ref m_startingStateSO);
        }
        #endregion

        #region Utility
        private void GetStartingStateSO(ref SerializedObject _charSO,ref SerializedObject _startingStateSO)
        {
            _charSO = new SerializedObject(SelectedCharacterSMC);
            _startingStateSO = new SerializedObject(_charSO.FindProperty("m_startingState").objectReferenceValue);

            AvailableStates = new List<AvailableStateData>();
            AvailableStateData data = new AvailableStateData()
            {
                StateSOBj = m_startingStateSO,
                HeightLevel = 0,
                WidthLevl = 0
            };
            AvailableStates.Add(data);
            GetChildState(m_startingStateSO,1);
            
        }
        private void GetChildState(SerializedObject _parent, int _width)
        {
            int transitions = _parent.FindProperty("m_stateTransitions").arraySize;

            if (transitions == 0)
                return;

            for(int i = 0; i < transitions;i++)
            {
                Object child = _parent.FindProperty("m_stateTransitions").GetArrayElementAtIndex(i).FindPropertyRelative("m_nextState").objectReferenceValue;
                if (child == null)
                    continue;
                SerializedObject obj = new SerializedObject(child);

                var hasState = AvailableStates.Where(x => string.Equals(x.StateSOBj.targetObject.name, obj.targetObject.name, System.StringComparison.OrdinalIgnoreCase)).ToArray();


                AvailableStateData data = new AvailableStateData()
                {
                    StateSOBj = obj,
                    WidthLevl = _width,
                    HeightLevel = i,
                    IsRepeat = hasState.Length > 0
                };
                AvailableStates.Add(data);

                if(hasState.Length == 0)
                    GetChildState(obj, ++_width);
            }

        }
        #endregion
    }
    public struct AvailableStateData
    {
        public SerializedObject StateSOBj;
        public int WidthLevl;
        public int HeightLevel;
        public bool IsRepeat;
    }
}
