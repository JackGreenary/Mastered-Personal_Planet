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
            return;

        Vector3 currentMousePosition = Input.mousePosition;
        Vector3 mouseDelta = currentMousePosition - initialMousePosition;

        float rotationX = mouseDelta.y * rotationSpeed * Time.deltaTime;
        float rotationY = -mouseDelta.x * rotationSpeed * Time.deltaTime;

        Quaternion newRotation = Quaternion.Euler(rotationX, rotationY, 0f) * initialRotation;
        centerPoint.rotation = newRotation;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        isDragging = false;
        initialRotation = centerPoint.rotation;
    }

}
