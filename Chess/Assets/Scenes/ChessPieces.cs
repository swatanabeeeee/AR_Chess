using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChessPieces : MonoBehaviour
{
  /*H	盤面
	G	56~   ~63
	F	.
	E	.
	D	.
	C	.
	B	
	A   0~    ~7	*/
	public GameObject[] boards;//盤面、左下から右に0~7、それが8行
	public Material[] materials;//盤面のマテリアル、0:赤、1:白、2:黒
	public GameObject[] CPU;//対戦相手
	public GameObject[] MY;//自分
	private List <int> PositionList1 = new List<int>();//駒の位置を入れておくリスト
	private List <int> PositionList2 = new List<int>();//相手駒の位置を入れておくリスト
	private int check=0;//チェックされているかどうか
	private List <int> RedList = new List<int>();//選択可能リスト
	private int moveflag=0;
	private int operateflag=0;
	private int selectchara;
	private Vector3 direction = new Vector3(0, 0, 0);
	public static int go=0;
    
    void Start()
    {
		//PositionListの要素の参照先
		//0:rook1
		//1:knight1
		//2:bishop1
		//3:queen
		//4:king
		//5:bishop2
		//6:knight2
		//7:rook2
		//8~15:pawn1~8
		for(int i=0;i<16;i++)//初期位置設定
		{
			PositionList1.Add(i);
			PositionList2.Add(63-i);
		}
		//PositionList1[3]=28;
		//PositionList2[1]=25;
		//bishopselect(1,2);
		//kingselect(1);
		//pawnselect(0,3);
		//pawnselect(1,2);
		//knightselect(1,1);
		//queenselect(0);
		//CpuRandomMove();
		//rookselect(1,1);
		//red();
    }
	void Update()
	{
		if(moveflag==1)
		{
			CPU[selectchara].transform.position=Vector3.MoveTowards(CPU[selectchara].transform.position,direction,1.0f);
			if(CPU[selectchara].transform.position==direction)
			{
				moveflag=0;
				WarResult(1);
				returnred();
			}
		}
		if(operateflag==1)
		{
			MY[selectchara].transform.position=Vector3.MoveTowards(MY[selectchara].transform.position,direction,1.0f);
			if(MY[selectchara].transform.position==direction)
			{
				operateflag=0;
				WarResult(0);
				returnred();
				CpuRandomMove2();
			}
		}
	}
	private void WarResult(int w)
	{
		if(w==1)
		{
			int i=PositionList1.IndexOf(PositionList2[selectchara]);
			if(i!=-1)
			{
				CPU[selectchara].gameObject.transform.Translate(-5,0,0);
				MY[i].gameObject.transform.Translate(-5,0,0);
				MY[i].transform.Rotate(new Vector3(0, 90, 0));
				CPU[selectchara].transform.Rotate(new Vector3(0, 90, 0));
				//アニメーション相手が勝ち、自分負けーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
				CPU[selectchara].transform.Rotate(new Vector3(0, -90, 0));
				CPU[selectchara].gameObject.transform.Translate(5,0,0);
				MY[i].SetActive(false);
				PositionList1[i]=100;
			}
		}
		else
		{
			int i=PositionList2.IndexOf(PositionList1[selectchara]);
			if(i!=-1)
			{
				CPU[i].gameObject.transform.Translate(-5,0,0);
				MY[selectchara].gameObject.transform.Translate(-5,0,0);
				CPU[i].transform.Rotate(new Vector3(0, 90, 0));
				MY[selectchara].transform.Rotate(new Vector3(0, 90, 0));
				//アニメーション自分勝ち、相手負けーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
				MY[selectchara].transform.Rotate(new Vector3(0, -90, 0));
				MY[selectchara].gameObject.transform.Translate(5,0,0);
				CPU[i].SetActive(false);
				PositionList2[i]=100;
			}
		}
	}
	
	/*void OnTriggerEnter(Collider collision)
	{
		ChessPieces.go=0;
		GameObject gameobject=GameObject.Find("Program");
		float num=collision.gameObject.transform.localPosition.x/10+collision.gameObject.transform.localPosition.z/10*8;
		int xz=(int)num;
		Debug.Log(xz);
		if(gameobject.GetComponent<ChessPieces>().redjudge(xz)==true)
		{
			ChessPieces.go=1;
		}
	}*/
	/*public void Go()
	{
		if(ChessPieces.go==1)
		{
			gameobject.GetComponent<ChessPieces>().MyMove(xz);
		}
	}*/
	
	public int getX(int PositionNum)//番号からX座標取得
	{
		return (PositionNum%8)*10;
	}
	public int getZ(int PositionNum)//番号からZ座標取得
	{
		return (PositionNum/8)*10;
	}
	
	public bool redjudge(int i)
	{
		return RedList.Contains(i);
	}
	public void MyMove(int xz)
	{
		Debug.Log(xz);
		direction.x = getX(xz); 
		direction.z = getZ(xz); 
		PositionList1[selectchara]=xz;
		operateflag=1;
	}
	
	public void OperateMove(int i)
	{
		returnred();
		if(operateflag==0)
		{
		if(i>=8)
		{
			pawnselect(0,i-7);
		}
		else if(i==0)
		{
			rookselect(0,1);
		}
		else if(i==1)
		{
			knightselect(0,1);
		}
		else if(i==2)
		{
			bishopselect(0,1);
		}
		else if(i==3)
		{
			queenselect(0);
		}
		else if(i==4)
		{
			kingselect(0);
		}
		else if(i==5)
		{
			bishopselect(0,2);
		}
		else if(i==6)
		{
			knightselect(0,2);
		}
		else if(i==7)
		{
			rookselect(0,2);
		}
		selectchara=i;
		red();
		/*if(RedList.Count!=0)
		{
		System.Random rnd = new System.Random();
		int xz=RedList[rnd.Next(RedList.Count)];
		Debug.Log(xz);
		direction.x = getX(xz); 
		direction.z = getZ(xz); 
		PositionList1[i]=xz;
		selectchara=i;
		operateflag=1;
		red();
		}*/
		}
	}
	
	public void CpuRandomMove()
	{
		if(moveflag==0)
		{
		List <int> rndlist = new List<int>();
		System.Random rnd = new System.Random();
		for(int i=0;i<16;i++)
		{
			int r;
			do
			{
				r=rnd.Next(16);
			}
			while(rndlist.Contains(r)==true);
			rndlist.Add(r);
		}
		int n=0;
		int i2;
		do
		{
			i2=rndlist[n];
			if(i2>=8)
			{
				pawnselect(1,i2-7);
			}
			else if(i2==0)
			{
				rookselect(1,1);
			}
			else if(i2==1)
			{
				knightselect(1,1);
			}
			else if(i2==2)
			{
				bishopselect(1,1);
			}
			else if(i2==3)
			{
				queenselect(1);
			}
			else if(i2==4)
			{
				kingselect(1);
			}
			else if(i2==5)
			{
				bishopselect(1,2);
			}
			else if(i2==6)
			{
				knightselect(1,2);
			}
			else if(i2==7)
			{
				rookselect(1,2);
			}
			n++;
		}
		while((RedList.Count==0)&&(n<=15));
		if(n==16)
		{
			Debug.Log("チェックメイト");
		}
		else
		{
			int xz=RedList[rnd.Next(RedList.Count)];
			Debug.Log(xz);
			direction.x = getX(xz); 
			direction.z = getZ(xz); 
			PositionList2[i2]=xz;
			selectchara=i2;
			moveflag=1;
			red();
		}
		}
	}
	public void CpuRandomMove2()
	{
		if(moveflag==0)
		{
		//ランダムな数のリストを生成
		List <int> rndlist = new List<int>();
		System.Random rnd = new System.Random();
		for(int i=0;i<16;i++)
		{
			int r;
			do
			{
				r=rnd.Next(16);
			}
			while(rndlist.Contains(r)==true);
			rndlist.Add(r);
		}
		int n=0;
		int i2=0;
		int xz=0;
		while(n<16)
		{
			RedList.Clear();
			do
			{
				i2=rndlist[n];
				if(i2>=8)
				{
					pawnselect(1,i2-7);
				}
				else if(i2==0)
				{
					rookselect(1,1);
				}
				else if(i2==1)
				{
					knightselect(1,1);
				}
				else if(i2==2)
				{
					bishopselect(1,1);
				}
				else if(i2==3)
				{
					queenselect(1);
				}
				else if(i2==4)
				{
					kingselect(1);
				}
				else if(i2==5)
				{
					bishopselect(1,2);
				}
				else if(i2==6)
				{
					knightselect(1,2);
				}
				else if(i2==7)
				{
					rookselect(1,2);
				}
				n++;
			}
			while((RedList.Count==0)&&(n<=15));
			int tt=RedList.Count;
			foreach(int t in RedList)
			{
				if(PositionList1.Contains(t)==true)
				{
					xz=t;
					break;
				}
				tt--;
			}
			if(tt!=0)
			{
				break;
			}
		}
		if(n>=16)
		{
			RedList.Clear();
			CpuRandomMove();
		}
		else
		{
			Debug.Log(xz*-1);
			direction.x = getX(xz); 
			direction.z = getZ(xz); 
			PositionList2[i2]=xz;
			red();
			selectchara=i2;
			moveflag=1;
		}
		}
	}
	
	public bool checkjudge(int c)//チェックしているかどうか,0:自分がしている　1:自分がされている
	{
		pawnselect(c,1);
		pawnselect(c,2);
		pawnselect(c,3);
		pawnselect(c,4);
		pawnselect(c,5);
		pawnselect(c,6);
		pawnselect(c,7);
		pawnselect(c,8);
		kingselectpre(c);
		knightselect(c,1);
		knightselect(c,2);
		rookselect(c,1);
		rookselect(c,2);
		bishopselect(c,1);
		bishopselect(c,2);
		queenselect(c);
		List <int> PositionList;
		if(c==0)
		{
			PositionList=PositionList2;
		}
		else
		{
			PositionList=PositionList1;
		}
		
		if(RedList.Contains(PositionList[4])==true)
		{
			RedList.Clear();
			return true;
		}
		RedList.Clear();
		return false;
	}
	
	//選択したところを赤くする////////////////////////////////////////////////////////////////////////
	public void red()
	{
		foreach(int i in RedList)
		{
			boards[i].GetComponent<Renderer>().sharedMaterial=materials[0];
		}
	}
	public void returnred()
	{
		foreach(int i in RedList)
		{
			if((i/8)%2==0)
			{
				if(i%2==0)
				{
					boards[i].GetComponent<Renderer>().sharedMaterial=materials[2];
				}
				else
				{
					boards[i].GetComponent<Renderer>().sharedMaterial=materials[1];
				}
			}
			else
			{
				if(i%2==0)
				{
					boards[i].GetComponent<Renderer>().sharedMaterial=materials[1];
				}
				else
				{
					boards[i].GetComponent<Renderer>().sharedMaterial=materials[2];
				}
			}
		}
		RedList.Clear();
	}
	
	public void kingselect(int c)
	{
		kingselectpre(c);
		List <int> kingredlist=new List<int>();
		List <int> deletelist=new List<int>();
		foreach(int i in RedList)
		{
			kingredlist.Add(i);
		}
		RedList.Clear();
		if(c==0)
		{
			int temp=PositionList1[4];
			foreach(int i in kingredlist)
			{
				PositionList1[4]=i;
				if(checkjudge(1)==true)
				{
					deletelist.Add(i);
				}
			}
			PositionList1[4]=temp;
		}
		else
		{
			int temp=PositionList2[4];
			foreach(int i in kingredlist)
			{
				PositionList2[4]=i;
				if(checkjudge(0)==true)
				{
					deletelist.Add(i);
				}
			}
			PositionList2[4]=temp;
		}
		foreach(int i in deletelist)
		{
			kingredlist.Remove(i);
		}
		RedList=kingredlist;
	}
	
	public void pawnselect(int c,int s)//自分か相手か 0:自分、1:相手、sにどのポーンか入れる1~8
	{
		List <int> PositionList;
		List <int> EnemyPositionList;
		if(c==0)
		{
			PositionList=PositionList1;
			EnemyPositionList=PositionList2;
			int pawn=PositionList[s+7];
			if(pawn!=100)
			{
				if(((EnemyPositionList.Contains(pawn+7)==true)&&(pawn%8==7))||((EnemyPositionList.Contains(pawn+9)==true)&&(pawn%8==0)))
				{
					if(pawn%8==0)
					{
						RedList.Add(pawn+9);
					}
					else if(pawn%8==7)
					{
						RedList.Add(pawn+7);
					}
					
				}
				else if(((EnemyPositionList.Contains(pawn+7)==true)||(EnemyPositionList.Contains(pawn+9)==true))&&(pawn%8!=0)&&(pawn%8!=7))
				{
					if(EnemyPositionList.Contains(pawn+7)==true)
					{
						RedList.Add(pawn+7);
						
					}
					if(EnemyPositionList.Contains(pawn+9)==true)
					{
						RedList.Add(pawn+9);
					}
				}
				
				if(EnemyPositionList.Contains(pawn+8)==true)
				{
					//目の前にいたらおわる
				}
				else if(pawn==s+7)
				{
					if(PositionList.Contains(pawn+8)==false)
					{
						RedList.Add(pawn+8);
					}
					if((PositionList.Contains(pawn+16)==false)&&(EnemyPositionList.Contains(pawn+16)==false)&&(PositionList.Contains(pawn+8)==false))
					{
						RedList.Add(pawn+16);
					}
				}
				else if((0<=pawn+8)&&(pawn+8<=63)&&(PositionList.Contains(pawn+8)==false))
				{
					RedList.Add(pawn+8);
				}
			}
		}
		else
		{
			PositionList=PositionList2;
			EnemyPositionList=PositionList1;
			int pawn=PositionList[s+7];
			if(pawn!=100)
			{
				if(((EnemyPositionList.Contains(pawn-7)==true)&&(pawn%8==0))||((EnemyPositionList.Contains(pawn-9)==true)&&(pawn%8==7)))
				{
					if(pawn%8==7)
					{
						RedList.Add(pawn-9);
					}
					else if(pawn%8==0)
					{
						RedList.Add(pawn-7);
					}	
				}
				else if(((EnemyPositionList.Contains(pawn-7)==true)||(EnemyPositionList.Contains(pawn-9)==true))&&(pawn%8!=0)&&(pawn%8!=7))
				{
					if(EnemyPositionList.Contains(pawn-7)==true)
					{
						RedList.Add(pawn-7);
					}
					if(EnemyPositionList.Contains(pawn-9)==true)
					{
						RedList.Add(pawn-9);
					}
				}
				
				if(EnemyPositionList.Contains(pawn-8)==true)
				{
					//目の前にいたらおわる
				}
				else if(pawn==(63-(s+7)))
				{
					if(PositionList.Contains(pawn-8)==false)
					{
						RedList.Add(pawn-8);
					}
					if(PositionList.Contains(pawn-16)==false&&(EnemyPositionList.Contains(pawn-16)==false)&&(PositionList.Contains(pawn-8)==false))
					{
						RedList.Add(pawn-16);
					}
				}
				else if((0<=pawn-8)&&(pawn-8<=63)&&(PositionList.Contains(pawn-8)==false))
				{
					RedList.Add(pawn-8);
				}
			}
		}
	}
	
	public void kingselectpre(int c)//kingが移動できる範囲を赤くする。
	{
		List <int> PositionList;
		if(c==0)
		{
			PositionList=PositionList1;
		}
		else
		{
			PositionList=PositionList2;
		} 
		int king=PositionList[4];//位置取得
		//上下
		if((0<=king+8)&&(king+8<=63)&&(PositionList.Contains(king+8)==false))
		{
			RedList.Add(king+8);
		}
		if((0<=king-8)&&(king-8<=63)&&(PositionList.Contains(king-8)==false))
		{
			RedList.Add(king-8);
		}

		//左端にいる場合を除く
		if(king%8!=0)
		{
			//左側
			if((0<=king+7)&&(king+7<=63)&&(PositionList.Contains(king+7)==false))
			{
				RedList.Add(king+7);
			}
			if((0<=king-1)&&(king-1<=63)&&(PositionList.Contains(king-1)==false))
			{
				RedList.Add(king-1);
			}
			if((0<=king-9)&&(king-9<=63)&&(PositionList.Contains(king-9)==false))
			{
				RedList.Add(king-9);
			}
		}
		
		//右端にいる場合を除く
		if(king%8!=7)
		{
			//右側
			if((0<=king+9)&&(king+9<=63)&&(PositionList.Contains(king+9)==false))
			{
				RedList.Add(king+9);
			}
			if((0<=king+1)&&(king+1<=63)&&(PositionList.Contains(king+1)==false))
			{
				RedList.Add(king+1);
			}
			if((0<=king-7)&&(king-7<=63)&&(PositionList.Contains(king-7)==false))
			{
				RedList.Add(king-7);
			}
		}
	}
	
	public void knightselect(int c,int s)//２つのうちどっちか
	{
		int knight;
		List <int> PositionList;
		if(c==0)
		{
			PositionList=PositionList1;
		}
		else
		{
			PositionList=PositionList2;
		}
		if(s==1)
		{
			knight=PositionList[1];
		}
		else
		{
			knight=PositionList[6];
		}
		//上
		if((0<=knight+17)&&(knight+17<=63)&&(PositionList.Contains(knight+17)==false))//端の場合
		{
			if(knight%8!=7)
			{
				RedList.Add(knight+17);
			}
			
		}
		if((0<=knight+15)&&(knight+15<=63)&&(PositionList.Contains(knight+15)==false))
		{
			if(knight%8!=0)
			{
				RedList.Add(knight+15);
			}
		}
		//下
		if((0<=knight-17)&&(knight-17<=63)&&(PositionList.Contains(knight-17)==false))
		{
			if(knight%8!=0)
			{
				RedList.Add(knight-17);
			}
		}
		if((0<=knight-15)&&(knight-15<=63)&&(PositionList.Contains(knight-15)==false))
		{
			if(knight%8!=7)
			{
				RedList.Add(knight-15);
			}
		}
		//右
		if((knight%8!=6)&&(knight%8!=7))
		{
			if((0<=knight+10)&&(knight+10<=63)&&(PositionList.Contains(knight+10)==false))
			{
				RedList.Add(knight+10);
			}
			if((0<=knight-6)&&(knight-6<=63)&&(PositionList.Contains(knight-6)==false))
			{
				RedList.Add(knight-6);
			}
		}
		//左
		if((knight%8!=0)&&(knight%8!=1))
		{
			if((0<=knight-10)&&(knight-10<=63)&&(PositionList.Contains(knight-10)==false))
			{
				RedList.Add(knight-10);
			}
			if((0<=knight+6)&&(knight+6<=63)&&(PositionList.Contains(knight+6)==false))
			{
				RedList.Add(knight+6);
			}
		}
	}

	public void rookselect(int c,int s)//２つのうちどっちか
	{
		int rook;
		List <int> PositionList;
		List <int> EnemyPositionList;
		if(c==0)
		{
			PositionList=PositionList1;
			EnemyPositionList=PositionList2;
		}
		else
		{
			PositionList=PositionList2;
			EnemyPositionList=PositionList1;
		}
		if(s==1)
		{
			rook=PositionList[0];
		}
		else
		{
			rook=PositionList[7];
		}
		//上
		for(int i=rook+8;i<=63;i=i+8)
		{
			if(((rook+8)!=i)&&(EnemyPositionList.Contains(i-8)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
		//下
		for(int i=rook-8;i>=0;i=i-8)
		{
			if(((rook-8)!=i)&&(EnemyPositionList.Contains(i+8)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
		//右
		for(int i=rook+1;i%8!=0;i++)
		{
			if(((rook+1)!=i)&&(EnemyPositionList.Contains(i-1)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
	
		//左
		for(int i=rook-1;i%8!=7;i--)
		{
			if(((rook-1)!=i)&&(EnemyPositionList.Contains(i+1)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
	}
	
	public void bishopselect(int c,int s)//２つのうちどっちか
	{
		int bishop;
		List <int> PositionList;
		List <int> EnemyPositionList;
		if(c==0)
		{
			PositionList=PositionList1;
			EnemyPositionList=PositionList2;
		}
		else
		{
			PositionList=PositionList2;
			EnemyPositionList=PositionList1;
		}
		if(s==1)
		{
			bishop=PositionList[2];
		}
		else
		{
			bishop=PositionList[5];
		}
		//右上
		for(int i=bishop+9;((i<=63)&&(i%8!=0));i=i+9)
		{
			if(((bishop+9)!=i)&&(EnemyPositionList.Contains(i-9)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
		//左上
		for(int i=bishop+7;(i<=63)&&(i%8!=7);i=i+7)
		{
			if(((bishop+7)!=i)&&(EnemyPositionList.Contains(i-7)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
		//左下
		for(int i=bishop-9;(i>=0)&&(i%8!=7);i=i-9)
		{
			if(((bishop-9)!=i)&&(EnemyPositionList.Contains(i+9)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
		//右下
		for(int i=bishop-7;(i>=0)&&(i%8!=0);i=i-7)
		{
			if(((bishop-7)!=i)&&(EnemyPositionList.Contains(i+7)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
	}
	
	public void queenselect(int c)
	{
		List <int> PositionList;
		List <int> EnemyPositionList;
		if(c==0)
		{
			PositionList=PositionList1;
			EnemyPositionList=PositionList2;
		}
		else
		{
			PositionList=PositionList2;
			EnemyPositionList=PositionList1;
		}
		int queen = PositionList[3];
		//上
		for(int i=queen+8;i<=63;i=i+8)
		{
			if(((queen+8)!=i)&&(EnemyPositionList.Contains(i-8)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
		//下
		for(int i=queen-8;i>=0;i=i-8)
		{
			if(((queen-8)!=i)&&(EnemyPositionList.Contains(i+8)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
		//右
		for(int i=queen+1;i%8!=0;i++)
		{
			if(((queen+1)!=i)&&(EnemyPositionList.Contains(i-1)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
	
		//左
		for(int i=queen-1;i%8!=7;i--)
		{
			if(((queen-1)!=i)&&(EnemyPositionList.Contains(i+1)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
		//右上
		for(int i=queen+9;((i<=63)&&(i%8!=0));i=i+9)
		{
			if(((queen+9)!=i)&&(EnemyPositionList.Contains(i-9)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
		//左上
		for(int i=queen+7;(i<=63)&&(i%8!=7);i=i+7)
		{
			if(((queen+7)!=i)&&(EnemyPositionList.Contains(i-7)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
		//左下
		for(int i=queen-9;(i>=0)&&(i%8!=7);i=i-9)
		{
			if(((queen-9)!=i)&&(EnemyPositionList.Contains(i+9)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
		//右下
		for(int i=queen-7;(i>=0)&&(i%8!=0);i=i-7)
		{
			if(((queen-7)!=i)&&(EnemyPositionList.Contains(i+7)==true))
			{
				break;
			}
			else if(PositionList.Contains(i)==true)
			{
				break;
			}
			else if((0>i)||(i>63))
			{
				break;
			}
			else
			{
				RedList.Add(i);
			}
		}
	}
	////////////////////////////////////////////////////////////////////////
}
