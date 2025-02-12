using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction PlatformBreakDown;    

    public static void RespawnPlatform() => PlatformBreakDown?.Invoke(); 
    

}
