
using UnityEngine;

namespace OTG.CombatSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class OTGSoundFXController:MonoBehaviour
    {
        #region Inspector Vars
        [SerializeField] private OTGSFXIdentification m_ID;
        #endregion

        #region Fields
        private AudioSource m_audioSource;
        private AudioClip m_clip;
        #endregion

        #region Properties
        public OTGSFXIdentification ID { get { return m_ID; } }
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

}
