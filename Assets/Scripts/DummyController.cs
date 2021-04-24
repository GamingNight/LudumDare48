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

    public void Pop(Vector3 pos)
    {
    	transform.position = pos;
    	gameObject.SetActive(true);
    }

    public void Depop()
    {
    				
    	gameObject.SetActive(false);
    }
}
