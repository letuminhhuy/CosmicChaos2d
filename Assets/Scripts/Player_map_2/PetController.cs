using System.Collections;
using TMPro;
using UnityEngine;

public class PetController : MonoBehaviour
{
    private Animator animator;
    private Player_map_2_Health playerHealth;
    private Player_map_2 player;
    private SpriteRenderer spriteRenderer;
    public TextMeshProUGUI healText; // Tham chiếu đến UI Text

    private const float HEAL_AMOUNT = 2f;
    private const float HEAL_INTERVAL = 5f;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponentInParent<Player_map_2_Health>();
        player = GetComponentInParent<Player_map_2>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (healText != null)
            healText.gameObject.SetActive(false);

        StartCoroutine(AutoHeal());

    }

    void Update()
    {
        UpdatePetAnimation();
        FlipPet(); // Lật Sprite Pet theo hướng Player

    }
    void FlipPet()
    {
        // Nếu Player quay trái thì Pet cũng quay trái, nếu Player quay phải thì Pet cũng quay phải
        spriteRenderer.flipX = player.IsFacingLeft();
    }
    void UpdatePetAnimation()
    {
        if (playerHealth.GetHealth() <= 0)
        {
            animator.SetBool("isDead", true);
            Destroy(gameObject, 1f);
            return;
        }

        animator.SetBool("isDead", false);
        animator.SetBool("isMoving", player.IsMoving());
    }

    IEnumerator AutoHeal()
    {
        while (true)
        {
            yield return new WaitForSeconds(HEAL_INTERVAL);

            if (playerHealth.GetHealth() < 100)
            {
                animator.SetTrigger("Heal"); // Chạy animation hồi máu
                playerHealth.Heal(HEAL_AMOUNT);
                if (healText != null)
                {                   
                    healText.gameObject.SetActive(true);
                    StartCoroutine(HideHealText());
                }
            }
        }
    }
    IEnumerator HideHealText()
    {
        yield return new WaitForSeconds(1f);
        if (healText != null)
            healText.gameObject.SetActive(false);
    }
}
