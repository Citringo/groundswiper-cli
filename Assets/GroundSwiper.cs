using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using WebSocket4Net;

public class GroundSwiper
{
    public static GroundSwiper I { get; } = new GroundSwiper();

    private GroundSwiper() { }

    private WebSocket sock;

    private readonly byte[] data = new byte[2];

    public void Connect(string protocol, string path, string port)
    {
        try
        {
            sock = new WebSocket($"{protocol}{path}:{port}");
            sock.Open();
        }
        catch (Exception ex)
        {
            throw new GSException("エラー: " + ex.Message, ex);
        }
    }

    public void SendData(KeyMode mode, ConsoleKey key)
    {
        // 0byte: mode (0: down, 1: up)
        // 1byte: KeyCode
        data[0] = (byte)mode;
        data[1] = (byte)key;
        sock.Send(data, 0, 2);
    }
}

public enum KeyMode
{
    Down = 0,
    Up = 1
}

public class GSException : Exception
{
    public GSException() { }
    public GSException(string message) : base(message) { }
    public GSException(string message, Exception innerException) : base(message, innerException) { }
    protected GSException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}