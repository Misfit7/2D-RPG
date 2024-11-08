using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTest : MonoBehaviour
{
    Object obj;
    // 垃圾回收
    public void Recycling()
    {
        Resources.UnloadUnusedAssets();
    }
    public void Recycling(Object obj)
    {
        Resources.UnloadAsset(obj);
    }

    //销毁对象
    public void Destroy()
    {
        Recycling(obj);
        Destroy(obj);
    }
}
