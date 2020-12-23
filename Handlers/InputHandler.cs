
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class InputHandler
    {
        #region Fields
        private InputHandlerData m_handlerData;
        #endregion

        #region Properties
        public Vector2 MovementVector { get; set; }
        public InfiniteRunnerInput RunnerInput { get; set; }
        #endregion

        #region Public API
        public InputHandler(HandlerDataGroup _dataGroup)
        {
            InitializeData(_dataGroup);
            RunnerInput = new InfiniteRunnerInput();
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        #endregion

        #region Utility
        private void InitializeData(HandlerDataGroup _group)
        {
            m_handlerData = _group.InputsHandlerData;
        }
        private void Cleanup()
        {
            m_handlerData = null;
        }
        #endregion

    }

    public class InfiniteRunnerInput
    {
        public bool HasRightInput { get; set; }
        public bool HasLeftInput { get; set; }
        public bool HasSwitchLanesUpInput { get; set; }
        public bool HasSwitchLanesDownInpu { get; set; }
    }
}