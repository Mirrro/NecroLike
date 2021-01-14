using System;
using System.ComponentModel.Design.Serialization;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace NecroCore.UI.INGAME
{
    public class UI_BottomPanel : UI_InGameComponent
    {
        [SerializeField] private Button button;

        private Animator ani;
        private bool IsShow = true;
        private static readonly int Hide = Animator.StringToHash("Hide");
        private static readonly int Show = Animator.StringToHash("Show");

        private void Awake()
        {
            UI_INGAME.Instance.EventGamePaused += OnPause;
        }

        private void Start()
        {
            ani = GetComponent<Animator>();
            button.onClick.AddListener(ShowOrHide);
        }

        private void OnPause(object sender, EventArgs args)
        {
            Debug.Log("GamePaused");
        }

        private void ShowOrHide()
        {
            if (ani == null || ani.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f && !(ani.GetAnimatorTransitionInfo(0).normalizedTime > 0))
            {
                return;
            }
            ani.SetTrigger(IsShow ? Hide : Show);
            IsShow = !IsShow;
        }
    }
}

