using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class DataSender : MonoBehaviour {
    private int numFreePlayer;
    private int numPayment1Player;
    private int numPayment2Player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ReceiveData () {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("Data");

        /* query.OrderByDescending ("Score"); */
        /* query.Limit = 5; */

        query.FindAsync ((List<NCMBObject> objList ,NCMBException e) => {
                if (e != null) {
                    Debug.Log( "failed" );
                } else {
                    Debug.Log( "success" );
                    Debug.Log( "objList.Count : " + objList.Count );
                    if( objList.Count == 0)
                    {
                        numFreePlayer = 0;
                        numPayment1Player = 0;
                        numPayment2Player = 0;
                        var ncmb_obj = new NCMBObject( "Data" );
                        SendData(ncmb_obj);
                    }
                    else
                    {
                        if( (objList[0])["numFreePlayer"] != null )
                        {
                            Debug.Log( "Find! numFreePlayer! " + (objList[0])["numFreePlayer"] );
                            numFreePlayer = System.Convert.ToInt32( (objList[0])["numFreePlayer"] );
                        }
                        SendData(objList[0]);
                    }

                    /* objList["numPlayer"] */
                }
                });
	}

	public void SendData( NCMBObject ncmb_obj ) {
        // オブジェクトに値を設定
        ncmb_obj["numFreePlayer"] = numFreePlayer + 1;
        ncmb_obj["numPayment1Player"] = numPayment1Player;
        ncmb_obj["numPayment2Player"] = numPayment2Player;
        // データストアへの登録
        ncmb_obj.SaveAsync();
	}
}
