using System;
using UnityEngine;
using UnityEngine.UI;

namespace NecroCore.UI.INGAME
{
    public class UI_BottomPanel : UI_InGameComponent, ISelectionStateListener
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
            if (ani == null || newShow == IsShow)
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

        public void OnSelectionState(InputHandler.SelectionState state)
        {
            if(state == InputHandler.SelectionState.Dragging)
                ShowOrHide(false);
            else
                ShowOrHide(true);
        }
    }
}

