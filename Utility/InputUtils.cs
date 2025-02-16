using UnityEngine;

namespace FireNBM
{
    public static class InputUtils
    {
        /// <summary>
        ///     Lấy vị trí con trỏ chuột trong ko gian thế giới. </summary>
        /// ---------------------------------------------------------------
        public static Vector3 FunGetMouseWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 500) == true)
            {
                return new Vector3(hit.point.x, 0.0f, hit.point.z); 
            }
            return Vector3.zero;
        }
    }
}