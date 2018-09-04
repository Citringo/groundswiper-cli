using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System.Net.Sockets;

public class GroundSwiper
{
    public static GroundSwiper I { get; } = new GroundSwiper();

    private GroundSwiper() { }

    List<byte> bytes;

    UdpClient udp;

    string ip;
    int port;

    public void Connect(string ip, string port)
    {
        bytes = new List<byte>();
        udp = new UdpClient();
        this.ip = ip;
        if (!int.TryParse(port, out this.port))
        {
            throw new GSException("エラー！ ポート番号が異常");
        }
    }

    public void Close()
    {
        udp.Close();
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
        udp.Send(bytes.ToArray(), bytes.Count, ip, port);
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