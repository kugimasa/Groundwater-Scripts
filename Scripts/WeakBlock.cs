using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakBlock : MonoBehaviour
{
    [SerializeField] float fallDuration = 2.0f;
    [SerializeField] float fallSpeed = 0.01f;
    [SerializeField] bool canFall = false;

    private void Update() {
        if (canFall)
        {
            transform.Translate(Vector3.down * fallSpeed, Space.World);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Person")
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDuration);
        canFall = true;
    }
}
