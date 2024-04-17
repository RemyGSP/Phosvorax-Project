using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private float healthBarHeight;   // Start is called before the first frame update
    void Start()
    {
        GenerateHealthBar();
    }
    public void GenerateHealthBar()
    {
        GameObject aux = Instantiate(healthBar);
        aux.transform.parent = null;
        aux.AddComponent<FollowPlayer>().SetPlayer(this.gameObject);
        aux.GetComponent<FollowPlayer>().SetOffset(new Vector3(0,healthBarHeight,0));
        aux.AddComponent<LookToCamera>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
