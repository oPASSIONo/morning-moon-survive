using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
    public GameObject damageTextPrefab; // Prefab ของ TextMeshPro
    public Transform damageTextPosition; // ตำแหน่งที่จะปรากฏตัวเลข
    public int damageAmount = 10;
    public Toggle displayDamageToggle; // Checkbox สำหรับเปิดปิด

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && displayDamageToggle.isOn)
        {
            ShowDamage(damageAmount);
            Debug.Log("Deal 20 Damage(Partical)");
        }

        else if (Input.GetKeyDown(KeyCode.Space) && displayDamageToggle)
        {
            Debug.Log("Deal 20 Damage");
        }
    }

    void ShowDamage(int damage)
    {
        GameObject damageTextObject = Instantiate(damageTextPrefab, damageTextPosition.position, Quaternion.identity);
        TextMeshPro textMesh = damageTextObject.GetComponent<TextMeshPro>();
        textMesh.text = damage.ToString();
        StartCoroutine(DestroyAfterSeconds(damageTextObject, 2.0f)); // ลบตัวหนังสือหลังจาก 2 วินาที
    }

    private System.Collections.IEnumerator DestroyAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
    }
}