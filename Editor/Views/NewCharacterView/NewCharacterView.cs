
using UnityEngine;


namespace OTG.CombatSystem.Editor
{
    public class NewCharacterView : BaseView
    {

        #region Subviews
        private NewCharacterBeginningView m_beginNewCharSubView;
        private NewCharacterCreationSubView m_newCharacterCreationSubView;
        #endregion


        #region base class Implementation
        protected override void SetStrings()
        {
            m_templatePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/NewCharacterViewTemplate.uxml";
            m_stylePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/NewCharacterViewStyle.uss";
            ContainerStyleName = "new-character-view";
        }
        protected override void HandleViewFocused()
        {
            CreateSubViews();
            SwitchSubViews(m_beginNewCharSubView);
        }
        protected override void HandlerViewLostFocus()
        {
            CleanupSubviews();
        }
        protected override void HandleSubviewCompletedEvent()
        {
            if (MatchingSubView(m_beginNewCharSubView))
            {
                SwitchSubViews(m_newCharacterCreationSubView);
                return;
            }
                

            if (MatchingSubView(m_newCharacterCreationSubView))
            {
                SwitchSubViews(m_beginNewCharSubView);
                return;
            }
                
        }
        public override void UpdateViewHeight(float _height)
        {
            base.UpdateViewHeight(_height);
            m_currentSubView.UpdateViewHeight(_height);
        }
        #endregion

        #region Utility
        private void CreateSubViews()
        {
            m_beginNewCharSubView = new NewCharacterBeginningView("begin-new-character-button");
            m_newCharacterCreationSubView = new NewCharacterCreationSubView("finished-character-creation");
        }
        private void CleanupSubviews()
        {
            m_beginNewCharSubView = null;
            m_newCharacterCreationSubView = null;
        }
        private bool MatchingSubView(SubView _targetSubView)
        {
            return (string.Equals(m_currentSubView.ContainerStyleName, _targetSubView.ContainerStyleName, System.StringComparison.OrdinalIgnoreCase));
        }
        #endregion

    }

}
