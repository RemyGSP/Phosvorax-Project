using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;

public class Prueba : MonoBehaviour
{
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject prefab;
    void Start()
    {


        Rect rec = new Rect(0, 0, 100, 100);

        image.GetComponent<RawImage>().texture = AssetPreview.GetAssetPreview(prefab);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
