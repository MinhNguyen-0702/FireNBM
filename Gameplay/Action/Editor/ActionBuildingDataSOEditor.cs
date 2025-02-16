using UnityEditor;
using FireNBM.Custom;

namespace FireNBM
{
    [CustomEditor(typeof(ActionBuildingDataSO))]
    public class ActionBuildingDataSOEditor : ActionDataEditor
    {
        public override void OnInspectorGUI()
        {
            GUIStyleCustom.Label.FunSetTitleScript("Action Building Data");

            base.FunInitialize();
            var actionBuildingData = (ActionBuildingDataSO)target;
            base.FunDisplayActionData(actionBuildingData);
            base.FunUpdateActionData(actionBuildingData, TypeRaceBuilding.None);
            base.FunApplyChangeActionData(actionBuildingData);
        }
    }
}