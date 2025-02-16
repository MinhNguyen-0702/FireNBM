using System.Collections.Generic;
using System.Linq;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public enum TypeHUD { Action };

    public class HUDManager : MonoBehaviour
    {
        private MessagingSystem m_messagingSystem;

        // -----------------------------------------------------------------------------
        // API UNITY
        // ---------
        // //////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            m_messagingSystem = MessagingSystem.Instance;
        }

        private void OnEnable()
        {
            m_messagingSystem.FunAttachListener(typeof(MessageUpdateObjectsHUD), OnUpdateObjectsHUD);
            m_messagingSystem.FunAttachListener(typeof(MessageDisableObjectsHUD), OnDisableObjectsHUD);
            m_messagingSystem.FunAttachListener(typeof(MessageDisplayObjectSelector), OnDisplayInfoObjectSelector);
        }

        private void OnDisable()
        {
            m_messagingSystem.FunDetachListener(typeof(MessageUpdateObjectsHUD), OnUpdateObjectsHUD);
            m_messagingSystem.FunDetachListener(typeof(MessageDisableObjectsHUD), OnDisableObjectsHUD);
            m_messagingSystem.FunDetachListener(typeof(MessageDisplayObjectSelector), OnDisplayInfoObjectSelector);
        }


        // -----------------------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        // //////////////////////////////////////////////////////////////////////////////

        // Cập nhật thông tin cá nhân cho đối tượng được chọn.
        // ---------------------------------------------------
        private void UpdateObjectSelectorHUD(TypeObjectRTS typeObject, GameObject objectRTS)
        {
            switch (typeObject)
            {
                case TypeObjectRTS.Unit:
                {
                    var unitActionComp = objectRTS.GetComponent<UnitActionComp>();
                    FunUpdateForActionHUD(unitActionComp.FunGetActionData());

                    var unitFlyweightComp = objectRTS.GetComponent<UnitFlyweightComp>();
                    m_messagingSystem.FunTriggerMessage(new MessageDisplayPortaitObjectHUD(unitFlyweightComp.FunGetNameUnit()), false);
                    m_messagingSystem.FunTriggerMessage(new MessageDisplayInfoObjectHUD(objectRTS, typeObject), false);
                    break;
                }
            
                case TypeObjectRTS.Building:
                {
                    var buildingActionComp = objectRTS.GetComponent<BuildingActionComp>();
                    FunUpdateForActionHUD(buildingActionComp.FunGetActionData());
                    break;
                }

                default:
                    break;
            }
        }

        // Cập nhật danh sách các đối tượng được chọn.
        // ------------------------------------------
        private void UpdateListObjectSelectorHUD(TypeObjectRTS typeObject, HashSet<GameObject> listObjectSelector, bool isDifferentObject)
        {
            if (typeObject != TypeObjectRTS.Unit)
                return;

            m_messagingSystem.FunTriggerMessage(new MessageDisableInfoObjectHUD(), false);
            m_messagingSystem.FunTriggerMessage(new MessageDisplayInfoButtonsDetails(listObjectSelector.ToList()), false);

            if (isDifferentObject == true)
                FunUpdateForActionHUD(StoringDataSystem.Instance.FunGetUnitActionDefault());
            else
            {
                var unitActionComp = listObjectSelector.First().GetComponent<UnitActionComp>();
                FunUpdateForActionHUD(unitActionComp.FunGetActionData());
            }

        }

        // Cập nhật các action của đối tượng khi nó được người chơi lựa chọn.
        // ------------------------------------------------------------------
        private void FunUpdateForActionHUD(ActionData actionData)
        {
            m_messagingSystem.FunTriggerMessage(new MessageDisplayActionHUD(actionData), false);
        }
        
        // -----------------------------------------------------------------------------
        // HANDLE MESSAGE
        // --------------
        // //////////////////////////////////////////////////////////////////////////////

        // Cập nhật các thông tin của các đối tượng trên màn hình HUD.
        // ----------------------------------------------------------
        private bool OnUpdateObjectsHUD(IMessage message)
        {
            var messageResult = message as MessageUpdateObjectsHUD;
            
            if (messageResult.ListObject.Count == 1)        
                UpdateObjectSelectorHUD(messageResult.TypeObject, messageResult.ListObject.First());
            else if (messageResult.ListObject.Count > 1)    
                UpdateListObjectSelectorHUD(messageResult.TypeObject, messageResult.ListObject, messageResult.IsDifferentObject);

            return true;
        }

        // Tắt tất cả các thông tin về đối tượng trong HUD.
        // -----------------------------------------------
        private bool OnDisableObjectsHUD(IMessage message)
        {
            m_messagingSystem.FunTriggerMessage(new MessageDisableActionHUD(), false);
            m_messagingSystem.FunTriggerMessage(new MessageDisableInfoObjectHUD(), false);
            m_messagingSystem.FunTriggerMessage(new MessageDisablePortraitObjectHUD(), false);
            m_messagingSystem.FunTriggerMessage(new MessageDisableInfoObjectsSelectorHUD(), false);
            return true;
        }

        private bool OnDisplayInfoObjectSelector(IMessage message)
        {
            var messageResult = message as MessageDisplayObjectSelector;
            UpdateObjectSelectorHUD(TypeObjectRTS.Unit, messageResult.ObjectSelector);
            return false;
        }
    }
}