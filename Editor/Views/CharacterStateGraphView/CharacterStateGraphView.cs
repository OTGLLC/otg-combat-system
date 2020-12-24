
using UnityEngine;


namespace OTG.CombatSystem.Editor
{
    public class CharacterStateGraphView : BaseView
    {

        #region base class Implementation
        protected override void SetStrings()
        {
            m_templatePath = "Assets/Submodules/otg-combat-system/Editor/Views/CharacterStateGraphView/CharacterStateGraphViewTemplate.uxml";
            m_stylePath = "Assets/Submodules/otg-combat-system/Editor/Views/CharacterStateGraphView/CharacterStateGraphViewStyle.uss";
            ContainerStyleName = "character-stategraph-view";
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
