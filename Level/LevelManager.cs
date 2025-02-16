using UnityEngine;
using UnityEngine.SceneManagement;

namespace FireNBM
{
    /// <summary>
    ///     Thực hiện công việc tải scene HUI vào scene chính.
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_miniMapCameraPrefab;

        private void Start()
        {
            // Báo lỗi nếu thành viên của lớp chưa được tham chiếu
            if (m_miniMapCameraPrefab == null)
            {
                Debug.LogError("In LevelManager, member m_miniMapCameraPrefab is NULL.");
                return;
            }

            // Tạo bản sao nếu không bị lỗi.
            Instantiate(m_miniMapCameraPrefab);
            // Tải scene HUD vào scene chính của chúng ta.
            SceneManager.LoadScene("HUD_Terrain", LoadSceneMode.Additive);
        }
    }
}