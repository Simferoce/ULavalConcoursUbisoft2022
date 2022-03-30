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
        {0, new Message() {Id = 0, Source = Source.Wrath, Text = "What ?!? A new assignment !?", Duration = 3.0f } },
        {1, new Message() {Id = 1, Source = Source.Wrath, Text = "Yeah. Give us more work because of the \"exceptional\" circumstances.", Duration = 3.0f } },
        {2, new Message() {Id = 2, Source = Source.Wrath, Text = "I cannot ... conti...", Duration = 2.0f } },
        {3, new Message() {Id = 3, Source = Source.Wrath, Text = "ARGGGGHHH !!!", Duration = 2.0f } },
        {4, new Message() {Id = 4, Source = Source.Exhaustion, Text = "I need zzzzz to work zzzzzz", Duration = 4.0f } },
        {5, new Message() {Id = 5, Source = Source.Exhaustion, Text = "No ! I was not sleeping.", Duration = 4.0f } },
        {6, new Message() {Id = 6, Source = Source.Exhaustion, Text = "So much work... I need to stay awake...", Duration = 4.0f } },
        {7, new Message() {Id = 7, Source = Source.Depression, Text = "There is nothing to do. Everyone is sick, everything is closed.", Duration = 4.0f } },
        {8, new Message() {Id = 8, Source = Source.Depression, Text = "The only thing that have not changed is the amount of work.", Duration = 4.0f } },
        {9, new Message() {Id = 9, Source = Source.Depression, Text = "I am lonely. Only my thoughts are staying with me.", Duration = 4.0f } },
        {10, new Message() {Id = 10, Source = Source.Depression, Text = "My only friend is the darkness inside me.", Duration = 4.0f } },
        {11, new Message() {Id = 11, Source = Source.Depression, Text = "There is no purpose.", Duration = 4.0f } },
        {12, new Message() {Id = 12, Source = Source.Depression, Text = "It is futile to stuggle.", Duration = 4.0f } },
        {13, new Message() {Id = 13, Source = Source.Depression, Text = "Empty. Drain. Tarnish.", Duration = 4.0f } },
        {14, new Message() {Id = 14, Source = Source.Depression, Text = "Nothing matters; School, Love, Friends, all is ephemeral.", Duration = 4.0f } },
        {15, new Message() {Id = 15, Source = Source.Player, Text = "Death is only an illusion", Duration = 3.0f } },
        {16, new Message() {Id = 16, Source = Source.Player, Text = "The school library? What I am doing here? I should not be there. (W, A, S, D) to move", Duration = 3.0f, ConditionBase = true } },
        {17, new Message() {Id = 17, Source = Source.Player, Text = "That was scary. I need to get out.", Duration = 3.0f } },
        {18, new Message() {Id = 18, Source = Source.Player, Text = "Oh no...", Duration = 3.0f } },
        {19, new Message() {Id = 19, Source = Source.Player, Text = "They are everywhere !!!", Duration = 3.0f } },
        {20, new Message() {Id = 20, Source = Source.Player, Text = "It is blocked by mysterious energy. I should look around for a way to disable it.", Duration = 5.0f } },
        {21, new Message() {Id = 21, Source = Source.Player, Text = "That is not a friendly companion. Let's teach him a lesson or two. (Left Click to attack)", Duration = 3.0f, ConditionBase = true } },
        {22, new Message() {Id = 22, Source = Source.Player, Text = "Feeling empty, bored is not abnormal. But if it persists it is important to seek help.", Duration = 10.0f } },
        {23, new Message() {Id = 23, Source = Source.Player, Text = "Feeling angry is totally normal. Talking to someone or pratice peacefull activity can help.", Duration = 10.0f } },
        {24, new Message() {Id = 24, Source = Source.Player, Text = "Work that piles up, no times to sleep, everbody felt that at least once. When it happens, you need to act before you burn out.", Duration = 10.0f } },
        {25, new Message() {Id = 25, Source = Source.Player, Text = "The door lock seem to loose up. 1 more push and it should open.", Duration = 5.0f } },
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
