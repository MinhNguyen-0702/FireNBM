using UnityEngine;

namespace FireNBM
{
    public class UnitEnemyComp : MonoBehaviour
    {
        private Vector3 m_moveTo;

        private void Start()
        {
            var stateComp = gameObject.GetComponent<UnitStateComp>();
            stateComp.FunChangeState(TypeRaceUnitBase.Patrol);

            var constrollerComp = gameObject.GetComponent<UnitControllerComp>();
            constrollerComp.FunSetNewDestination(m_moveTo);
        }

        public void FunSetDataEnemy(Vector3 moveTo) => m_moveTo = moveTo;


        // Đầu tiên phải spawn quái vật.

        // Thiết lập trạng thái tuần tra cho quái., tấn công.

        // Người dùng cung cấp vị trí tuần cho cho quái.

        // Cung cấp vị trí được lấy từ đối tượng Cube,
        // Xong thì xóa đối tượng ý đi.

        // Cấp độ 1:
        //      1 - Quái đi 1 mình.
        //      2 - Quái đi với số lượng 2.
        //      3 - Quái đi với số lượng 3.
        //      4 - Quái đi với số lượng 5.

        // Map: cần tiêu diệt 15 con quái trong 10p.

        // Cấp độ 2: 
        //      Từ 1 vị trí. được nhô lên.
        //      10 con quái bắt đầu tấn công trụ chính bên mình.
        //      Nếu Trụ chính bị phá hủy thì thua.
        //      Nếu tiêu diệt hết quái thì thắng.
    }
}