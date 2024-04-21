using UnityEngine;
using UnityEditor;

public class TeleportToBossRoom : Editor
{
    [MenuItem("Tools/Teleport to Boss Room %&b")] // Shortcut: Ctrl + Shift + B
    static void TeleportToBoss()
    {

        GameObject bossRoom = GameObject.FindWithTag("BossRoom");
        PlayerReferences.instance.GetPlayer().transform.position = bossRoom.transform.position;
        Debug.Log("Teleporting to Boss Room");
    }
}
