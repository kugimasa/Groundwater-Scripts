using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public float speed = 0.01f;
    private bool elevating = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update() {
        if (elevating) 
        {
            Elevation();
        }
    }
    public void Elevation()
    {
        transform.Translate(0.0f, speed, 0.0f);
    }

    public void WaterStop()
    {
        elevating = false;
    }
    // Start from StageController
    public IEnumerator StartEvelation(float duration)
    {
        yield return new WaitForSeconds(duration);
        elevating = true;
    }
}
