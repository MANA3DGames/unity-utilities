using System.Collections;
using System.Collections.Generic;
using MANA3DGames;
using UnityEngine;

public class Test : MonoBehaviour {

    public GameObject menu;

    public GameObject toBeMoved;
    public Transform target;

    UIMenu myMenu;

    // Use this for initialization
    void Start () 
    {

        myMenu = new UIMenu(menu);
        //Debug.Log(mmm.GetImageFillAmount("Image") );


        //mmm.SetDropDownOption( "Dropdown", new List<string> { "kkkk", "aaaaa" } );

        List<TMPro.TMP_Dropdown.OptionData> optionDatas = new List<TMPro.TMP_Dropdown.OptionData>();
        optionDatas.Add( new TMPro.TMP_Dropdown.OptionData("Mahmoud") );
        optionDatas.Add( new TMPro.TMP_Dropdown.OptionData("Mahmoud AbdElrahim") );
        optionDatas.Add( new TMPro.TMP_Dropdown.OptionData("Mahmoud AbdElrahim Naser") );
        optionDatas.Add( new TMPro.TMP_Dropdown.OptionData("Mahmoud AbdElrahim Naser Abu Obaid") );
        optionDatas.Add( new TMPro.TMP_Dropdown.OptionData("ADDDD????????") );
        optionDatas.Add( new TMPro.TMP_Dropdown.OptionData("aaaaa") );
        myMenu.SetDropDownOption( "Dropdown", optionDatas );

        //Debug.Log(mmm.Get("Image"));
        //mmm.Delete( "Image" );
        //Debug.Log(mmm.Get("Image") );

        CommonTween.Move( toBeMoved, toBeMoved.transform.position, target.position, 1.0f, 0.5f, ()=> { 
            CommonTween.Rotate( toBeMoved, toBeMoved.transform.rotation, target.rotation, 1.0f );
        }  );


        
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log( myMenu.GetDropDownTextValue("Dropdown") );
	}
}
