using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Blue
{
    public class BonfireUIController : MonoBehaviour
    {
        public GameObject Panel; // 界面
        public Button BtnClose; // 关闭按钮
        public Transform RuleItemRoot; // 所有列表的父节点

        public BonfireRuleItemController RuleItemPrefab; // 原型Prefab模板

        public void Open()
        {
            Panel.SetActive(true);
            BtnClose.Select();

            Refresh();
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        private void Refresh()
        {
            BtnClose.Select(); // 刷新后重新选定

            // 清空所有子节点
            for (int i = RuleItemRoot.childCount - 1; i >= 0; i--)
            {
                Destroy(RuleItemRoot.GetChild(i).gameObject);
            }

            // 每次打开重新生成列表
            var bonfireSystem = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>();
            foreach (var bonfireRule in bonfireSystem.Rules
                    .Where(rule => !rule.Unlocked && rule.VisibleCondition(rule as AbstractBonfireRule)))
            {
                var ruleItem = Instantiate(RuleItemPrefab, RuleItemRoot);
                ruleItem.SetData(bonfireRule);
                ruleItem.BtnUnlock.onClick.AddListener(Refresh);
            }
        }

        public void Close()
        {
            Panel.SetActive(false);
            BtnClose.Select();
        }
    }
}