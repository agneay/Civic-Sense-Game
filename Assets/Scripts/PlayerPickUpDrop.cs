using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;

    private ObjectGrabable objectGrabable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabable == null)
            {
                float pickUpDistance = 2f;

                if (Physics.Raycast(
                    playerCameraTransform.position,
                    playerCameraTransform.forward,
                    out RaycastHit raycastHit,
                    pickUpDistance,
                    pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabable))
                    {
                        objectGrabable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                objectGrabable.Drop();
                objectGrabable = null;
            }
        }
    }
}
