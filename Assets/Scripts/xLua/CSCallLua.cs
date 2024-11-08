using MyCSCallLua;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace MyCSCallLua
{
    //����ӳ�䵽���Ա
    [GCOptimize]    //ӳ�����GC
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
    //����ӳ�䵽ί��
    public delegate void Func1();
    public delegate void Func2(string name);
    public delegate string Func3(); //����ֵ(�ַ���)
    [CSharpCallLua]
    public delegate int Func4(int a, string b, out DClass c);   //�෵��ֵ

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
        LuaTable g = xLuaMgr.Instance.Global;   //����Luaȫ�ֱ�����
        LuaToCS luaToCS = g.Get<LuaToCS>("LuaToCS");    //Lua��ӳ�䵽Class������ֵ�ԣ������ٸ�ֵ
        //����
        //luaToCS.num = g.Get<int>("num");
        //luaToCS.flt = g.Get<float>("flt");
        //luaToCS.bIs = g.Get<bool>("bIs");
        //luaToCS.name = g.Get<string>("name");
        Debug.Log(luaToCS.ToString());

        //����
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
