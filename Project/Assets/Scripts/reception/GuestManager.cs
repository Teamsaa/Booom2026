using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class GuestManager : MonoBehaviour
{
    [Header("Settings")]
    public GameObject guestPrefab;
    public Transform guestParent;
    public TextMeshProUGUI winText;
    public int maxGuests = 3;
    public float spawnInterval = 5f;

    private List<Guest> guests = new List<Guest>();
    private int spawnedCount = 0;
    private float spawnTimer = 0;

    void Start()
    {
        winText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (spawnedCount >= maxGuests) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnGuest();
            spawnTimer = 0;
        }
    }

    void SpawnGuest()
    {
        GameObject guestObj = Instantiate(guestPrefab, guestParent);
        Guest guest = guestObj.GetComponent<Guest>();
        guests.Add(guest);
        spawnedCount++;

        // 给客人排位置，从上到下
        float yPos = 200 - (spawnedCount - 1) * 200;
        guestObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yPos);
    }

    public void ServeNextGuest()
    {
        foreach (var guest in guests)
        {
            if (!guest.isServed && guest.gameObject.activeSelf)
            {
                guest.Serve();
                CheckWin();
                return;
            }
        }
    }

    void CheckWin()
    {
        bool allServed = true;
        foreach (var guest in guests)
        {
            if (!guest.isServed)
            {
                allServed = false;
                break;
            }
        }

        if (allServed && spawnedCount >= maxGuests)
        {
            winText.gameObject.SetActive(true);
        }
    }
}