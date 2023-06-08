using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCPTestClient : MonoBehaviour
{
	public static TCPTestClient instance;

	public bool connected = false;
	public bool confirmationRecived = false;

	#region private members 	
	private TcpClient socketConnection;
	private Thread clientReceiveThread;
	private Thread connectServer;

	Coroutine connectionCoroutine;
    #endregion
    private void Awake()
    {

		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void StartConnection()
    {
		connectionCoroutine = StartCoroutine(ConnectToTcpServer());
    }

    /// <summary> 	
    /// Setup socket connection. 	
    /// </summary> 	
    IEnumerator ConnectToTcpServer()
	{
		while (true)
		{
			Debug.Log("try");
			try
			{
				clientReceiveThread = new Thread(new ThreadStart(ListenForData));
				clientReceiveThread.IsBackground = true;
				clientReceiveThread.Start();
				socketConnection = new TcpClient("localhost", 8052);
				connected = true;

				StopCoroutine(connectionCoroutine);
			}
			catch (Exception e)
			{
				Debug.Log("On client connect exception " + e);

			}

			yield return new WaitForSeconds(1f);
		}
	}
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>     
	private void ListenForData()
	{
		try
		{
			
			Byte[] bytes = new Byte[1024];
			while (true)
			{
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream())
				{
					int length;
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);
						// Convert byte array to string message. 						
						string serverMessage = Encoding.ASCII.GetString(incommingData);

						if (serverMessage == "OK")
                        {
							confirmationRecived = true;
                        }
						Debug.Log("server message received as: " + serverMessage);
					}
				}
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
	/// <summary> 	
	/// Send message to server using socket connection. 	
	/// </summary> 	
	public void SendMessageToApps(string dateTime_first, string dateTime_last)
	{
		if (socketConnection == null)
		{
			Debug.Log("nullll");
			return;
		}
		try
		{
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream();
			if (stream.CanWrite)
			{
				string clientMessage = dateTime_first + "&" + dateTime_last;
				// Convert string message to byte array.                 
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
				// Write byte array to socketConnection stream.                 
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
				Debug.Log("Client sent his message - should be received by server");
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
}