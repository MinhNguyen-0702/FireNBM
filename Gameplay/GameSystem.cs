using FireNBM.Pattern;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FireNBM
{
    [AddComponentMenu("FireNBM/System/Game System")]
    public class GameSystem : Singleton<GameSystem>
    {
        private string m_strSceneMain;
        private SettingGame m_settingGame;
        public static GameSystem Instance { get { return InstanceSingleton; }}

        protected override void Awake()
        {
            base.Awake();

            m_settingGame = new SettingGame();
            m_settingGame.FunSetRacePlayer(TypeRaceRTS.Terran);
            m_settingGame.FunSetTimeLeft(600f); // 5p

            m_strSceneMain = "MainGame"; // TEST
        }

        public void FunLoadScene(string nameScene, LoadSceneMode mode) => SceneManager.LoadScene(nameScene, mode);
        public AsyncOperation FunLoadSceneAsync(string nameScene) => SceneManager.LoadSceneAsync(nameScene);
        public SettingGame FunGetSettingGame() => m_settingGame;
        public string FunGetStrSceneMain() => m_strSceneMain;
    }
}  