using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockup : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            UnitKnockup(100);
            Debug.Log("Should go up!?!?!?!");
        }
    }
    void UnitKnockup(float power)
    {
        rb.AddForce(new Vector3(0,power,0),ForceMode.Impulse);
    }

}
