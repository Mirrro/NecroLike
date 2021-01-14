using UnityEngine;

namespace NecroCore.UI.INGAME
{
    public class UI_InGameComponent : MonoBehaviour
    {
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
