using UnityEngine;
using UnityEngine.UI;

namespace Blue
{
    public class BonfireRuleItemController : MonoBehaviour
    {
        public Text DisplayName;
        public Button BtnUnlock;
        public GameObject NotEnough;
        public Text Price;

        private IBonfireRule mData;

        public void SetData(IBonfireRule bonfireRule)
        {
            mData = bonfireRule;

            this.BtnUnlock.onClick.AddListener(() => { mData.Unlock(); });
            this.gameObject.SetActive(true);

            UpdataView();
        }

        private void UpdataView()
        {
            this.DisplayName.text = mData.DisplayName;
            this.Price.text = "价格" + mData.NeedSeconds + "s 寿命";

            if(Bonfire.RemainSeconds>=mData.NeedSeconds)
            {
                BtnUnlock.gameObject.SetActive(true);
                NotEnough.gameObject.SetActive(false);
            }
            else
            {
                BtnUnlock.gameObject.SetActive(false);
                NotEnough.gameObject.SetActive(true);
            }
        }
        private void OnDestroy()
        {
            mData = null;
        }
    }
}