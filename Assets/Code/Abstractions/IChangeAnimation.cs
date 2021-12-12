using System;

namespace Code.Abstractions
{
    public interface IChangeAnimation
    {
        Action<AnimationStates> ChangeAnimation { get; set; }
    }
}