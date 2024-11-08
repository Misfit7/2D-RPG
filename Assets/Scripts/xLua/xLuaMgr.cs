using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class xLuaMgr
{
    #region Singleton
    private static xLuaMgr instance = null;
    public static xLuaMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new xLuaMgr();
            }
            return instance;
        }
    }
    public void Free()
    {
        if (luaEnv != null)
        {
            luaEnv.Dispose();
            instance = null;
        }
    }
    #endregion
    #region LuaEnv
    private LuaEnv luaEnv = null;
    private xLuaMgr()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader); //���Զ����������ӵ���������
    }
    //�Զ�����������Ի�ȡrequire���ص�lua�ļ�����
    private byte[] MyLoader(ref string filepath)
    {
        string path = Application.streamingAssetsPath + filepath;
        //Debug.Log(path);
        //��ȡΪ�ֽ����飬ִ�з��ص�Lua����
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        else
        {
            return null;
        }
    }
    public object[] DoString(string code)
    {
        return luaEnv.DoString(code);
    }
    public LuaTable Global
    {
        get
        {
            return luaEnv.Global;
        }
    }
    #endregion
}
