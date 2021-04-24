using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pop(Vector3 pos)
    {
    	gameObject.SetActive(true);
    	transform.position = pos;
    }

    public void depop()
    {
    				
    	gameObject.SetActive(false);
    }
}
