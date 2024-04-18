using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifiyingObject : MonoBehaviour
{
    // Script para el material de zoom

    Renderer renderer;
    Camera cam;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.position);
        screenPoint.x = screenPoint.x / Screen.width;
        screenPoint.y = screenPoint.y / Screen.height;
        renderer.material.SetVector("_ObjectScreenPosition", screenPoint);
    }
}
