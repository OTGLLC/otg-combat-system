using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OTG.CombatSystem.Editor
{
    public class NewCharacterCreationSubView : SubView
    {

        #region base class Implementation
        public NewCharacterCreationSubView(string _completionButton) : base(_completionButton) { }
        protected override void SetStrings()
        {
            m_templatePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/SubViews/NewCharacterCreationSubView/NewCharacterCreationSubViewTemplate.uxml";
            m_stylePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/SubViews/NewCharacterCreationSubView/NewCharacterCreationSubViewStyle.uss";
            ContainerStyleName = "new-character-creation-subview";
        }
        protected override void HandleViewFocused() { }
        protected override void HandlerViewLostFocus() { }
        protected override bool PerformCompletedEventActions()
        {

            return true;
        }
        #endregion
    }

}
