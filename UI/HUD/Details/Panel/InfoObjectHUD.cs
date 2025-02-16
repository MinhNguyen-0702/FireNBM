using FireNBM.Pattern;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FireNBM
{
    public class InfoObjectHUD : MonoBehaviour
    {
        [SerializeField] private Image m_avatar;
        [Space(5)]
        [SerializeField] private TextMeshProUGUI m_textName;
        [SerializeField] private TextMeshProUGUI m_textInfo;
        [Space(5)]
        [SerializeField] private TextMeshProUGUI m_textHp;
        [SerializeField] private TextMeshProUGUI m_textAttack;
        [SerializeField] private TextMeshProUGUI m_textRange;
        [SerializeField] private TextMeshProUGUI m_textSpeedAttack;
        [SerializeField] private TextMeshProUGUI m_textWalk;

        private UnitHeathComp m_healthComp;

        private void Awake()
        {
            if (m_avatar == null)
            {
                DebugUtils.FunLog("Lỗi thành phần Image chưa được gắn vào.");
                return;
            }
            if (!m_textHp || !m_textName || !m_textInfo)
            {
                DebugUtils.FunLog("Lỗi một trong những thành phần Text chưa được gắn vào.");
                return;
            }
        }

        private void OnDisable()
        {
            m_healthComp = null;
        }

        private void Update()
        {
            if (m_healthComp != null)
            {
                UpdateHp(m_healthComp.FunGetCurrentHP(), m_healthComp.FunGetMaxHP());

                if (m_healthComp.FunGetCurrentHP() == 0)
                    gameObject.SetActive(false);
            }
        }


        public void FunUpdateInfoObjectRTS(GameObject objRTS, TypeObjectRTS typeObject)
        {
            if (objRTS == null)
            {
                DebugUtils.FunLog("Lỗi, đối tượng muốn hiển thị thông tin trên bảng HUD không tồn tại.");
                return;
            }

            if (typeObject == TypeObjectRTS.Unit)
            {
                m_healthComp = objRTS.GetComponent<UnitHeathComp>();
                UpdateHp(m_healthComp.FunGetCurrentHP(), m_healthComp.FunGetMaxHP());

                var flyweightComp = objRTS.GetComponent<UnitFlyweightComp>();
                UpdateAvartar(flyweightComp.FunGetAvartar());
                UpdateName(flyweightComp.FunGetNameUnit().ToString());
                UpdateInfo(flyweightComp.FunGetInfo());

                var dataComp = objRTS.GetComponent<UnitDataComp>();
                UpdateAttack(dataComp.Attack);
                UpdateRange(dataComp.RangeAttack);
                UpdateSpeedAttack(dataComp.AttackSpeed);
                UpdateSpeedWalk(dataComp.WalkSpeed);
            }
            else if (typeObject == TypeObjectRTS.Building)
            {
                // TODO: Cập nhật sau.
            }
        }

        private void UpdateAvartar(Sprite avatar) => m_avatar.sprite = avatar;
        private void UpdateName(string name) => m_textName.text = name;
        private void UpdateInfo(string info) => m_textInfo.text = $"'Info: {info}'";

        private void UpdateHp(float currHp, float maxHp) => m_textHp.text = $"Hp: {((int)currHp).ToString()}/{((int)maxHp).ToString()}";
        private void UpdateAttack(float attack) => m_textHp.text = $"Attack: {attack}";
        private void UpdateRange(float range) => m_textHp.text = $"Range: {range}";
        private void UpdateSpeedAttack(float speed) => m_textHp.text = $"Speed Attack: {speed}";
        private void UpdateSpeedWalk(float walk) => m_textHp.text = $"Walk: {walk}";

    }
}