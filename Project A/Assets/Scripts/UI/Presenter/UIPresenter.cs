using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

namespace CKProject.MVP
{
    public class UIPresenter : MonoBehaviour
    {
        protected UIModel uiModel;
        protected UiView uiView;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            uiView = GetComponent<UiView>();
            uiModel = GetComponent<UIModel>();
        }
    }
}
