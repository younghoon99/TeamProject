using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator characterAnimator;
    public GameObject npcMenu; // 메뉴 UI 오브젝트
    public GameObject[] npcs; // NPC 오브젝트 배열
    public float moveRadius = 5f; // NPC 이동 반경
    public float moveSpeed = 2f; // NPC 이동 속도

    // Start is called before the first frame update
    void Start()
    {
        // Start NPC movement coroutine
        foreach (GameObject npc in npcs)
        {
            StartCoroutine(MoveNPC(npc));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) // Press 'D' to start digging
        {
            StartDigging();
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            CheckNPCClick();
        }
    }

    void StartDigging()
    {
        if (characterAnimator != null)
        {
            characterAnimator.SetTrigger("Dig");
        }
    }

    void CheckNPCClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("NPC")) // NPC 태그 확인
            {
                ShowNPCMenu();
            }
        }
    }

    void ShowNPCMenu()
    {
        if (npcMenu != null)
        {
            npcMenu.SetActive(true); // 메뉴 활성화
        }
    }

    IEnumerator MoveNPC(GameObject npc)
    {
        while (true)
        {
            Vector2 randomPosition = (Vector2)npc.transform.position + Random.insideUnitCircle * moveRadius;
            float elapsedTime = 0f;
            Vector2 startPosition = npc.transform.position;

            while (elapsedTime < 1f)
            {
                npc.transform.position = Vector2.Lerp(startPosition, randomPosition, elapsedTime);
                elapsedTime += Time.deltaTime * moveSpeed;
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(1f, 3f)); // Wait before moving again
        }
    }
}
