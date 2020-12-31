using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler
{
    private Vector3 startPosition;
    public float maxDistance;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Game.SetInput(new Vector2((transform.position.x - startPosition.x)/ maxDistance, (transform.position.y - startPosition.y)/ maxDistance));
        if (Input.GetMouseButtonUp(0) || Input.touchCount==1&&Input.GetTouch(0).phase==TouchPhase.Ended)
        {
            transform.position = startPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Vector3.MoveTowards(startPosition, Input.mousePosition, maxDistance);
    }
}
