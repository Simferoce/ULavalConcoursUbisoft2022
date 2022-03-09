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
        Unknow
    }

    public struct Message
    {
        public int Id;
        public Source Source;
        public string Text;
        public float Duration;
    }

    public static Dictionary<int, Message> messages = new Dictionary<int, Message>()
    {
        {0, new Message() {Id = 0, Source = Source.Unknow, Text = "What ?!? A new assignment !?", Duration = 3.0f } },
        {1, new Message() {Id = 1, Source = Source.Unknow, Text = "Don't you think we did enough !? We are exhauted of all those SPECIALS measures. ", Duration = 3.0f } },
        {2, new Message() {Id = 2, Source = Source.Unknow, Text = "I cannot ... conti...", Duration = 2.0f } },
        {3, new Message() {Id = 3, Source = Source.Unknow, Text = "ARGGGGHHH !!!", Duration = 2.0f } },
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
