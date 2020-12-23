using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OTG.CombatSM.Core
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
        public void OnAnimationEvent(OTGAnimationEvent _event)
        {
            if (_event.VfxID == null)
                return;
            if (VFXLookup.Count == 0)
                return;

            VFXLookup[_event.VfxID].OnPlayVFX();
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
