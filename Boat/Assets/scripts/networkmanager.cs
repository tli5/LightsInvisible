using UnityEngine;
using System.Collections;

public class networkmanager : MonoBehaviour {

	private const string typeName = "ARBoatMultiPlayer";
	private const string gameName = "litr965";
	private const int maxPlayerCount = 6;
	public GameObject playerPrefab;


	private void StartServer()
	{
		Network.InitializeServer(maxPlayerCount, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized()
	{
		Debug.Log("Boat Gaming Server Initializied");
		SpawnPlayer();
	}

	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}

	//to deal with hosts
	private HostData[] hostList;
	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		SpawnPlayer();
	}

	private void SpawnPlayer ()
	{
		float x = Random.Range (0f, 250f);
		float y = Random.Range (0f, 250f);
		float z = Random.Range (0f, 250f);
		Quaternion qt = Quaternion.Euler (270, 0, 0);
		GameObject player = (GameObject)Network.Instantiate (playerPrefab, new Vector3 (x, y, z), qt, 0);
		player.GetComponent<Renderer> ().material.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));

	}
	
}
