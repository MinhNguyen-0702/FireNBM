using UnityEngine;

namespace FireNBM
{
    public class ButtonQuitStartGameHUD : MonoBehaviour
    {
        public void FunOnClickQuitStartGame()
        {
            StartGameHUD.Instace.FunHandleStartGame();
        }
    }
}