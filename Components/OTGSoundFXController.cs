
using UnityEngine;

namespace OTG.CombatSM.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class OTGSoundFXController:MonoBehaviour
    {
        #region Inspector Vars
        [SerializeField] private E_SoundFXType m_sfxType;
        
        #endregion

        #region Fields
        private AudioSource m_audioSource;
        private AudioClip m_clip;
        #endregion

        #region Properties
        public E_SoundFXType SFXType { get { return m_sfxType; } }
        #endregion

        #region Public API
        public void OnSetClip(AudioClip _clip)
        {
            m_clip =  _clip;
        }
        public void OnPlaySFX()
        {
            m_audioSource.PlayOneShot(m_clip);
        }
        #endregion

        #region Unity API
        private void OnEnable()
        {
            Init();
        }
        private void OnDisable()
        {
            Cleanup();
        }
        #endregion

        #region Utility
        private void Init()
        {
            m_audioSource = GetComponent<AudioSource>();
            m_audioSource.playOnAwake = false;
        }
        private void Cleanup()
        {
            m_audioSource = null;
        }
        #endregion
    }

    public enum E_SoundFXType
    {
        WeaponSFX,
        LocomotionSFX,
        VocalSFX,
        ImpactSFX
    }
}
