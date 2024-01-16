using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPackingString
{
    private Dictionary<string, object> dataList = new Dictionary<string, object>();
    const string FDPSTRING_ID = "FDPS";

    public void clear()
    {
        dataList.Clear();
    }

    public void add(string key, object obj)
    {
        if( dataList.ContainsKey(key) )
        {
            dataList[key] = obj;
        }
        else
        {
            dataList.Add(key, obj);
        }
    }

    public void setSata(string key, object obj)
    {
        if(obj is bool) { }
        else if(obj is int) { }
        else if(obj is float) { }
        else if(obj is string) { }
        else
        {
            Debug.LogError("Set Data Syntac Error");
        }

        dataList[key] = obj;
    }

    public object getData(string key)
    {
        if( dataList.ContainsKey(key) )
        {
            return dataList[key];
        }

        return null;
    }

    // 인코드
    public string encodeDataPackingString()
    {
        string rtnString = FDPSTRING_ID;

        foreach( KeyValuePair<string, object> data in dataList )
        {
            rtnString += "," + data.Key + "," + data.Value;

            if(data.Value is bool)
            {
                rtnString += ",b";
            }
            else if( data.Value is int )
            {
                rtnString += ",i";
            }
            else if( data.Value is float )
            {
                rtnString += ",f";
            }
            else if ( data.Value is string )
            {
                rtnString += ",s";
            }
            else
            {
                Debug.LogError(string.Format(
                    "encodingDataPackingString Syntex Error {0}, {1}", data.Key, data.Value ));
            }
        }

        return rtnString;
    }

    // 디코드 
    public bool decodeDataPackingString(string val)
    {
        string[] dataTip = val.Split(',');

        if( dataTip[0] != FDPSTRING_ID )
        {
            return false;
        }

        int length = dataTip.Length;
        for( int i = 1; i < length; i += 3 )
        {
            switch( dataTip[i + 2][0] )
            {
                case 'b':
                    add(dataTip[i + 0], bool.Parse(dataTip[i + 1]));
                    break;
                case 'i':
                    add(dataTip[i + 0], int.Parse(dataTip[i + 1]));
                    break;
                case 'f':
                    add(dataTip[i + 0], float.Parse(dataTip[i + 1]));
                    break;
                case 's':
                    add(dataTip[i + 0], dataTip[i + 1]);
                    break;
            }
        }

        return true;
    }

    // 문자열 쓰기, 불러오기
    public void PlyerPrefsSetStringUTF8(string key, string val)
    {
        string valBase64 = System.Convert.ToBase64String(
            System.Text.Encoding.Unicode.GetBytes(val));
        PlayerPrefs.SetString(key, valBase64);
    }

    public string PlyerPrefsGetStringUTF8(string key)
    {
        string valBase64 = PlayerPrefs.GetString(key);

        return System.Text.Encoding.Unicode.GetString(
            System.Convert.FromBase64String(valBase64));
    }
}
