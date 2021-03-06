using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleText : MonoBehaviour
{
    [SerializeField] private Canvas _canvas = null;
    [SerializeField] private TextMeshProUGUI _text = null;
    [SerializeField] private Vector3 _offSet = Vector3.zero;
    [SerializeField] private GameObject _conditionSucceed = null;

    private Camera _camera = null;
    private float _endMessage = 0.0f;
    private Message _currentMessage = null;
    public enum Source
    {
        Anxiety,
        Depression,
        Exhaustion,
        Wrath,
        Player,
        Unknow
    }

    public class Message
    {
        public int Id;
        public Source Source;
        public string Text;
        public float Duration;
        public bool ConditionBase = false;
    }

    private bool _conditionFill = false;

    public static Dictionary<int, Message> messages = new Dictionary<int, Message>()
    {
        {0, new Message() {Id = 0, Source = Source.Wrath, Text = "What ?!? A new assignment !?", Duration = 5.0f } },
        {1, new Message() {Id = 1, Source = Source.Wrath, Text = "Yeah. Give us more work because of the \"exceptional\" circumstances.", Duration = 3.0f } },
        {2, new Message() {Id = 2, Source = Source.Wrath, Text = "I cannot ... conti...", Duration = 5.0f } },
        {3, new Message() {Id = 3, Source = Source.Wrath, Text = "ARGGGGHHH !!!", Duration = 5.0f } },
        {4, new Message() {Id = 4, Source = Source.Exhaustion, Text = "I need zzzzz to work zzzzzz", Duration = 5.0f } },
        {5, new Message() {Id = 5, Source = Source.Exhaustion, Text = "No ! I was not sleeping.", Duration = 5.0f } },
        {6, new Message() {Id = 6, Source = Source.Exhaustion, Text = "So much work... I need to stay awake...", Duration = 5.0f } },
        {7, new Message() {Id = 7, Source = Source.Depression, Text = "There is nothing to do. Everyone is sick, everything is closed.", Duration = 5.0f } },
        {8, new Message() {Id = 8, Source = Source.Depression, Text = "The only thing that have not changed is the amount of work.", Duration = 5.0f } },
        {9, new Message() {Id = 9, Source = Source.Depression, Text = "I'm lonely. Only my thoughts are staying with me.", Duration = 5.0f } },
        {10, new Message() {Id = 10, Source = Source.Depression, Text = "My only friend is the darkness inside me.", Duration = 5.0f } },
        {11, new Message() {Id = 11, Source = Source.Depression, Text = "There is no purpose.", Duration = 5.0f } },
        {12, new Message() {Id = 12, Source = Source.Depression, Text = "It's futile to stuggle.", Duration = 5.0f } },
        {13, new Message() {Id = 13, Source = Source.Depression, Text = "Empty. Drained. Tarnished.", Duration = 5.0f } },
        {14, new Message() {Id = 14, Source = Source.Depression, Text = "Nothing matters; School, Love, Friends, all is ephemeral.", Duration = 5.0f } },
        {15, new Message() {Id = 15, Source = Source.Player, Text = "Hey you are not dead, you are sleeping!", Duration = 3.0f } },
        {16, new Message() {Id = 16, Source = Source.Player, Text = "The school library? What I am doing here? I should not be there. (W, A, S, D) to move", Duration = 3.0f, ConditionBase = true } },
        {17, new Message() {Id = 17, Source = Source.Player, Text = "That was scary. I need to get out.", Duration = 3.0f } },
        {18, new Message() {Id = 18, Source = Source.Player, Text = "Oh no...", Duration = 3.0f } },
        {19, new Message() {Id = 19, Source = Source.Player, Text = "They're everywhere !!!", Duration = 3.0f } },
        {20, new Message() {Id = 20, Source = Source.Player, Text = "It's blocked by mysterious energy. I should look around for a way to disable it.", Duration = 5.0f } },
        {21, new Message() {Id = 21, Source = Source.Player, Text = "That's not a friendly companion. Let's teach him a lesson or two. (Left Click to attack)", Duration = 3.0f, ConditionBase = true } },
        {22, new Message() {Id = 22, Source = Source.Player, Text = "Feeling empty or bored is not abnormal. But if it persists, it's important to seek help.", Duration = 10.0f } },
        {23, new Message() {Id = 23, Source = Source.Player, Text = "Feeling angry is totally normal. Talking to someone or praticing peaceful activities can help.", Duration = 10.0f } },
        {24, new Message() {Id = 24, Source = Source.Player, Text = "Work that piles up, no time to sleep, everybody felt that at least once. When it happens, you need to act before you burnout.", Duration = 10.0f } },
        {25, new Message() {Id = 25, Source = Source.Player, Text = "The door lock seems to be loosening up. One more push should do it!", Duration = 5.0f } },
        {26, new Message() {Id = 26, Source = Source.Player, Text = "It seems like I'm not welcome here. I should get out before it's too late", Duration = 5.0f } },
        {27, new Message() {Id = 27, Source = Source.Anxiety, Text = "Let's see what we have here. Oh, a little calf.", Duration = 5.0f } },
        {28, new Message() {Id = 28, Source = Source.Anxiety, Text = "Don't you feel the pressure. ", Duration = 5.0f } },
        {29, new Message() {Id = 29, Source = Source.Anxiety, Text = "You're shaking a lot. Are you afraid of something ?", Duration = 5.0f } },
        {30, new Message() {Id = 30, Source = Source.Anxiety, Text = "Ah ah ah, you are falling appart.", Duration = 5.0f } },
        {31, new Message() {Id = 31, Source = Source.Anxiety, Text = "You will ne-never succeed.", Duration = 5.0f } },
        {32, new Message() {Id = 32, Source = Source.Anxiety, Text = "I ca-cannot lose here.", Duration = 5.0f } },
        {33, new Message() {Id = 33, Source = Source.Anxiety, Text = "You should have taken time to prepare while you had the chance.", Duration = 5.0f } },
        {34, new Message() {Id = 34, Source = Source.Anxiety, Text = "No ! No ! It is not the end. I will be back.", Duration = 5.0f } },
        {35, new Message() {Id = 35, Source = Source.Anxiety, Text = "Look at you. You're a disgrace.", Duration = 5.0f } },
        {36, new Message() {Id = 36, Source = Source.Player, Text = "The exit is open !", Duration = 5.0f } },
    };

    private void Start()
    {
        _camera = Camera.main;

        AlignToCameraAngleYZAxis();
        _canvas.transform.localPosition = _canvas.transform.localRotation * _offSet;
    }

    private void AlignToCameraAngleYZAxis()
    {
        _canvas.transform.rotation = Quaternion.Euler(Vector3.Scale((Quaternion.Inverse(_camera.transform.rotation)).eulerAngles, new Vector3(0, 1, 1)));
    }

    public void ShowMessage(int id)
    {
        ShowMessage(id, -1);
    }

    public void ShowMessage(int id, float duration)
    {
        Message message;
        if(messages.TryGetValue(id, out message))
        {
            _conditionFill = false;
            _canvas.gameObject.SetActive(true);
            _conditionSucceed.SetActive(false);
            _text.SetText(message.Text);
            _endMessage = Time.time + (duration == -1 ? message.Duration : duration);
            _currentMessage = message;
        }
    }

    private void Update()
    {
        AlignToCameraAngleXAxis();
        RepositionateBubbleText();

        if((_currentMessage != null && !_currentMessage.ConditionBase) || _conditionFill)
        {
            if (Time.time > _endMessage)
            {
                _conditionSucceed.SetActive(false);
                _canvas.gameObject.SetActive(false);
                _currentMessage = null;
            }
        }
        
    }

    public void TriggerEndMessage(int messageId)
    {
        if(_currentMessage != null && _currentMessage.Id == messageId && !_conditionFill)
        {
            _endMessage = Time.time + _currentMessage.Duration;
            _conditionSucceed.SetActive(true);
            _conditionFill = true;
        }
    }

    private void RepositionateBubbleText()
    {
        _canvas.transform.position = _canvas.transform.parent.position + _canvas.transform.parent.localRotation * _offSet;
    }

    private void AlignToCameraAngleXAxis()
    {
        _canvas.transform.rotation = Quaternion.Euler(Vector3.Scale((_camera.transform.rotation).eulerAngles, new Vector3(1, 0, 0)));
    }
}
