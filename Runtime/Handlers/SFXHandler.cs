
using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSystem
{
    public class SFXHandler
    {
        private Dictionary<OTGSFXIdentification, OTGSoundFXController> m_soundFXControllers;
        private SoundHandlerData m_data;

        #region Public API
        public SFXHandler(SoundHandlerData _data,OTGSoundFXController[] _sfxControllers)
        {
            m_data = _data;
            InitializeSFXController(_sfxControllers);
        }
        public void CleanupHandler()
        {
            CleanupSFXControllers();
        }
        public void SetData(SoundFXData[] _allData)
        {
            for(int i = 0; i < _allData.Length; i++)
            {
                if (m_soundFXControllers.ContainsKey(_allData[i].ID))
                    m_soundFXControllers[_allData[i].ID].OnSetClip(_allData[i].ClipToPlay);
            }
        }
        public void OnSFXEvent(OTGSFXIdentification _sfxID)
        {
       
            m_soundFXControllers[_sfxID].OnPlaySFX();
        }
        #endregion

        #region Utility
        private void InitializeSFXController(OTGSoundFXController[] _sfxControllers)
        {
            m_soundFXControllers = new Dictionary<OTGSFXIdentification, OTGSoundFXController>();
            for(int i = 0; i < _sfxControllers.Length; i++)
            {
                if (!m_soundFXControllers.ContainsKey(_sfxControllers[i].ID))
                    m_soundFXControllers.Add(_sfxControllers[i].ID, _sfxControllers[i]);
            }
        }
        private void CleanupSFXControllers()
        {
            m_soundFXControllers.Clear();
            m_soundFXControllers = null;
            m_data = null;
        }
        #endregion
    }
}
