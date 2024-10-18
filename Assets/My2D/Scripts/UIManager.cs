using TMPro;
using UnityEngine;

namespace My2D
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        public GameObject damageTextPrefab;
        public GameObject healTextPrefab;

        private Canvas canvas;
        [SerializeField] private Vector3 healthTextOffset = Vector3.zero;
        #endregion

        private void Awake()
        {
            //����
            canvas = FindObjectOfType<Canvas>();
        }

        private void OnEnable()
        {
            //ĳ���� ���� �̺�Ʈ �Լ� ���
            CharacterEvents.charaterDamaged += characterDamaged;
            CharacterEvents.charaterDamaged += CharacterHealed;
            
        }

        private void OnDisable()
        {
            //ĳ���� ���� �̺�Ʈ �Լ� ����
            CharacterEvents.charaterDamaged -= characterDamaged;
            CharacterEvents.charaterDamaged -= CharacterHealed;
        }

        public void characterDamaged(GameObject character, float damage)
        {
            //damageTextPrefab ����
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

            GameObject textGo = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, canvas.transform);
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            damageText.text = damage.ToString();
        }

        public void CharacterHealed(GameObject character, float restore)
        {
            //healTextPrefab ����
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

            GameObject textGo = Instantiate(healTextPrefab, spawnPosition + healthTextOffset, Quaternion.identity, canvas.transform);
            TextMeshProUGUI healText = textGo.GetComponent<TextMeshProUGUI>();
            healText.text = restore.ToString();
        }
    }
}