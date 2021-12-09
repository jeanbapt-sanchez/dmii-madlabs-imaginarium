using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {
    private Vector3 offset;
    private Transform target;
    private Vector2 playerMouseInput;

    [SerializeField] private float CameraSpeed = 0.009f;
    //[SerializeField] private float Sensitivity = 10f;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    private void LateUpdate() {
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        Debug.Log(target.position);
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, CameraSpeed);

        // TODO smooth demi rotation
    }
}
