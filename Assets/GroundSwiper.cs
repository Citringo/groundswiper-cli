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

    List<byte> bytes;

    public void Connect(string protocol, string path, string port)
    {
        bytes = new List<byte>();
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

    public void Close()
    {
        sock.Close();
    }

    public void QueueData(KeyMode mode, ConsoleKey key)
    {
        // 0byte: mode (0: down, 1: up)
        // 1byte: KeyCode
        bytes.Add((byte)mode);
        bytes.Add((byte)key);
    }

    public void SendData()
    {
        if (bytes.Count == 0) return;
        sock.Send(bytes.ToArray(), 0, bytes.Count);
        Debug.Log(bytes.Count);
        bytes.Clear();
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