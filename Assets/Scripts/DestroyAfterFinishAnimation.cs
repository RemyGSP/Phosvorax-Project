using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterFinishAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_DestroyWhenFinished(gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length));
    }

    private IEnumerator _DestroyWhenFinished(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
