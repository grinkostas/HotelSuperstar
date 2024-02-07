
using UnityEngine.Events;

public interface IProgressible
{
    public UnityAction<float> ProgressChanged { get; set; }
}
