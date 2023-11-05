using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Canvas
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image healthBar;
        [SerializeField] Health health;

        void Start()
        {

        }

        void Update()
        {
            HealthUpdate(health.health);
        }

        public void HealthUpdate(float health)
        {
            healthBar.fillAmount = health / 200f;
        }

    }
}
