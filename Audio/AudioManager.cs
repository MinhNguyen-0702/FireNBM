using UnityEngine;

namespace FireNBM
{
    // TODO: 
    // Chứa danh sách các audio đang hoạt động.
    // Cần danh sách các unit đang có trong cảnh.
    // Cần luôn danh sách cách underConstruction đang được công nhân hoạt động.

    // Nếu các unit hay building nằm ngoài Camera thì nên tắt hết các trạng thái cập nhật của audio.

    // Nếu nằm trong thì cập nhật lại.

    // Khi cập nhật thì: Nếu đối tượng nào nằm trong phạm vi mà đã cập nhật trước thì thôi cập nhật.

    // Mỗi thành phần trong các đối tượng có tránh nhiệm tự cập nhật phạm vi âm thanh đối với Camera.

    // Khi under Construction được đặt thì cập nhật âm thanh luôn vì nó luôn nằm trong phạm của camera.

    public class AudioManager : MonoBehaviour
    {

        // Kiểm tra xem các unit hay building có nằm trong phạm vi của camera hay không. 
        private void LateUpdate()
        {
            
        }
    }
}