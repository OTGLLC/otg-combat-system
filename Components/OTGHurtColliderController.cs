using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class OTGHurtColliderController : MonoBehaviour
    {
        #region Inspector Vars
        [SerializeField] private OTGHurtColliderID m_hurtColliderID;
        #endregion

        #region Properties
        public OTGHurtColliderID HurtColliderID { get { return m_hurtColliderID; } }
       
        #endregion

        #region Fields
        private Transform m_trans;
        private BoxCollider m_boxCollider;
        #endregion

        #region Unity API
        private void OnEnable()
        {
           
            m_trans = GetComponent<Transform>();
            m_boxCollider = GetComponent<BoxCollider>();
        }
        private void OnDisable()
        {
    
            m_trans = null;
            m_boxCollider = null;
        }
        #endregion

        #region Publc API
        
        public int OnPerformDamageScan(Collider[] _scanResults,CombatAnimHurtCollisionData _data)
        {
            
            return Physics.OverlapBoxNonAlloc(m_trans.position, m_boxCollider.size/2, _scanResults, m_trans.rotation, _data.ValidTargets);

        }
        #endregion
    }
    

}
