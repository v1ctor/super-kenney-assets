using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
	public Transform target;
    public int limit;

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < limit && transform.position.x <= target.position.x) {
            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
        }  
    }
}
