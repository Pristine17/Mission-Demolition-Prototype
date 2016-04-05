using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    static public FollowCam S;

    public bool _____________________________;

    public GameObject poi;
    public float easing = 0.05f;
    public float camZ;
    public Vector2 minXY;
    
   void Awake()
    {
        S = this;
        camZ = S.transform.position.z;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        Vector3 destination;

        if (poi == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = poi.transform.position;
            if (poi.tag == "Projectile")
            {
                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    poi = null;
                    return;
                }
            }
        }
            this.GetComponent<Camera>().orthographicSize = Mathf.Max(10f, destination.y + 10f);
            destination.x = Mathf.Max(minXY.x, destination.x);
            destination.y = Mathf.Max(minXY.y, destination.y);
            destination = Vector3.Lerp(transform.position, destination, easing);
            destination.z = camZ;
            S.transform.position = destination;
        
	}
}
