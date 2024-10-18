using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    public class Damageable : MonoBehaviour
    {
        #region Variables
        private Animator animator;

        //������ ������ ��ϵ� �Լ� ȣ��
        public UnityAction<float, Vector2> hitAction;

        //ü��
        [SerializeField] private float maxHealth = 100f;
        public float MaxHealth
        {
            get { return maxHealth; }
            private set { maxHealth = value; }
        }

        private float currentHealth;
        public float CurrentHealth
        {
            get { return currentHealth; }
            private set
            {
                currentHealth = value;

                //���� ó��
                if (currentHealth <= 0)
                {
                    IsDeath = true;
                }
            }
        }

        private bool isDeath = false;
        public bool IsDeath
        {
            get { return isDeath; }
            private set
            {
                isDeath = value;
                //�ִϸ��̼�
                animator.SetBool(AnimationString.IsDeath, value);
            }
        }

        //�������
        private bool isInvincible = false;
        [SerializeField] private float invincibleTimer = 3f;
        private float countdown = 0f;

        //
        public bool LockVelocity
        {
            get
            {
                return animator.GetBool(AnimationString.LockVelocity);
            }
            private set
            {
                animator.SetBool(AnimationString.LockVelocity, value);
            }
        }
        #endregion        

        private void Awake()
        {
            //����
            animator = GetComponent<Animator>();
            countdown = invincibleTimer;
        }

        private void Start()
        {
            //�ʱ�ȭ
            CurrentHealth = MaxHealth;
            countdown = invincibleTimer;
        }

        private void Update()
        {
            //���������̸� ���� Ÿ�̸Ӹ� ������
            if (isInvincible)
            {
                if (countdown <= 0)
                {
                    isInvincible = false;

                    //Ÿ�̸� �ʱ�ȭ
                    countdown = invincibleTimer;
                }
                countdown -= Time.deltaTime;
            }
        }

        public void TakeDamage(float damage, Vector2 knocback)
        {
            if (!IsDeath && !isInvincible)
            {
                //������� �ʱ�ȭ
                isInvincible = true;

                //������ ���� hp
                float beforHealth = CurrentHealth;

                CurrentHealth -= damage;
                Debug.Log($"{transform.name}�� ���� ü���� {CurrentHealth}");

                LockVelocity = true;
                //�ִϸ��̼�
                animator.SetTrigger(AnimationString.HitTrigger);

                float realDamage = beforHealth - CurrentHealth;

                //������ ȿ��
                hitAction?.Invoke(damage, knocback);
                CharacterEvents.charaterDamaged?.Invoke(gameObject, damage);
            }
        }

        //
        public bool Heal(float amount)
        {
            if(CurrentHealth >= MaxHealth)
            {
                return false;
            }
            //�� ���� hp
            float beforeHealth = CurrentHealth;

            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);

            //���� �� hp��
            float realHealth = CurrentHealth - beforeHealth;

            CharacterEvents.charaterHealed?.Invoke(gameObject, realHealth);

            return true;
        }
    }
}