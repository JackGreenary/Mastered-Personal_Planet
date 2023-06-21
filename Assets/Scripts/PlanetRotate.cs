using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetRotate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //[SerializeField]
    //private CameraManager cameraManager;

    //private void OnMouseEnter()
    //{
    //    cameraManager.canRotate = true;
    //}

    //private void OnMouseExit()
    //{
    //    cameraManager.canRotate = false;
    //}

    public float rotationSpeed;
    public float rotationDamping;

    [SerializeField]
    private float _rotationVelocity;
    private bool _dragged;

    [SerializeField]
    private Transform centerPoint;

    

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rotationVelocity = eventData.delta.x * rotationSpeed;
        transform.Rotate(Vector3.up, -_rotationVelocity, Space.Self);
        if (_rotationVelocity > 5)
            _rotationVelocity = 5;
        if (_rotationVelocity < -5)
            _rotationVelocity = -5;
        //transform.Rotate(centerPoint.position, -_rotationVelocity, Space.Self);

        //transform.rotation = Quaternion.identity;
        //transform.RotateAround(transform.position + new Vector3(gameObject.GetWidth() / 2f, arrow.GetHeight() / 2f, 0f), Vector3.forward, angle);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragged = false;
    }

    private void Update()
    {
        if (!_dragged && !Mathf.Approximately(_rotationVelocity, 0))
        {
            float deltaVelocity = Mathf.Min(
                Mathf.Sign(_rotationVelocity) * Time.deltaTime * rotationDamping,
                Mathf.Sign(_rotationVelocity) * _rotationVelocity
            );
            _rotationVelocity -= deltaVelocity;
            transform.Rotate(Vector3.up, -_rotationVelocity, Space.Self);
            //transform.Rotate(centerPoint.position, -_rotationVelocity, Space.Self);
        }
    }
}
