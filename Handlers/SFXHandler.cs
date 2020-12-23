
using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class SFXHandler
    {
        private Dictionary<E_SoundFXType, OTGSoundFXController> m_soundFXControllers;
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
                if (m_soundFXControllers.ContainsKey(_allData[i].SFXType))
                    m_soundFXControllers[_allData[i].SFXType].OnSetClip(_allData[i].ClipToPlay);
            }
        }
        public void OnAnimationEventUpdate(OTGAnimationEvent _ev)
        {
            if (_ev.SfxID == null)
                return;
            E_SoundFXType type = m_data.SoundFXTOID[_ev.SfxID];
            m_soundFXControllers[type].OnPlaySFX();
        }
        #endregion

        #region Utility
        private void InitializeSFXController(OTGSoundFXController[] _sfxControllers)
        {
            m_soundFXControllers = new Dictionary<E_SoundFXType, OTGSoundFXController>();
            for(int i = 0; i < _sfxControllers.Length; i++)
            {
                if (!m_soundFXControllers.ContainsKey(_sfxControllers[i].SFXType))
                    m_soundFXControllers.Add(_sfxControllers[i].SFXType, _sfxControllers[i]);
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
