
using UnityEngine;

namespace OTG.CombatSystem
{
    public class OTGWeapon : MonoBehaviour
    {
        [SerializeField] private OTGWeaponID m_weaponID;
        public OTGWeaponID ID { get { return m_weaponID; } }

        
        [SerializeField] private WeaponPositionData m_PosData;
       

        private Transform m_transform;

        #region Unity API
        private void OnEnable()
        {
            InitializeProperties();
            unEquipWeapon();
        }
        private void OnDisable()
        {
            unEquipWeapon();
            Cleanup();
        }

        #endregion

        #region Public API
        public void EquipCurrentWeapon()
        {
            EquipWeapon();
        }
        public void UnEquipCurrentWeapon()
        {
            unEquipWeapon();
        }
        #endregion

        #region Utility

        private void unEquipWeapon()
        {
            m_transform.parent = m_PosData.IdleParent;
            m_transform.localPosition = m_PosData.IdlePositionOffset;
            m_transform.localRotation = Quaternion.Euler(m_PosData.IdleRotationOffset);
        }
        private void EquipWeapon()
        {
            m_transform.parent = m_PosData.EquippedParent;
            m_transform.localPosition = m_PosData.EquippedPositionOffset;
            m_transform.localRotation = Quaternion.Euler(m_PosData.EquippedRotationOffset);
        }
        private void InitializeProperties()
        {
            m_transform = GetComponent<Transform>();
            
        }
        private void Cleanup()
        {
            m_transform = null;
        }
        #endregion
    }

    [System.Serializable]
    public struct WeaponPositionData
    {
        public Transform EquippedParent;
        public Vector3 EquippedPositionOffset;
        public Vector3 EquippedRotationOffset;

        public Transform IdleParent;
        public Vector3 IdlePositionOffset;
        public Vector3 IdleRotationOffset;
        
    }
}
