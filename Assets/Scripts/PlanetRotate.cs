using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetRotate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform centerPoint;
    public float rotationSpeed = 5f;

    private bool isDragging = false;
    private Vector3 initialMousePosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = centerPoint.rotation;
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - initialMousePosition;

            float rotationX = mouseDelta.y * rotationSpeed;
            float rotationY = -mouseDelta.x * rotationSpeed;

            Debug.Log($"mouseDelta: {mouseDelta}");
            Debug.Log($"rotationX: {rotationX}");
            Debug.Log($"rotationY: {rotationY}");

            //Quaternion.AngleAxis();

            Quaternion newRotation = Quaternion.Euler(rotationX, rotationY, 0f) * initialRotation;
            centerPoint.rotation = newRotation;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        isDragging = true;
        initialMousePosition = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
        {
            isDragging = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        isDragging = false;
        initialRotation = centerPoint.rotation;
    }

}
