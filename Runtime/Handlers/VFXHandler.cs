using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OTG.CombatSystem
{
    public class VFXHandler 
    {
        #region properties
        public Dictionary<OTGVFXIdentification, OTGVFXController> VFXLookup;
        #endregion


        #region Public API
        public VFXHandler(OTGVFXController[] _vfxControllers)
        {
            InitializeVFXLookup(_vfxControllers);
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        public void OnVFXEvent(OTGVFXIdentification _vfxID)
        {
            if (VFXLookup.Count == 0)
                return;
            VFXLookup[_vfxID].OnPlayVFX();
        }
        #endregion

        #region Utility
        private void InitializeVFXLookup(OTGVFXController[] _vfxControllers)
        {
            VFXLookup = new Dictionary<OTGVFXIdentification, OTGVFXController>();
            for(int i = 0; i < _vfxControllers.Length; i++)
            {
                if(!VFXLookup.ContainsKey(_vfxControllers[i].VfxID))
                {
                    VFXLookup.Add(_vfxControllers[i].VfxID, _vfxControllers[i]);
                }
            }
        }
        private void Cleanup()
        {
            VFXLookup.Clear();
            VFXLookup = null;
        }
        #endregion
    }

}
