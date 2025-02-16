using UnityEngine;

namespace FireNBM
{
    public class ObjectTypeBaseActionComp : ActionObjectComp
    {
        private ActionData m_actionData;

        // ---------------------------------------------------------------------------------
        // METHOD PUBLIC
        // -------------
        // /////////////////////////////////////////////////////////////////////////////////

        
        public override ActionData FunGetActionData() => m_actionData;

        /// <summary>
        ///     Cập nhật dữ liệu cho action. </summary>
        /// -------------------------------------------
        public void FunSetActionData(ActionData actionData)
        {
            if (actionData == null)
            {
                Debug.LogError("Action data is null. SetActionData failed.");
                return;
            }
            m_actionData = actionData;
            SetActionsForObjectRTS();
        }

        /// <summary>
        ///     Thêm một hành động action mới cho đối tượng. </summary>
        /// -----------------------------------------------------------
        public void FunAddAction(ActionComposite composite) => m_actionData.FunAddAction(composite);

        /// <summary>
        ///     Xóa một hành động action cho đối tượng. </summary>
        /// ------------------------------------------------------
        public void FunRemoveAction(ActionComposite composite) => m_actionData.FunRemoveAction(composite);

        // ---------------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thiết lập hành động cho đối tượng. </summary>
        /// -------------------------------------------------
        private void SetActionsForObjectRTS()
        {
            ObjectTypeBaseStateComp stateComp = null;
            if (m_actionData is ActionUnitDataSO)
            {
                stateComp = GetComponent<UnitStateComp>();
                DebugUtils.HandleErrorIfNullGetComponent<UnitStateComp, UnitActionComp>(stateComp, this, gameObject);
            }
            else if (m_actionData is ActionBuildingDataSO)
            {
                stateComp = GetComponent<BuildingStateComp>();
                DebugUtils.HandleErrorIfNullGetComponent<BuildingStateComp, UnitActionComp>(stateComp, this, gameObject);
            }

            var groupAction = m_actionData.FunGetRoot();
            SetActionsHelper(groupAction, stateComp);
        }

        private void SetActionsHelper(ActionCompositeGroup groupAction, ObjectTypeBaseStateComp stateComp)
        {
            foreach (var composite in groupAction.FunGetChild())
            {
                var typeAction = composite.FunGetAction()?.FunGetTypeAction();
                IObjectState newState = null;

                if (stateComp is UnitStateComp)
                {
                    newState = UnitStateHandler.FunCreate(typeAction, gameObject);
                    if (newState == null)
                        continue;
                }
                else if (stateComp is BuildingStateComp)
                {
                    newState = BuildingStateHandler.FunCreate(typeAction, gameObject);
                    if (newState == null)
                        continue;
                }
                stateComp.FunRegisterState(newState);

                if (composite is ActionCompositeGroup group)
                    SetActionsHelper(group, stateComp);
            }
        }
    }
}
