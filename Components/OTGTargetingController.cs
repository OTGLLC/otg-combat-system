using System.Collections;
using UnityEngine;

namespace OTG.CombatSM.Core
{
    [RequireComponent(typeof(BoxCollider))]
    public class OTGTargetingController : MonoBehaviour
    {
        #region Properties
        public int ValidTargets { get; private set; }
        public Collider[] TargetingResults { get; private set; }
        #endregion

        #region Fields
        private BoxCollider m_targetingCollider;
        private Transform m_trans;
        [SerializeField] private LayerMask m_validTargets;
        #endregion

        #region Unity API
        private void OnEnable()
        {
            m_targetingCollider = GetComponent<BoxCollider>();
            m_trans = GetComponent<Transform>();
            
        }
        private void OnDisable()
        {
            m_targetingCollider = null;
            m_trans = null;
            TargetingResults = null;
        }
        #endregion

        #region Public API
        public void InitController(int _maxScanElements)
        {
            TargetingResults = new Collider[_maxScanElements];
        }
        public void ScanForTargets()
        {
            ValidTargets = Physics.OverlapBoxNonAlloc(m_trans.position, m_targetingCollider.size / 2, TargetingResults, m_trans.localToWorldMatrix.rotation, m_validTargets);
        }
        #endregion

        #region Utility

        #endregion
    }
}