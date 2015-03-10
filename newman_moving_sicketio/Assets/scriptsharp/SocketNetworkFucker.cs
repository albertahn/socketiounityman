using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SocketNetworkFucker : MonoBehaviour {


	public SocketIOClient.Client awesocket;


	bool isRefreshing = false;
	float refreshRequestLength = 3.0f;

	HostData[] hostData;

	//start server
	public GameObject playerone;


//start socket
	private void Start(){

		awesocket = new SocketIOClient.Client("http://127.0.0.1:3000/");


		awesocket.On("connect", (fn) => {


			Debug.Log ("connect - socket");
			
			Dictionary<string, string> args = new Dictionary<string, string>();
			//args.Add("send message", "what's up?");
			awesocket.Emit("send message", "yo new albert socket in town");

			//awesocket.Emit("send message", "yo new socket in town");
		});
		awesocket.On("new message", (data) => {

			Debug.Log (data.Json.ToJsonString());

		});
		awesocket.Error += (sender, e) => {

			Debug.Log ("socket Error: " + e.Message.ToString ());
		};
		awesocket.Connect();

	//	SpawnPlayer ();


		}//end

//init

	void Update () {
		
	}


	
	public void OnGUI(){
		
		if (GUI.Button (new Rect (20, 70, 150, 30), "SEND adduser")) {

			
			Dictionary<string, string> args = new Dictionary<string, string>();
			//args.Add("send message", "hello!");
			awesocket.Emit("adduser", "albertmofo");
		}
		
		if (GUI.Button (new Rect (20, 120, 150, 30), "Close Connection")) {
			Debug.Log ("Closing");
			
			awesocket.Close();
		}
	}//end gui

//gui

		
	public void sendfukMessage(){

		Debug.Log ("sending");
		//awesocket.Emit("send message", messsage);


	}


	//player part2

	private void SpawnPlayer(){

		Debug.Log ("Spawning Player..");

		//GameObject go = Instantiate (playerone, new Vector3(0f, 2.5f, 0f), Quaternion.identity, 0);

		//Network.Instantiate (playerone, new Vector3(0f, 2.5f, 0f), Quaternion.identity, 0);

	}



}
