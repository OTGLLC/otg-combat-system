using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTG.CombatSystem.Editor
{
    public class NewCharacterBeginningView : SubView
    {


        #region base class Implementation
        public NewCharacterBeginningView(string _complettionButton) : base(_complettionButton) { }
        protected override void SetStrings()
        {
            m_templatePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/SubViews/NewCharacterBeginningView/NewCharacterBeginningViewTemplate.uxml";
            m_stylePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/SubViews/NewCharacterBeginningView/NewCharacterBeginningViewStyle.uss";
            ContainerStyleName = "new-character-beginning-subview";
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

