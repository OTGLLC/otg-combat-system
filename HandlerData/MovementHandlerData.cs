

using UnityEngine;

namespace OTG.CombatSM.Core
{
    [System.Serializable]
    public class MovementHandlerData
    {
        #region Inspector Vars
        [SerializeField] private float m_groundAcceleration;
        [SerializeField] private float m_airAcceleration;
        [SerializeField] private float m_maxXAxisSpeed;
        [SerializeField] private float m_maxYAxisSpeed;
        [SerializeField] private float m_maxZAxisSpeed;
        #endregion

        #region Properties
        public float GroundAccelreration { get { return m_groundAcceleration; } }
        public float AirAcceleration { get { return m_airAcceleration; } }
        public float MaxXaxisSpeed { get { return m_maxXAxisSpeed; } }
        public float MaxYaxisSpeed { get { return m_maxYAxisSpeed; } }
        public float MaxZaxisSpeed { get { return m_maxZAxisSpeed; } }
        #endregion
    }
    
}