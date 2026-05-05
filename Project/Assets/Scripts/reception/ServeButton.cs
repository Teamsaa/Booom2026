using UnityEngine;
using UnityEngine.UI;

public class ServeButton : MonoBehaviour
{
    public GuestManager guestManager;

    public void OnClick()
    {
        guestManager.ServeNextGuest();
    }
}