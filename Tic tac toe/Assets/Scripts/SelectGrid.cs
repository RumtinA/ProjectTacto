using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGrid : MonoBehaviour {
    public bool selected; //Checks for selection of space via click.
    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hitInfo = new RaycastHit2D();
            bool hit = Physics2D.Raycast(new Vector2(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).x, GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);

<<<<<<< HEAD:Tic tac toe/Assets/Scripts/SelectGrid.cs
          //  if (hit)
          //  {
                //if (!GameObject(Space).GetComponent<Spaces>().isTaken){

                      //  GameObject RedMarker = (GameObject)Instantiate(RedMarker, )

           //     }
=======
            
                
>>>>>>> origin/master:Tic tac toe/Assets/SelectGrid.cs

        //    }

          //  else
         //   {

        //    }
        }
	}

