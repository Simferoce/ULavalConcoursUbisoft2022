using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleText : MonoBehaviour
{
    [SerializeField] private Canvas _canvas = null;
    [SerializeField] private TextMeshProUGUI _text = null;
    [SerializeField] private Vector3 _offSet = Vector3.zero;

    private Camera _camera = null;
    private float _endMessage = 0.0f;

    public enum Source
    {
        Depression,
        Exhaustion,
        Wrath,
        Player,
        Unknow
    }

    public struct Message
    {
        public Source Source;
        public string Text;
        public float Duration;
    }

    public static Dictionary<int, Message> messages = new Dictionary<int, Message>()
    {
        {0, new Message() {Source = Source.Wrath, Text = "What ?!? A new assignment !?", Duration = 3.0f } },
        {1, new Message() {Source = Source.Wrath, Text = "Yeah. Give us more work because of the \"exceptional\" circumstances.", Duration = 3.0f } },
        {2, new Message() {Source = Source.Wrath, Text = "I cannot ... conti...", Duration = 2.0f } },
        {3, new Message() {Source = Source.Wrath, Text = "ARGGGGHHH !!!", Duration = 2.0f } },
        {4, new Message() {Source = Source.Exhaustion, Text = "I need zzzzz to work zzzzzz", Duration = 2.0f } },
        {5, new Message() {Source = Source.Exhaustion, Text = "No ! I was not sleeping.", Duration = 2.0f } },
        {6, new Message() {Source = Source.Exhaustion, Text = "So much work... I need to stay awake...", Duration = 2.0f } },
        {7, new Message() {Source = Source.Depression, Text = "There is nothing to do. Everybody is sick, everything is close.", Duration = 4.0f } },
        {8, new Message() {Source = Source.Depression, Text = "The only thing that have not change is the amount of work.", Duration = 4.0f } },
        {9, new Message() {Source = Source.Depression, Text = "I am lonely. Only my thoughts are staying with me.", Duration = 4.0f } },
        {10, new Message() {Source = Source.Depression, Text = "My only friend is the darkness inside me.", Duration = 4.0f } },
        {11, new Message() {Source = Source.Depression, Text = "There is no purpose.", Duration = 4.0f } },
        {12, new Message() {Source = Source.Depression, Text = "It is futile to stuggle.", Duration = 4.0f } },
        {13, new Message() {Source = Source.Depression, Text = "Empty. Drain. Tarnish.", Duration = 4.0f } },
        {14, new Message() {Source = Source.Depression, Text = "Nothing matters; School, Love, Friends all is ephemeral.", Duration = 4.0f } },
        {15, new Message() {Source = Source.Player, Text = "Death is only an illusion", Duration = 3.0f } },
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
            _canvas.gameObject.SetActive(true);
            _text.SetText(message.Text);
            _endMessage = Time.time + (duration == -1 ? message.Duration : duration);
        }
    }

    private void Update()
    {
        AlignToCameraAngleXAxis();
        RepositionateBubbleText();

        if (Time.time > _endMessage)
        {
            _canvas.gameObject.SetActive(false);
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
