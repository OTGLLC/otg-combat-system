using UnityEngine;
using System.Collections.Generic;

namespace OTG.CombatSystem
{
    public class CollisionHandler
    {
        #region Fields
        private CollisionsHandlerData m_handlerData;
        private CombatAnimHurtCollisionData m_hurtData;
        #endregion

        #region Properties
        public OTGHitColliderController HitCollider { get; private set; }
        public Dictionary<OTGHurtColliderID, OTGHurtColliderController> HurtColliders;
        public int NumberOfContacts { get; set; }
        public Collider[] ScanResults { get; private set; }
        //public OTGTargetingController TargetingController { get; private set; }
        #endregion

        #region Public API
        public CollisionHandler(HandlerDataGroup _dataGroup, OTGHitColliderController _hitCollider, OTGHurtColliderController[] _hurtColliders, OTGTargetingController _targetingController,OTGGlobalCombatConfig _globalConfig)
        {
            InitHandler(_dataGroup, _hitCollider, _globalConfig);
            InitHurtColliderLookup(_hurtColliders);
            //TargetingController = _targetingController;
            //TargetingController.InitController(_globalConfig.MaxScanElemements);
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        public void SetHurtColliderData(CombatAnimHurtCollisionData _data)
        {
            m_hurtData = _data;
#if UNITY_EDITOR
            HasCollisionID = false;
#endif
        }
        public void OnAnimHurtColliderEvent(OTGHurtColliderID _hurtColID)
        {
            
#if UNITY_EDITOR
            HasCollisionID = true;
#endif

            if (HurtColliders.ContainsKey(_hurtColID))
            {
                NumberOfContacts = HurtColliders[_hurtColID].OnPerformDamageScan(ScanResults, m_hurtData);
            }
        }
        #endregion

        #region Utility
        private void InitHandler(HandlerDataGroup _dataGroup, OTGHitColliderController _hitCollider, OTGGlobalCombatConfig _globalConfig)
        {
            m_handlerData = _dataGroup.CollisionHandlerData;
            HitCollider = _hitCollider;
            ScanResults = new Collider[_globalConfig.MaxScanElemements];
        }
        private void InitHurtColliderLookup(OTGHurtColliderController[] _hurtColliders)
        {
            HurtColliders = new Dictionary<OTGHurtColliderID, OTGHurtColliderController>();
            for(int i = 0; i < _hurtColliders.Length; i++)
            {
                OTGHurtColliderController ctrl = _hurtColliders[i];
                if (!HurtColliders.ContainsKey(ctrl.HurtColliderID))
                    HurtColliders.Add(ctrl.HurtColliderID, ctrl);
            }
        }
        private void Cleanup()
        {
            //TargetingController = null;
            m_handlerData = null;
            m_hurtData = null;
            HitCollider = null;
            HurtColliders.Clear();
            HurtColliders = null;
            ScanResults = null;
        }
        #endregion

        #region Unity Editor Only
#if UNITY_EDITOR
        public bool HasCollisionID { get; set; }
#endif
#endregion

    }
}