using UnityEngine;
using TMPro;

public class FishTugMinigame : MiniGameBase
{
    public Transform fish;
    public TextMeshProUGUI countdownText;
    public float countdownTime = 5f;
    public float dragThreshold = 50f;
    public float swimSpeed = 3f;
    public float swimLimitX = 5f;
    public float returnSpeed = 5f;

    private float timer;
    private Vector2 dragStart;
    private int fishDir;

    private bool returningToCenter = false;
    private bool inputLocked = false;

    private void Start()
    {
        timer = countdownTime;

        fishDir = (Random.value < 0.5f) ? -1 : 1;
        float yRot = (fishDir == -1) ? 180f : 0f;
        fish.localRotation = Quaternion.Euler(0, yRot, 0);

        Vector3 pos = fish.position;
        pos.y = 0.3f;
        fish.position = pos;

        countdownText.text = Mathf.Ceil(timer).ToString();
    }

    private void Update()
    {
        if (returningToCenter)
        {
            Vector3 target = new Vector3(0f, fish.position.y, fish.position.z);
            fish.position = Vector3.MoveTowards(fish.position, target, returnSpeed * Time.deltaTime);

            if (Mathf.Abs(fish.position.x) < 0.01f)
            {
                returningToCenter = false;
                StartCoroutine(UnloadAfterDelay(1f));
            }

            return;
        }

        if (gameEnded) return;

        Vector3 move = Vector3.right * fishDir * swimSpeed * Time.deltaTime;
        fish.position += move;

        if (Mathf.Abs(fish.position.x) > swimLimitX)
        {
            fishDir *= -1;
            float yRot = (fishDir == -1) ? 180f : 0f;
            fish.localRotation = Quaternion.Euler(0, yRot, 0);
        }

        timer -= Time.deltaTime;
        countdownText.text = Mathf.Ceil(timer).ToString();

        if (timer <= 0f)
        {
            EndMiniGame(false);
            StartCoroutine(UnloadAfterDelay(1f));
            return;
        }

        if (!inputLocked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragStart = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                float deltaX = Input.mousePosition.x - dragStart.x;

                if (Mathf.Abs(deltaX) < dragThreshold)
                {
                    EndMiniGame(false);
                    StartCoroutine(UnloadAfterDelay(1f));
                    return;
                }

                bool win = (fishDir * deltaX) < 0f;

                EndMiniGame(win);

                if (win)
                {
                    returningToCenter = true;
                    inputLocked = true;
                }
                else
                {
                    StartCoroutine(UnloadAfterDelay(1f));
                }
            }
        }
    }
}
