
using UnityEngine;
using UnityEngine.UIElements;

namespace OTG.CombatSystem.Editor
{
    public abstract class SubView : BaseView
    {

        public delegate void Completed();
        public event Completed CompletedEvent;

        protected Button m_completionButton;


        #region Public API
        public SubView(string _completionButtonName)
        {
            m_completionButton = ContainerElement.Q<Button>(_completionButtonName);
            m_completionButton.clickable.clicked += OnCompleted;
        }
        public virtual void Cleanup()
        {
            m_completionButton.clickable.clicked -= OnCompleted;
            m_completionButton = null;
        }
        protected void OnCompleted()
        {
            if(CompletedEvent!=null)
            {
                if(PerformCompletedEventActions())
                    CompletedEvent();
            }
        }
        #endregion

        #region Abstract Methods
        protected abstract bool PerformCompletedEventActions();
        #endregion


    }

}
