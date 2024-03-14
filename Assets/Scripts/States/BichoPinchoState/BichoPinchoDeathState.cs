using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/BichoPinchoDeath")]
public class BichoPinchoDeathState : States
{
    public BichoPinchoDeathState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override void OnExitState()
    {
        throw new System.NotImplementedException();
    }






    // Start is called before the first frame update
    public override void Start()
    {
        stateGameObject.GetComponent<CrystalDrop>().Drop();
        Destroy(stateGameObject);
    }

    public override void Update()
    {
        return;
    }
}
