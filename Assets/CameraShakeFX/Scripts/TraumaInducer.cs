 using UnityEngine;
using System.Collections;

/* Example script to apply trauma to the camera or any game object */
public class TraumaInducer : MonoBehaviour 
{
    [Tooltip("Seconds to wait before trigerring the explosion particles and the trauma effect")]
    public float Delay = 1;
    [Tooltip("Maximum stress the effect can inflict upon objects Range([0,1])")]
    public float MaximumStress = 0.6f;
    [Tooltip("Maximum distance in which objects are affected by this TraumaInducer")]
    public float Range = 45;
    private bool canShakeCam;
    GameObject camera;


    private void Start()
    {
        camera = Camera.main.transform.parent.gameObject;
    }
    
    private IEnumerator ShakeCamera()
    {
        canShakeCam = false;
        Debug.Log("Enter camers shake");
        yield return new WaitForSeconds(Delay);


        camera = Camera.main.transform.parent.gameObject;

        var receiver = camera.GetComponent<StressReceiver>();
            float distance = Vector3.Distance(transform.position, camera.transform.position);
            float distance01 = Mathf.Clamp01(distance / Range);
            float stress = (1 - Mathf.Pow(distance01, 2)) * MaximumStress;
            receiver.InduceStress(stress);
    }

    private void Update()
    {
        if(canShakeCam)
        {
            StartCoroutine(ShakeCamera());
        }
    }
    
    public void setCanShakeCam(bool canShakeCam)
    {
        this.canShakeCam = canShakeCam;
    }

}