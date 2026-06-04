using UnityEngine;

public class FrictionSimulation : MonoBehaviour
{
    [Header("Состояние")]
    [SerializeField] private float position;
    [SerializeField] private float velocity;

    [Header("Настройки сил")]
    [SerializeField] private float outerForce; // Внешняя сила (например, тяга двигателя)
    [SerializeField] private float mass = 1.0f; // Масса объекта

    [Header("Трение")]
    [SerializeField] private float staticFrictionThreshold = 5.0f; // Порог сдвига
    [SerializeField] private float kineticFrictionCoefficient = 3.0f; // Сила сопротивления в движении

    private void FixedUpdate()
    {
        float acceleration = 0;

        // Проверяем, стоит ли объект
        if (Mathf.Approximately(velocity, 0))
        {
            // Состояние покоя: проверяем, достаточно ли внешней силы, чтобы сдвинуть объект
            if (Mathf.Abs(outerForce) > staticFrictionThreshold)
            {
                // Сдвигаемся: вычитаем кинетическое трение из внешней силы
                float netForce = outerForce - (Mathf.Sign(outerForce) * kineticFrictionCoefficient);
                acceleration = netForce / mass;
            }
            else
            {
                // Сила слишком мала, объект стоит на месте
                velocity = 0;
                acceleration = 0;
            }
        }
        else
        {
            // Состояние движения: действует кинетическое трение
            float frictionDirection = -Mathf.Sign(velocity);
            float netForce = outerForce + (frictionDirection * kineticFrictionCoefficient);
            acceleration = netForce / mass;

            // Проверка на остановку: если сила трения затормозила объект до "микро-скорости"
            // Мы принудительно останавливаем его, чтобы избежать бесконечного медленного дрейфа
            if (Mathf.Abs(velocity) < 0.01f && Mathf.Abs(outerForce) <= staticFrictionThreshold)
            {
                velocity = 0;
                acceleration = 0;
            }
        }

        // Интеграция Эйлера
        velocity += (acceleration + outerForce)* Time.fixedDeltaTime;
        position += velocity * Time.fixedDeltaTime;

        // Визуализация в Unity
        transform.position = new Vector3(position, transform.position.y, transform.position.z);
    }
}
