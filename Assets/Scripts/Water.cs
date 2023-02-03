using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Water : MonoBehaviour
{
    private TextMeshProUGUI tmpText; // получает значение нашего ресурса
    private BackScript tmpObject; // обьект нашего основного скрипта
    private BoxCollider2D m_Sprite; // отвечает за работу нашего слайна с водой
    public int waterValue = 5; // количество воды, которое добавляем при касании

    void Start()
    {
        tmpText = GameObject.FindGameObjectWithTag("Sunbeam").GetComponent<TextMeshProUGUI>(); // берем со сцены наше значение ресурса
        tmpObject = GameObject.FindGameObjectWithTag("Start").GetComponent<BackScript>(); // ищем наш скрипт на сцене
        m_Sprite = GetComponent<BoxCollider2D>(); // берем значение спрайта
    }
    private void OnTriggerEnter2D(Collider2D other) { // функция работы с триггером
        if(other.tag == "Roots") // проверка, что за триггер сработал
        {
            m_Sprite.isTrigger = false; // блокируем, чтобы не было множественное срабатывание
            int tms = int.Parse(tmpText.text); // переводим наше значение ресурсы
            tmpText.text = (tms + waterValue).ToString(); // меняем его и возвращаем на сцену
            this.waterValue = 0; // обнуляем значение локации
            tmpObject.st = false; // даем флаг на отработку перемещения из локации обьекта, а не корня
            tmpObject.xf1 = this.transform.position;
        }    
    }
}
