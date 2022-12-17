using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget;
    public float speedFollow;
    public Vector3 offset;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTarget.position + offset, speedFollow * Time.deltaTime);
    }
}
