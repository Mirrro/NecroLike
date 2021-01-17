using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AICreatures;

public class HealthBar : MonoBehaviour
{
    AIEntity entity;
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        entity = GetComponentInParent<AIEntity>();
        entity.hitEvent.AddListener(UpdateBar);
        entity.deathEvent.AddListener(delegate { gameObject.SetActive(false); }) ;
        slider.maxValue = entity.stats.health;
        UpdateBar();
    }
    private void Update()
    {
        Camera camera = Camera.main;

        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);

    }

    public void UpdateBar()
    {
        if (entity.stats.lostHealth == 0)
            gameObject.SetActive(false);
        else
        {
            gameObject.SetActive(true);
            slider.value = entity.stats.health - entity.stats.lostHealth;
        }
    }
}
