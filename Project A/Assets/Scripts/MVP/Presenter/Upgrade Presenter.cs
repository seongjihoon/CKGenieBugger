using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.Managers;
using CKProject.AttributeEditor;
using CKProject.Interactable;


namespace MVP.Upgrade
{
    public class UpgradePresenter : MonoBehaviour
    {
        public UpgradeModel upgradeModel;
        public UpgradeView upgradeView;

        private void Awake()
        {
            upgradeView.Initialized(GameManager.Instance, FoodManager.Instance);
        }

        // Getter
        // 업그레이드가 될 때마다 Manager과 View / Model에 업데이트 해주기.
        public void UpdateConponent()
        {

        }

        // 버튼을 누르면 업그레이드
        [VisibleEnum(typeof(EFoodType))]
        public void TempMethod(int foodType)
        {
            try
            {
                FoodManager.Instance.LevelUp((EFoodType)foodType);
                // UI에 업데이트 해주기
                upgradeView.UpdateUpgradeUIText((EFoodType)foodType, FoodManager.Instance.GetFoodLevelData((EFoodType)foodType));
            }
            catch
            {
#if UNITY_EDITOR
                Debug.Log("아직 개방되지 않은 행동");
#endif
            }
        }



    }

}