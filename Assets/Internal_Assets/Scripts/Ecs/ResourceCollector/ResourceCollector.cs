using UnityEngine;

namespace Secret
{
    public class ResourceCollector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerCargoManager.Instance.ClearCargo();
            }
        }
    }
}
