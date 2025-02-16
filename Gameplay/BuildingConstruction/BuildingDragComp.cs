using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Cung cấp tính năng kéo và thả một đối tượng trong không gian 3D bằng cách dùng chuột. 
    /// </summary>
    [AddComponentMenu("FireNBM/Building/Building Drag Comp")]
    public class BuildingDragComp : MonoBehaviour
    {
        private Vector3 m_posPlaceable;

        private void Update()
        {
            Vector3 pos = InputUtils.FunGetMouseWorldPosition();
            m_posPlaceable = BuildingSystem.Instance.FunSnapToGrid(pos);
            transform.position = m_posPlaceable;
        }

        public Vector3 FunGetPosPlaceableBuilding() => m_posPlaceable;
    }
}





// Vector3 pos = InputUtils.FunGetMouseWorldPosition() + m_mouseOffset;

// Lưu trữ khoảng cách giữa vị trí chuột khi nhấn và đối tượng.
// private Vector3 m_mouseOffset;

// public void FunSetMouseOffset()
// {
//     // Tính toán lại mouseOffset khi người chơi nhấn chuột.
//     m_mouseOffset = transform.position - InputUtils.FunGetMouseWorldPosition();
// }