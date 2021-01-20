using System;
using System.Collections;
using UnityEngine;

namespace NecroCore.UI.INGAME
{
    public class UI_InGameComponent : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Spawn());
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual IEnumerator Spawn()
        {
            float timer = 0;
            gameObject.transform.localScale = Vector3.zero;
            while (gameObject.transform.localScale.magnitude < Vector3.one.magnitude)
            {
                gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer);
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
