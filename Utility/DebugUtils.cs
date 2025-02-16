using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Một tiện ích giúp đơn giản hóa quá trình Debug.
    /// </summary>
    public static class DebugUtils
    {
        /// <summary>
        ///     Kiểm tra xem một thành phần (component) cụ thể có tồn tại hay không trên một GameObject.
        ///     Nếu không tìm thấy, ghi lỗi vào Unity Console (chỉ trong unity editor).   
        /// </summary>
        /// 
        /// <typeparam name="TO"> Loại của component mong đợi (Expected Componnet Type).</typeparam>
        /// <typeparam name="TS"> Loại của component hiện tại đang thực hiện kiểm tra (Source Component Type).</typeparam>
        /// 
        /// <param name="component"> Tham chiếu đến Component cần kiểm tra.</param>
        /// <param name="source"> Component hiện tại thực hiện kiểm tra (thường là "This").</param>
        /// <param name="onObject"> GameObject được mong đợi chứa Component loại <typeparamref name="TO"/>.</param>
        /// 
        /// <returns></returns>
        public static void HandleErrorIfNullGetComponent<TO, TS>(Component component, Component source, GameObject onObject)
        {
            #if UNITY_EDITOR
                if (component == null)
                {
                    Debug.LogError("ERROR: Component of type " + typeof(TS) + 
                                   " on GameObject " + source.gameObject.name + 
                                   " expected to find a component of type " + typeof(TO) + 
                                   " on GameObject " + onObject.name + ", but none were found.");
                }
            #endif
        }

        public static void FunLog(object message)
        {
            #if UNITY_EDITOR
                Debug.Log(message);
            #endif
        }

        public static void FunLogWarning(object message)
        {
            #if UNITY_EDITOR
                Debug.LogWarning(message);
            #endif
        }

        public static void FunLogError(object message)
        {
            #if UNITY_EDITOR
                Debug.LogError(message);
            #endif
        }

        public static void FunDrawLine(Vector3 start, Vector3 end, Color color)
        {
            #if UNITY_EDITOR
                Debug.DrawLine(start, end, color);
            #endif
        }
    }
}