using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static Action<string, float> OnVolumeChanged;
    public static Action<string, bool> OnButtonVolumeChanged;
    public static Action<GameObject, GameObject> OnBowlChange;
    public static Action<GameObject, Vector3> OnAssembly;

    public static bool isScene3;
}
