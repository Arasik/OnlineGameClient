using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using Common;

/// <summary>
/// 
/// 用来管理和服务器端的socket连接
/// </summary>
public class ClientManager:BaseManager
{
    private const string IP = "127.0.0.1";
    private const int PORT = 6688;
    private Socket clientSocket;
    private Message msg;

    public ClientManager(GameFacade gameFacade) : base(gameFacade) { }
    public override void OnInit()
    {
        base.OnInit();
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        msg = new Message();
        try
        {
            clientSocket.Connect(IP, PORT);
            Start();
        }
        catch(Exception e)
        {
            Debug.LogWarning("无法连接到服务器端，请检查您的网络!" + e);
        }
    }
    private void Start()
    {
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize,SocketFlags.None,ReceiveCallBack,null);
    }
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            int count = 0;
            if (clientSocket == null || !clientSocket.Connected) return;
            count = clientSocket.EndReceive(ar);
            msg.ReadMessage(count, OnProcessDataCallback);
            Start();
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
        
    }
    private void OnProcessDataCallback(ActionCode actionCode,string data)
    {
        //TODO
        facade.HandleResponse(actionCode, data);
    }
    public void SendRequest(RequestCode requestCode,ActionCode actionCode,string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch(Exception e)
        {
            Debug.LogWarning("无法关闭连接！" + e);
        }
    }

 }
