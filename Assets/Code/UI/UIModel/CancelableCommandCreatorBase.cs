using System;
using System.Threading;
using Code.Abstractions;
using Code.Abstractions.Command;
using Code.Tools;
using Code.Tools.InjectAssetAttribute;
using Code.Tools.Scriptable;
using UnityEngine;
using Zenject;

namespace Code.UI.UIModel
{
    public abstract class CancelableCommandCreatorBase<TCommand, TArgument>
        :CommandCreatorBase<TCommand> where TCommand:ICommand
    {
        [Inject] private AssetsContext _assetsContext;
        [Inject] private IAwaitable<TArgument> _awaitable;
        private CancellationTokenSource _cancellationToken;
        protected override async void ClassSpecificCommandCreator(Action<TCommand> creationCallback)
        {
            _cancellationToken = new CancellationTokenSource();
            try
            {
                var argument = await _awaitable.WithCancellation(_cancellationToken.Token);
                creationCallback?.Invoke(_assetsContext.Inject(CreateCommand(argument)));
            }
            catch (Exception e)
            {
            }
        }

        protected abstract TCommand CreateCommand(TArgument argument);

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            if (_cancellationToken != null)
            {
                _cancellationToken.Cancel();
                _cancellationToken.Dispose();
                _cancellationToken = null;
            }
        }
    }
}