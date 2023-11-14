using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        public float health = 100f;
        [SerializeField] ParticleSystem _particleSystem;
        [SerializeField] UnityEvent onDamage;
        [SerializeField] UnityEvent onHealing;
        [SerializeField] UnityEvent onDie;

        bool isDead = false;

        void Update()
        {
            if (_particleSystem != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(HealthPotion());
                }
            }
            
        }

        private IEnumerator HealthPotion()
        {
            onHealing.Invoke();

            for (int i = 0; i < 4; i++)
            {
                health += 10f;
                _particleSystem.Play();
                yield return new WaitForSeconds(0.5f);
                
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            onDamage.Invoke();
            if (health == 0)
            {
                onDie.Invoke();
                Die();
            }
        }

        private void Die()
        {
            if (isDead)
            {
                return;
            }
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
