using UnityEditor;
using FireNBM.Custom;

namespace FireNBM
{
    [CustomEditor(typeof(ActionUnitDataSO)), CanEditMultipleObjects]
    public class ActionUnitDataSOEditor : ActionDataEditor
    {
        public override void OnInspectorGUI()
        {
            GUIStyleCustom.Label.FunSetTitleScript("Action Unit Data");

            var actionUnitData = (ActionUnitDataSO)target;
            base.FunInitialize();
            base.FunDisplayActionData(actionUnitData);
            base.FunUpdateActionData(actionUnitData, TypeRaceUnit.None);
            base.FunApplyChangeActionData(actionUnitData);
        }
    }
}