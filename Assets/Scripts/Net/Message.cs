using System.Linq;
using System.Text;
using System;
using Common;

class Message
{
    private byte[] data = new byte[1024];
    private int startIndex = 0;//存储了多少数据在数组里面
    public void AddCount(int count)
    {
        startIndex += count;
    }
    public byte[] Data
    {
        get { return data; }
    }
    public int StartIndex
    {
        get { return startIndex; }
    }
    public int RemainSize
    {
        get { return data.Length - startIndex; }
    }
    public void ReadMessage(int newDataAmount, Action<ActionCode, string> processDataCallback)
    {
        startIndex += newDataAmount;
        while (true)
        {
            if (startIndex <= 4) return;
            int count = BitConverter.ToInt32(data, 0);
            if ((startIndex - 4) >= count)
            {
                //Console.WriteLine(startIndex);
                //Console.WriteLine(count);
                //string s = Encoding.UTF8.GetString(data, 4, count);
                //Console.WriteLine("解析出来一条数据: " + s);
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                string s = Encoding.UTF8.GetString(data, 8, count - 4);
                processDataCallback(actionCode, s);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= (count + 4);
            }
            else
            {
                break;
            }
        }
    }
    //对数据加上包头和code
    //public static byte[] PackData(ActionCode actionCode, string data)
    //{
    //    byte[] requestCodeBytes = BitConverter.GetBytes((int)actionCode);
    //    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
    //    int dataAmount = requestCodeBytes.Length + dataBytes.Length;
    //    byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);

    //    return dataAmountBytes.Concat(requestCodeBytes)
    //        .Concat(dataBytes).ToArray<byte>();
    //}
    public static byte[] PackData(RequestCode requestCode,ActionCode actionCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + dataBytes.Length + actionCodeBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);

        return dataAmountBytes
            .Concat(requestCodeBytes)
            .Concat(actionCodeBytes)
            .Concat(dataBytes)
            .ToArray<byte>();
    }
}
