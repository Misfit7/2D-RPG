using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTest : MonoBehaviour
{
    Object obj;
    // ��������
    public void Recycling()
    {
        Resources.UnloadUnusedAssets();
    }
    public void Recycling(Object obj)
    {
        Resources.UnloadAsset(obj);
    }

    //���ٶ���
    public void Destroy()
    {
        Recycling(obj);
        Destroy(obj);
    }
}
