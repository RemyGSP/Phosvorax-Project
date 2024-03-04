using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CrystalDrop : MonoBehaviour
{
    [SerializeField] private GameObject objectToDrop;
    [SerializeField] private int dropAmount;
    [SerializeField] private int dropValue;
    [SerializeField] private Vector3 mindropForce;
    [SerializeField] private Vector3 maxdropForce;
    private Vector3 dropForce;
    [SerializeField] private float heightOffset;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 ClampForce(Vector3 value)
    {
        value.x = Mathf.Clamp(value.x, mindropForce.x, maxdropForce.x);
        value.y = Mathf.Clamp(value.y, mindropForce.y, maxdropForce.y);
        value.z = Mathf.Clamp(value.z, mindropForce.z, maxdropForce.z);
        return value;
    }
    public void Drop()
    {
        for (int i = 0; i <= dropAmount; i++)
        {
            Vector3 position = gameObject.transform.position;
            GameObject crystal = Instantiate(objectToDrop);
            dropForce.x = Random.Range(mindropForce.x,maxdropForce.x);
            dropForce.y = Random.Range(mindropForce.y,maxdropForce.y);
            dropForce.z = Random.Range(mindropForce.z,maxdropForce.z);
            position.y += heightOffset;
            crystal.transform.position = position;
            crystal.GetComponent<CrystalBehaviour>().SetCrystalValue(dropValue);
            crystal.GetComponent<Rigidbody>().AddTorque((Vector3.up - Vector3.right) * 10);
            crystal.GetComponent<Rigidbody>().AddForce(ClampForce(dropForce), ForceMode.Impulse);
        }
    }


}
