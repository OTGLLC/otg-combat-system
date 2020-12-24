
using UnityEngine;


namespace OTG.CombatSystem.Editor
{
    public class NewCharacterView : BaseView
    {


        #region base class Implementation
        protected override void SetStrings()
        {
            m_templatePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/NewCharacterViewTemplate.uxml";
            m_stylePath = "Assets/Submodules/otg-combat-system/Editor/Views/NewCharacterView/NewCharacterViewTemplate.uxml";
            ContainerStyleName = "new-character-view";
        }
        protected override void HandleViewFocused()
        {
            
        }
        protected override void HandlerViewLostFocus()
        {
            
        }
        #endregion
    }

}
