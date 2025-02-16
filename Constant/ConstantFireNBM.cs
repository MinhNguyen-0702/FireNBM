namespace FireNBM
{
    public static class ConstantFireNBM
    {
        public const int MOUSE_LEFT = 0;
        public const int MOUSE_RIGHT = 1;

        public const int ONE_MEMBER = 1;                   
        public const float DRAG_THRESHOLD = 10f;      

        public const string UNIT = "Unit";
        public const string BUILDING = "Building";
        public const string UNDER_CONSTRUCTION = "UnderConstruction";

        public const string ENEMY = "Enemy";

        public const string BACKGROUND_UI = "Background_UI";

        public const float TARGET_PROXIMITY_THRESHOLD = 0.7f;
        public const float TARGET_REACHED_THRESHOLD = 0.5f;

        /// <summary>
        ///     Lượng công việc tối đa mà công nhân làm đc trong 1 lần
        /// </summary>
        public const int MAX_BUILD_PROGRESS = 20;      

        /// <summary>
        ///     Đảm bảo thời gian ko quá 1 khung hình (milliseconds (ms) - 1 frame - 0.01667s). 
        /// </summary>
        public const int MAX_QUEUE_PROCESSING_TIME = 16667; 
        
        // Tên của trục lăn chuột trong input manager.
        public static string MOUSE_SCROLL_AXIS = "Mouse ScrollWheel";  
        public static string MOUSE_X_AXIS = "Mouse X";  
    }
}