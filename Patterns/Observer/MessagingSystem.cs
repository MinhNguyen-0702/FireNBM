using System.Collections.Generic;
using UnityEngine;

namespace FireNBM.Pattern
{
    /// <summary>
    ///     Một loại dữ liệu đại điện cho con trỏ hàm - hàm xử lý tin nhắn.</summary>
    /// -----------------------------------------------------------------------------
    public delegate bool MessageHandlerDelegate(IMessage message);


    /// <summary>
    ///     Triển khai hệ thống gửi và nhận tin nhắn toàn cầu. Sử dụng mẫu thiết kế Observer.
    /// </summary>
    public class MessagingSystem : Singleton<MessagingSystem>
    {   
        private Dictionary<string, HashSet<MessageHandlerDelegate>> m_listenerDict;     // Lưu trữ các hàm xử lý cho từng loại tin nhắn khác nhau.
        private Dictionary<string, HashSet<MessageHandlerDelegate>> m_detachListeners;  // Lưu trữ các thông điệp sẽ được xóa sau khi cập nhật xong.

        private Queue<IMessage> m_messageQueue;                                         // Lưu trữ tin nhắn mà chưa được xử lý.
        private System.Diagnostics.Stopwatch m_timer;                                   // Đo thời gian trôi qua trong quá trình xử lý hàng đợi.

        public static MessagingSystem Instance { get => InstanceSingleton; }



        // ---------------------------------------------------------------------------------
        // API UNITY 
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////
        
        protected override void Awake()
        {
            base.Awake();

            m_listenerDict = new Dictionary<string, HashSet<MessageHandlerDelegate>>();
            m_messageQueue = new Queue<IMessage>();
            m_timer = new System.Diagnostics.Stopwatch();
            m_detachListeners = new Dictionary<string, HashSet<MessageHandlerDelegate>>();       
        }

        private void Update()
        {
            // Xử lý các thông điệp được xếp hàng theo thời gian thực (fame)
            // Thoát nếu thời gian trong 1 khung vẫn chưa xử lý xong.
            m_timer.Start();
            while (m_messageQueue.Count > 0)
            {
                if (ConstantFireNBM.MAX_QUEUE_PROCESSING_TIME > 0)
                {  
                    if (m_timer.Elapsed.Milliseconds > ConstantFireNBM.MAX_QUEUE_PROCESSING_TIME)
                    {
                        m_timer.Stop();
                        return;
                    }
                }
                // Thực hiện cập nhật các hàm delegate đã đăng ký.
                IMessage message = m_messageQueue.Dequeue();
                if (FunTriggerMessage(message, false) == false)
                {
                    Debug.Log("Error when processing message.");
                }
            }
        }


        // ---------------------------------------------------------------------------------
        // FUNCTION PUBLIC 
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Đăng ký một listener cho một loại thông điệp nhất định.</summary>
        /// ---------------------------------------------------------------------
        public bool FunAttachListener(System.Type type, MessageHandlerDelegate handler)
        {
            if (type == null)
            {
                Debug.LogError("MessagingSystem: AttachListener failed due to having no message type specified!");
                return false;
            }

            string messageType = type.Name;
            if (m_listenerDict.ContainsKey(messageType) == false)
            {
                m_listenerDict.Add(messageType, new HashSet<MessageHandlerDelegate>());
            }

            // Thoát nếu nó đã tồn tại trong danh sách listener của loại thông điêp này.
            HashSet<MessageHandlerDelegate> listeners = m_listenerDict[messageType];
            if (listeners.Contains(handler) == true)
                return false;

            listeners.Add(handler);
            return true;
        }


        /// <summary>
        ///     Gỡ bỏ một listener đã đăng ký cho một loại thông điệp cụ thể.</summary>
        /// ----------------------------------------------------------------------------
        public bool FunDetachListener(System.Type type, MessageHandlerDelegate handler)
        {
            if (type == null)
            {
                Debug.LogError("MessagingSystem: DetachListener failed due to hanving no message type specified!");
                return false;
            }

            string messageType = type.Name;
            if (m_listenerDict.ContainsKey(messageType) == false)
                return false;

            // Thoát nếu trong danh sách ko có hành động listener.
            HashSet<MessageHandlerDelegate> listeners = m_listenerDict[messageType];
            if (listeners.Contains(handler) == false)
                return false;

            listeners.Remove(handler);
            return true;
        }

        /// <summary>
        ///     Thêm vào danh sách tạm thời, sẽ gỡ bỏ một listener đã đăng ký sau khi thông điệp được phát hành hoàn tất. </summary>
        /// -----------------------------------------------------------------------------------------------------------------------
        public bool FunDetachListenerLate(System.Type type, MessageHandlerDelegate handler)
        {
            if (type == null)
            {
                Debug.LogError("MessagingSystem: DetachListenerLate failed due to hanving no message type specified!");
                return false;
            }

            string messageType = type.Name;
            if (m_detachListeners.ContainsKey(messageType) == false)
            {
                m_detachListeners.Add(messageType, new HashSet<MessageHandlerDelegate>());
            }

            // Thoát nếu nó đã tồn tại trong danh sách listener của loại thông điêp này.
            HashSet<MessageHandlerDelegate> lateListeners = m_detachListeners[messageType];
            if (lateListeners.Contains(handler) == true)
            {
                return false;
            }

            lateListeners.Add(handler);
            return true;
        }


        /// <summary>
        ///     Thêm thông điệp vào hàng đợi để xử lý theo thứ tự.</summary>
        /// ---------------------------------------------------------------
        public bool FunQueueMessage(IMessage message)
        {
            if (m_listenerDict.ContainsKey(message.NameType) == false)
                return false;
            
            m_messageQueue.Enqueue(message);
            return true;
        }


        /// <summary>
        ///     Kích hoạt ngay lập tức thông điệp và gửi nó đến các listener.</summary>
        /// ---------------------------------------------------------------------------
        public bool FunTriggerMessage(IMessage message, bool notifyALl)
        {
            string messageType = message.NameType;
            if (m_listenerDict.ContainsKey(messageType) == false)
            {
                Debug.Log("MessagingSystem: Message '" + messageType + "' has no listeners!");
                return false;
            }
            
            // Lấy danh sách listener của loại thông điệp này.
            HashSet<MessageHandlerDelegate> listeners = m_listenerDict[messageType];
            foreach (var listener in listeners)
            {
                bool result = listener(message); // DynamicInvoke(message);

                // Dừng lại nếu ko cần thông báo tất cả và listener xử lý thành công.
                if (notifyALl == false && result == true)
                    return true;
            }

            // Kiểm tra xem có ai cần xóa thông điệp này sau khi cập nhật xong ko.
            if (m_detachListeners.ContainsKey(messageType) == true)
            {
                var detachListeners = m_detachListeners[messageType];
                foreach (var listener in detachListeners)
                    FunDetachListener(message.GetType(), listener);
  
                m_detachListeners.Remove(messageType);
            }
            return true;
        }
    }
}