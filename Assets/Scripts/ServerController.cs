﻿using UnityEngine;
using System.Collections;
using ARWServer_UnityApi;

public class ServerController : MonoBehaviour {

	public static ServerController instanse;

	public ARWServer server;

	public string host;
	public string userName;

	void Start(){

		server = new ARWServer();
		server.Init();

		// server.host = "192.168.1.101";
		server.host = host;
		server.tcpPort = 8081;

		server.AddEventHandler(ARWEvents.CONNECTION, OnConnectionHandler);
		server.AddEventHandler(ARWEvents.LOGIN, OnLoginHandler);
		server.AddEventHandler(ARWEvents.LOGIN_ERROR, OnLoginError);
		server.AddEventHandler(ARWEvents.DISCONNECTION, OnDisconectionHandler);
		server.AddEventHandler(ARWEvents.ROOM_JOIN, RoomJoinSuccess);
		server.AddEventHandler(ARWEvents.USER_ENTER_ROOM, UserEnterRoom);

		server.AddExtensionRequest("IamReady", IamReadyHandler);
		server.Connect();

		instanse = this;
	}

	void Update(){
		if(server != null)
			server.ProcessEvents();
	}

	private void OnConnectionHandler(ARWObject obj){
		if(obj.GetString("error") != ""){
			Debug.Log("Connection Fail");
			return;
		}
		
		Debug.Log("Connection Success");
		server.SendLoginRequest(userName, new ARWObject());
	}

	private void OnLoginHandler(ARWObject obj){
		User user = obj.GetUser();
		Debug.Log(user.name + " : " + user.id + " : " + user.isMe);

		ARWObject findRoomRequest = new ARWObject();
		findRoomRequest.PutString("roomTag", "GameRoom");

		server.SendExtensionRequest("FindRoom", findRoomRequest);
	}

	private void OnLoginError(ARWObject obj){
		Debug.Log(obj.GetString("error"));
	}

	private void RoomJoinSuccess(ARWObject obj){
		Room currentRoom = obj.GetRoom();
		Debug.Log("Join Room : " + currentRoom.name + " User Count : " + currentRoom.GetUserList().Length);

		Vector3 spawnPoint;
		if(server.me.name == "umut")
			spawnPoint = Vector3.zero;
		else
			spawnPoint = new Vector3(4, 0, 0);

		server.me.character = (GameObject)Instantiate(Resources.Load<GameObject>("Player"), spawnPoint, Quaternion.identity);
		server.me.character.name = server.me.name;
		// server.SendExtensionRequest("IamReady", new ARWObject(), true);
	}
	
	private void UserEnterRoom(ARWObject obj){
		User newUser = new User(obj.eventParams);
		Debug.Log("User Enter Room = " + newUser.name);
		server.me.lastJoinedRoom.AddUser(newUser);

		Vector3 spawnPoint;
		if(server.me.name == "umut")
			spawnPoint = Vector3.zero;
		else
			spawnPoint = new Vector3(4, 0, 0);

		newUser.character = (GameObject)Instantiate(Resources.Load<GameObject>("Player"), spawnPoint, Quaternion.identity);
		newUser.character.name = newUser.name;
	}

	private void IamReadyHandler(ARWObject obj){

	}


	private void OnDisconectionHandler(ARWObject obj){
		Debug.Log("Disconnection!");
	}

	private void OnApplicationQuit(){
		server.Disconnect();
	}
}
