using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointer : MonoBehaviour
{
	private GameObject obj;
	private int onoff=0;
	private int re;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) 
		{
			this.transform.position += new Vector3(0,10 * Time.deltaTime,0);
		}
		if (Input.GetKey(KeyCode.DownArrow)) 
		{
			this.transform.position += new Vector3(0,-10 * Time.deltaTime,0);
		}
		if (Input.GetKey(KeyCode.RightArrow)) 
		{
			this.transform.position += new Vector3(10 * Time.deltaTime,0,0);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) 
		{
			this.transform.position += new Vector3(-10 * Time.deltaTime,0,0);
		}
		if (Input.GetKey(KeyCode.LeftShift)) 
		{
			this.transform.position += new Vector3(0,0,10 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.RightShift)) 
		{
			this.transform.position += new Vector3(0,0,-10 * Time.deltaTime);
		}
		
		if(onoff==1)
		{
			obj.transform.position=Vector3.MoveTowards(obj.transform.position,this.transform.position,1.0f);
		}
    }
	void OnTriggerEnter(Collider collision)
	{
		Debug.Log("衝突されたオブジェクト：" + collision.gameObject.name);
		GameObject gameobject=GameObject.Find("Program");
		float num=collision.gameObject.transform.position.x/10+collision.gameObject.transform.position.z/10*8;
		if(onoff==0)
		{
			if(collision.gameObject.name=="TT_Ballista_lvl2")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(0);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Heavy_Cavalry")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(1);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Mage")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(2);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Knight")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(3);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Mounted_King")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(4);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Mage (1)")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(5);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Heavy_Cavalry (1)")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(6);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Ballista_lvl2 (1)")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(7);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Swordman")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(8);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Swordman (1)")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(9);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Swordman (2)")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(10);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Swordman (3)")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(11);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Swordman (4)")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(12);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Swordman (5)")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(13);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Swordman (6)")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(14);
				obj=collision.gameObject;
				onoff=1;
			}
			else if(collision.gameObject.name=="TT_Swordman (7)")
			{
				re=(int)num;
				gameobject.GetComponent<ChessPieces>().OperateMove(15);
				obj=collision.gameObject;
				onoff=1;
			}
		}
		else
		{
			int xz=(int)num;
			Debug.Log(xz);
			if(xz==re)
			{
				onoff=0;
				gameobject.GetComponent<ChessPieces>().returnred();
				obj.transform.position=new Vector3((re%8)*10,0,(re/8)*10);
			}
			else if(gameobject.GetComponent<ChessPieces>().redjudge(xz)==true)
			{
				onoff=0;
				gameobject.GetComponent<ChessPieces>().MyMove(xz);
			}
		}
    }
}