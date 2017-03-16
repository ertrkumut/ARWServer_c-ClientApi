﻿using System;

namespace ARWServer_UnityApi
{
	public class User
	{

		public string name;
		public int id;
		public bool isMe;

		public Room lastJoinedRoom;

		public User(){
		}
		public User (SpecialEventParam e){
			this.name = e.GetString ("userName");
			this.id = e.GetInt ("userId");
			this.isMe = bool.Parse (e.GetString ("isMe"));
			this.lastJoinedRoom = null;

			UserManager.allUserInGame.Add (this);
		}
	}
}
