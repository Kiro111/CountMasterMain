using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [System.Serializable]
    public struct MoveTarget
    {
        public Transform targetObject; // Целевой объект
        public float moveSpeed; // Скорость перемещения к целевому объекту
    }

    [SerializeField] MoveTarget[] moveTargets; // Массив с целевыми объектами
    [SerializeField] float minScaleFactor = 0.0000001f; // Минимальный коэффициент масштаба

    private Vector3 initialScale; // Изначальный размер объекта
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
                    // Вычисляем расстояние до целевого объекта и на основе этого расстояния вычисляем коэффициент масштаба для уменьшения размера объекта
                    float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
                    float scaleFactor = Mathf.Lerp(1f, minScaleFactor, distanceToTarget / moveSpeed);

                    // Применяем уменьшение размера объекта
                    transform.localScale = initialScale * scaleFactor/3f;
                }

                yield return null;
            }
        }

        // Удаляем объект после достижения последнего целевого объекта
        Destroy(gameObject);
    }
}
