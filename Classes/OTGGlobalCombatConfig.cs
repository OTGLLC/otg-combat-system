#pragma warning disable CS0649

using System.Collections;
using UnityEngine;

namespace OTG.CombatSM.Core
{
    [CreateAssetMenu]
    public class OTGGlobalCombatConfig : ScriptableObject
    {
        [SerializeField] private int m_maxScanElements = 10;
        [SerializeField] private float m_gravitySetting;
        [SerializeField] private float m_stickingForce;
        [SerializeField] private LayerMask m_playerPushBox;
        [SerializeField] private LayerMask m_playerHitBox;
        [SerializeField] private LayerMask m_playerHurtBox;
        [SerializeField] private LayerMask m_enemyPushBox;
        [SerializeField] private LayerMask m_enemyHitBox;
        [SerializeField] private LayerMask m_enemyHurtBox;
        [SerializeField] private LayerMask m_targetingBox;
        [SerializeField] private SoundHandlerData m_soundHandlerData;
        [SerializeField] private float m_laneDistance;
        [SerializeField] private float m_facingRightRotation;
        [SerializeField] private float m_facingLeftRotation;

        public int MaxScanElemements { get { return m_maxScanElements; } }
        public LayerMask PlayerPushBox { get { return m_playerPushBox; } }
        public LayerMask PlayerHitBox { get { return m_playerHitBox; } }
        public LayerMask PlayerHurtBox { get { return m_playerHurtBox; } }
        public LayerMask EnemyPushBox { get { return m_enemyPushBox; } }
        public LayerMask EnemyHitBox { get { return m_enemyHitBox; } }
        public LayerMask EnemyHurtBox { get { return m_enemyHurtBox; } }
        public LayerMask TargetingBox { get { return m_targetingBox; } }
        public float GravitySetting { get { return m_gravitySetting; } }
        public float StickingForce { get { return m_stickingForce; } }
        public SoundHandlerData SoundHandleData { get { return m_soundHandlerData; } }
        public float LaneDistance { get { return m_laneDistance; } }
        public float FacingRightRotation { get { return m_facingRightRotation; } }
        public float FacingLeftRotation { get { return m_facingLeftRotation; } }
  
        //public const int MaxHitScanElements = 10;


        //public const int LAYER_PLAYER_PUSH = 9;
        //public const int LAYER_PLAYER_HIT = 10;
        //public const int LAYER_PLAYER_HURT = 11;

        //public const int LAYER_ENEMY_PUSH = 12;
        //public const int LAYER_ENEMY_HIT = 13;
        //public const int LAYER_ENEMY_HURT = 14;

        //public const int LAYER_TARGETING = 15;

        //public const float GRAVITY_SETTTING = 10;
        //public const float STICKING_FORCE = 1;

    }
   
}