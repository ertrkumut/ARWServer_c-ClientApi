﻿using System;
using System.Collections.Generic;

namespace ARWServer_UnityApi
{
	public class ARWEvents
	{
		public static List<ARWEvent> allEvents;
		public static List<ExtensionRequest> extensionRequests;
		
#region Public Events
		public static ARWEvent CONNECTION;
		public static ARWEvent CONNECTION_LOST;
		public static ARWEvent DISCONNECTION;
		public static ARWEvent LOGIN;
		public static ARWEvent LOGIN_ERROR;
		public static ARWEvent LOGOUT;
		public static ARWEvent ROOM_JOIN;
		public static ARWEvent ROOM_JOIN_ERROR;
		public static ARWEvent USER_ENTER_ROOM;
		public static ARWEvent USER_EXIT_ROOM;
		public static ARWEvent USER_VARIABLE_UPDATE;
#endregion
		
#region Private Events
		private static ARWEvent EXTENSION_REQUEST;
#endregion

		public static void Init(){
			PrivateEventHandlers _privateEventHandlers = new PrivateEventHandlers ();

			allEvents = new List<ARWEvent> ();
			extensionRequests = new List<ExtensionRequest>();

			CONNECTION 					= new ARWEvent(ARWServer_CMD.Connection_Success);
			CONNECTION.p_handler 		= _privateEventHandlers.P_Connection;


			CONNECTION_LOST 			= new ARWEvent (ARWServer_CMD.Connection_Lost);
			CONNECTION_LOST.p_handler 	= _privateEventHandlers.P_Connection_Lost;

			DISCONNECTION 				= new ARWEvent (ARWServer_CMD.Disconnection);
			DISCONNECTION.p_handler 	= _privateEventHandlers.P_Disconnection;

			EXTENSION_REQUEST 			= new ARWEvent (ARWServer_CMD.Extension_Request);
			EXTENSION_REQUEST.p_handler = _privateEventHandlers.P_Extension_Handler;

			LOGIN 						= new ARWEvent (ARWServer_CMD.Login);
			LOGIN.p_handler 			= _privateEventHandlers.P_Login;

			LOGIN_ERROR 				= new ARWEvent (ARWServer_CMD.Login_Error);

			LOGOUT 						= new ARWEvent ();

			ROOM_JOIN 					= new ARWEvent (ARWServer_CMD.Join_Room);
			ROOM_JOIN.p_handler 		= _privateEventHandlers.P_Join_Room;

			ROOM_JOIN_ERROR 			= new ARWEvent ();

			USER_ENTER_ROOM 			= new ARWEvent (ARWServer_CMD.User_Enter_Room);
			USER_ENTER_ROOM.p_handler	= _privateEventHandlers.P_User_Enter_Room;
			
			USER_EXIT_ROOM 				= new ARWEvent (ARWServer_CMD.User_Exit_Room);

			USER_VARIABLE_UPDATE = new ARWEvent(ARWServer_CMD.User_Variable_Update);
			USER_VARIABLE_UPDATE.p_handler = _privateEventHandlers.P_User_Variable_Update;

			allEvents.Add (CONNECTION);
			allEvents.Add (CONNECTION_LOST);
			allEvents.Add (LOGIN);
			allEvents.Add (LOGIN_ERROR);
			allEvents.Add (ROOM_JOIN);
			allEvents.Add (USER_ENTER_ROOM);
			allEvents.Add (USER_EXIT_ROOM);
			allEvents.Add (DISCONNECTION);
			allEvents.Add(EXTENSION_REQUEST);

		}
	}
}

