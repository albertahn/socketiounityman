using UnityEngine;
using System.Collections;

public class SocketNetworkTest : MonoBehaviour {

	string registeredGameName ="Tanggoal_BattleGoal_awesome_zombie";


	bool isRefreshing = false;
	float refreshRequestLength = 3.0f;

	HostData[] hostData;

	//start server
	public GameObject playerone;

	private void StartSocket(){


		}//end

//init
	void OnServerInitialized(){

		Debug.Log ("server intilized");

		SpawnPlayer ();

		}//end

// inumer

	public IEnumerator RefreshHostList(){

		Debug.Log ("refreshing...");

		MasterServer.RequestHostList (registeredGameName);

		float timeStarted = Time.time;
		float timeEnd = Time.time + refreshRequestLength;

		while (Time.time < timeEnd) {
		
			hostData = MasterServer.PollHostList();
			yield return new WaitForEndOfFrame();

		}//end while

		if (hostData == null || hostData.Length == 0) {
						Debug.Log ("No active servers have been found");		
				} else {//endif
				
			Debug.Log (hostData.Length + "have been found");
		}

		hostData = MasterServer.PollHostList ();



	}


	void OnMasterServerEvent (MasterServerEvent masterServerEvent){

		if(masterServerEvent== MasterServerEvent.RegistrationSucceeded){
			Debug.Log("registration success");


		}else{//endif
			Debug.Log("registration fail");

		}
	
	}



//gui

		public void OnGUI(){

		//check if server
		if (Network.isServer) {
			GUILayout.Label("Running as a server.");

		}else if (Network.isClient) {
			GUILayout.Label("Running as a client.");
		}

		if (Network.isClient) {

			if (GUI.Button (new Rect (25f, 25f, 150f, 30f), "spawn")) {
				
				SpawnPlayer();
				
				
			}//end if
		}

		if (!Network.isClient && !Network.isServer) {  //not either

				
		if (GUI.Button (new Rect (25f, 25f, 150f, 30f), "start new server")) {

			StartSocket();

				//SpawnPlayer();
		
		}//end if
}//end if for not server and not client

		if (GUI.Button (new Rect (25f, 65f, 150f, 30f), "Refresh Server List")) {
		
			StartCoroutine("RefreshHostList");
		
		 }//end if

		if (hostData != null) {
				
			for(int i = 0; i< hostData.Length; i++){

				if(GUI.Button (new Rect(Screen.width/2, 65f + (30f * i ), 300f, 30f), hostData[i].gameName))
				{
					Network.Connect (hostData[i]);

					Debug.Log("conecting to server..."+hostData[i]);

					SpawnPlayer ();

				}

			}//endfor
		}//endif

}//end gui



	//player part2

	private void SpawnPlayer(){

		Debug.Log ("Spawning Player..");

		Network.Instantiate (playerone, new Vector3(0f, 2.5f, 0f), Quaternion.identity, 0);

	}

	void OnPlayerDisconnected(NetworkPlayer player){
		Debug.Log ("player disconnected from "+ player.ipAddress +":"+player.port);
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}// onplayerdis


//quiting game 
	void OnApplicationQuit(){

		if(Network.isServer){
			Network.Disconnect(200);
			MasterServer.UnregisterHost();


		}//endif

		if (Network.isClient) {
			Network.Disconnect(200);
		}
	}


}
