using System;
using UnityEngine;
using UnityEngine.UI;

namespace NecroCore.UI.INGAME
{
    public class UI_BottomPanel : UI_InGameComponent, ILevelStateListener, ISelectionStateListener
    {
        [SerializeField] private Button button;

        private Animator ani;
        private bool IsShow = true;
        private static readonly int hide = Animator.StringToHash("Hide");
        private static readonly int show = Animator.StringToHash("Show");

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

        private void ShowOrHide(bool newShow)
        {
            if (ani == null || newShow == IsShow)// || ani.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f && !(ani.GetAnimatorTransitionInfo(0).normalizedTime > 0))
            {
                return;
            }         
            ani.SetTrigger(newShow ? show : hide);
            IsShow = newShow;
        }

        private void ShowOrHide()
        {
            ShowOrHide(!IsShow);
        }

        public void OnLevelStateEnd(Level.State state)
        {
            
        }

        public void OnLevelStateBegin(Level.State state)
        {
            if (state == Level.State.Fighting)
                ShowOrHide(false);
        }

        public void OnSelectionState(InputHandler.SelectionState state)
        {
            if(state == InputHandler.SelectionState.Dragging)
                ShowOrHide(false);
            else
                ShowOrHide(true);

        }
    }
}

