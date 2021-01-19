using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AICreatures;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();

        AIEntity entity = GetComponentInParent<AIEntity>();
        entity.hitEvent.AddListener(UpdateBar);
        entity.deathEvent.AddListener(delegate { gameObject.SetActive(false); }) ;
        slider.maxValue = entity.stats.health;
        UpdateBar(entity);
    }
    private void Update()
    {
        Camera camera = Camera.main;

        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);

    }

    public void UpdateBar(AIEntity entity)
    {
        if (entity.stats.lostHealth == 0 || entity.stats.lostHealth >= entity.stats.health)
            gameObject.SetActive(false);
        else
        {
            gameObject.SetActive(true);
            slider.value = entity.stats.health - entity.stats.lostHealth;
        }
    }
}
