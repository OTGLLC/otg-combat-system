
using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSM.Core
{
    [System.Serializable]
    public class SoundHandlerData
    {
        public Dictionary<OTGSFXIdentification, E_SoundFXType> SoundFXTOID
        {
            get
            {
                if(m_soundFXToID == null)
                {
                    CreateLookup();
                }
                return m_soundFXToID;
            }
        }
        private Dictionary<OTGSFXIdentification, E_SoundFXType> m_soundFXToID;

        [SerializeField] private SoundIDToSoundType[] m_soundIDToTypes;
        
        private void CreateLookup()
        {
            m_soundFXToID = new Dictionary<OTGSFXIdentification, E_SoundFXType>();
            for(int i = 0; i < m_soundIDToTypes.Length; i++)
            {
                if (!m_soundFXToID.ContainsKey(m_soundIDToTypes[i].ID))
                    m_soundFXToID.Add(m_soundIDToTypes[i].ID, m_soundIDToTypes[i].SFXType);
                   
            }
        }
    }
    
    [System.Serializable]
    public class SoundIDToSoundType
    {
        [SerializeField] private OTGSFXIdentification m_sfxID;
        [SerializeField] private E_SoundFXType m_sfxType;

        public OTGSFXIdentification ID { get { return m_sfxID; } }
        public E_SoundFXType SFXType { get { return m_sfxType; } }
    }
}
