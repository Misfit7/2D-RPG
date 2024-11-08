using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace MyLuaCallCS
{
    #region ��̬��
    public static class TestStatic
    {
        public static int ID = 99;
        public static string Name
        {
            get;
            set;
        }
        public static string OutPut()
        {
            return "static function";
        }
        public static void Default(string str = "default function")
        {
            Debug.Log(str);
        }
    }
    #endregion
    #region ʵ������
    public class NPC
    {
        public string name;
        public float HP
        {
            get;
            set;
        }
        public NPC()
        {
            this.name = "Default NPC Base";
        }
        public NPC(string name)
        {
            this.name = name;
        }
        public virtual void VirtualOutput()
        {
            Debug.Log("Virtual Output");
        }
        public void OutPut()
        {
            Debug.Log(name);
        }
        public void OutPut(string str)
        {
            Debug.Log(name + " " + str);
        }
        public void OutPut(int a)
        {
            Debug.Log(name + " " + a);
        }
        public void OutPut(float a)
        {
            Debug.Log(name + " " + (float)a);
        }
    }
    #endregion
    #region �̳У����أ��ຯ����չ
    public class NPC1 : NPC
    {
        public NPC1()
        {
            this.name = "Default NPC1";
        }
        public NPC1(string name)
        {
            this.name = name;
        }
        public override void VirtualOutput()
        {
            Debug.Log("Override Output");
        }
    }
    [LuaCallCSharp]
    public static class NPCExtend   //����չ
    {
        public static void Show(this NPC1 npc1) //�෽����չ
        {
            Debug.Log("NPC1 Class Extend Function");
        }
    }
    #endregion
    #region �ṹ��
    struct MyStruct
    {
        public string name;
        public void OutPut()
        {
            Debug.Log(name);
        }
    }
    #endregion
    #region ö��
    enum MyEnum
    {
        No1 = 1,
        No2 = 2,
        No3 = 3,
    }
    #endregion
    #region ί��
    public delegate void FDelegate();
    public class MyDelegate
    {
        public static FDelegate StaticDelegate;
        public FDelegate DynamicDelegate;
        public static void StaticFunction()
        {
            Debug.Log("My C# Static Function");
        }
    }
    #endregion
    #region �¼�
    public delegate void EventDelegate();
    public class MyEventDelegate
    {
        public static event EventDelegate StaticEventDelegate;
        public event EventDelegate DynamicEventDelegate;
        public static void StaticFunction()
        {
            Debug.Log("My C# Static Function");
        }
        public static void CallStaticEvent()
        {
            if (StaticEventDelegate != null)
            {
                StaticEventDelegate();
            }
        }
        public void CallDynamicEvent()
        {
            if (DynamicEventDelegate != null)
            {
                DynamicEventDelegate();
            }
        }
    }
    #endregion
    #region �෵��ֵ
    public class MyOutRef
    {
        public static string Func1()
        {
            return "Func1";
        }
        public static string Func2(string str1, out string str2)
        {
            str2 = "Func2 out";
            return str1;
        }
        public static string Func3(string str1, ref string str2)
        {
            str2 = "Func3 ref";
            return str1;
        }
        public static string Func4(out string str1, ref string str2, string str3)    //out/ref��ǰ
        {
            str1 = "Func4 out";
            str2 = "Func4 ref";
            return str3;
        }
    }
    #endregion
}

public class LuaCallCS : MonoBehaviour
{
    private void Start()
    {
        xLuaMgr.Instance.DoString("require('Lua/LuaCallCS')");
    }

    private void OnDestroy()
    {
        xLuaMgr.Instance.Free();
    }
}