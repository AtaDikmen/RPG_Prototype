using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] Weapon weapon2 = null;

        /*private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player") 
            {
                other.GetComponent<Fighter>().SpawnWeapon(weapon);
                Destroy(gameObject);
            }
        }*/

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                this.GetComponent<Fighter>().SpawnWeapon(weapon);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                this.GetComponent<Fighter>().SpawnWeapon(weapon2);
            }
        }
    }
}
