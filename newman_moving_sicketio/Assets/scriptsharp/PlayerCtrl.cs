using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PlayerCtrl : MonoBehaviour {
	//private float h = 0.0f;
//	private float v = 0.0f;

	private Transform tr;

	public float normalSpeed = 10.0f;
	public float dashSpeed = 20.0f;

	public float moveSpeed = 10.0f;
	public float rotSpeed = 100.0f;
//	private Vector3 target = Vector3.zero;

	public int hp = 100;
	public int maxHp = 100;

	//public delegate void PlayerDieHandler();
//	public static event PlayerDieHandler OnPlayerDie;
	public Material trail;

	public SocketNetworkFucker NetworkFucker;// = GetComponent<SocketNetworkFucker> ();
	//socket
	SocketIOClient.Client socket;
	
	void Start () {

		NetworkFucker =  (SocketNetworkFucker) GetComponent<SocketNetworkFucker> ();
		tr = this.transform;

//socket

		NetworkFucker.sendfukMessage ();

	

		socket = new SocketIOClient.Client("http://127.0.0.1:3000/");
		
		
		socket.On("connect", (fn) => {
			
			
			Debug.Log ("connect - socket");
			
			Dictionary<string, string> args = new Dictionary<string, string>();
			//args.Add("send message", "what's up?");
			socket.Emit("send message", "yo new albert socket in town");
			
			//awesocket.Emit("send message", "yo new socket in town");
		});
		socket.On("new message", (data) => {
			
			Debug.Log (data.Json.ToJsonString());
			
		});
		socket.Error += (sender, e) => {
			
			Debug.Log ("socket Error: " + e.Message.ToString ());
		};
		socket.Connect();
	
	}

	
	// Update is called once per frame
	void Update () {

//NETWORK IMPORTANT BEFORE ANYTHING
		//if(!networkView.isMine){ return;}

		NetworkFucker =  (SocketNetworkFucker) gameObject.GetComponent<SocketNetworkFucker> ();

		if (Input.GetKey (KeyCode.W)) {
			tr.Translate(Vector3.forward * normalSpeed * Time.deltaTime);		


			//NetworkFucker.sendfukMessage ("forwads");
			socket.Emit("send message", "forward");
		}

		if (Input.GetKey (KeyCode.S) ){
			tr.Translate(-Vector3.forward * normalSpeed*Time.deltaTime);	

			socket.Emit("send message","back");
		}

		if (Input.GetKey (KeyCode.D) ){
			tr.Translate(Vector3.right * normalSpeed*Time.deltaTime);		

			socket.Emit("send message","right");
		}

		if (Input.GetKey (KeyCode.A)) {
			tr.Translate(-Vector3.right * normalSpeed*Time.deltaTime);	

			socket.Emit("send message","left");
		}
	}




}
