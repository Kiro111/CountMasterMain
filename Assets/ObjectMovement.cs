using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [System.Serializable]
    public struct MoveTarget
    {
        public Transform targetObject; // ������� ������
        public float moveSpeed; // �������� ����������� � �������� �������
    }

    [SerializeField] MoveTarget[] moveTargets; // ������ � �������� ���������
    [SerializeField] float minScaleFactor = 0.0000001f; // ����������� ����������� ��������

    private Vector3 initialScale; // ����������� ������ �������
    private bool isMoving = false;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveToObject());
        }
    }

    private System.Collections.IEnumerator MoveToObject()
    {
        for (int i = 0; i < moveTargets.Length; i++)
        {
            Transform currentTarget = moveTargets[i].targetObject;
            float moveSpeed = moveTargets[i].moveSpeed;

            while (transform.position != currentTarget.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

                bool isLastTarget = (i == moveTargets.Length - 1);

                if (isLastTarget)
                {
                    // ��������� ���������� �� �������� ������� � �� ������ ����� ���������� ��������� ����������� �������� ��� ���������� ������� �������
                    float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
                    float scaleFactor = Mathf.Lerp(1f, minScaleFactor, distanceToTarget / moveSpeed);

                    // ��������� ���������� ������� �������
                    transform.localScale = initialScale * scaleFactor/3f;
                }

                yield return null;
            }
        }

        // ������� ������ ����� ���������� ���������� �������� �������
        Destroy(gameObject);
    }
}
