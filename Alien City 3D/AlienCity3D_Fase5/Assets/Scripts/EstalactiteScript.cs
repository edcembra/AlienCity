using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstalactiteScript : MonoBehaviour
{
    public float speed;
    private bool fall = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fall == true)
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fall = true;
        }
    }


}
