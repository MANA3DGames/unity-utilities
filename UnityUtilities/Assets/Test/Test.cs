using System.Collections;
using System.Collections.Generic;
using MANA3DGames;
using UnityEngine;

public class Test : MonoBehaviour {

    public GameObject fill;

    public GameObject toBeMoved;
    public Transform target;

	// Use this for initialization
	void Start () 
    {

        //UIMenu mmm = new UIMenu( fill );
        ////Debug.Log(mmm.GetImageFillAmount("Image") );


        //Debug.Log(mmm.Get("Image"));
        //mmm.Delete( "Image" );
        //Debug.Log(mmm.Get("Image") );

        CommonTween.Move( toBeMoved, toBeMoved.transform.position, target.position, 1.0f, 0.5f, ()=> { 
            CommonTween.Rotate( toBeMoved, toBeMoved.transform.rotation, target.rotation, 1.0f );
        }  );
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
