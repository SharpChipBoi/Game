using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class ConversationChangeEvent : UnityEvent<Conversation> { }

public class ChoiceController : MonoBehaviour
{
    public Choice choice;
    public ConversationChangeEvent conversationChangeEvent;

    //если у нас есть вопрос, то пы создаем соответсвубщие кнопки(тут просто задается расстоаяние из родственная связь и размер)
    public static ChoiceController AddChoiceButton(Button choiceButtonTemplate, Choice choice, int index)
    {
        int buttonSpacing = -40;
        Button button = Instantiate(choiceButtonTemplate);

        button.transform.SetParent(choiceButtonTemplate.transform.parent);
        button.transform.localScale = Vector3.one;
        button.transform.localPosition = new Vector3(0, index * buttonSpacing, 0);
        button.name = "Choice " + (index + 1);
        button.gameObject.SetActive(true);

        ChoiceController choiceController = button.GetComponent<ChoiceController>();
        choiceController.choice = choice;
        return choiceController;
    }

    private void Start()//Если у нас есть вопрос то мы подключаем кнопки
    {
        if (conversationChangeEvent == null)
            conversationChangeEvent = new ConversationChangeEvent();
        GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
        //GameObject.Find("Dialogue box cover").SetActive(false);
    }

    public void MakeChoice()//Меняем диалог в зависимости от выбора
    {
        conversationChangeEvent.Invoke(choice.conversation);
        //GameObject.Find("Dialogue box cover").SetActive(true);
    }
}
