using MyCSCallLua;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace MyCSCallLua
{
    //变量映射到类成员
    [GCOptimize]    //映射后无GC
    public class LuaToCS
    {
        public int num;
        public float flt;
        public bool bIs;
        public string name;
        public override string ToString()
        {
            return "num=" + num + " flt=" + flt + " bIs=" + bIs + " name=" + name;
        }

        public Func1 func1;
        public Func2 func2;
        public Func3 func3;
        public Func4 func4;
    }
    //函数映射到委托
    public delegate void Func1();
    public delegate void Func2(string name);
    public delegate string Func3(); //返回值(字符串)
    [CSharpCallLua]
    public delegate int Func4(int a, string b, out DClass c);   //多返回值

    public class DClass
    {
        public int f1;
        public int f2;
    }
}

public class CSCallLua : MonoBehaviour
{
    private void Start()
    {
        xLuaMgr.Instance.DoString("require('Lua/CSCallLua')");
        LuaTable g = xLuaMgr.Instance.Global;   //导出Lua全局变量表
        LuaToCS luaToCS = g.Get<LuaToCS>("LuaToCS");    //Lua表映射到Class，按键值对，不用再赋值
        //变量
        //luaToCS.num = g.Get<int>("num");
        //luaToCS.flt = g.Get<float>("flt");
        //luaToCS.bIs = g.Get<bool>("bIs");
        //luaToCS.name = g.Get<string>("name");
        Debug.Log(luaToCS.ToString());

        //函数
        //luaToCS.func1 = g.Get<Func1>("func1");
        luaToCS.func1();
        //luaToCS.func2 = g.Get<Func2>("func2");
        luaToCS.func2("func2 arg");
        //luaToCS.func3 = g.Get<Func3>("func3");
        Debug.Log(luaToCS.func3());
        //luaToCS.func4 = g.Get<Func4>("func4");
        DClass d;
        int r = luaToCS.func4(1, "b", out d);
        Debug.Log("ret=" + r + ", d = {f1=" + d.f1 + ", f2=" + d.f2 + "}");


    }
    private void OnDestroy()
    {
        xLuaMgr.Instance.Free();
    }
}
