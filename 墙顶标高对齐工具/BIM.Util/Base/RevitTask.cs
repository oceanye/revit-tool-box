using Autodesk.Revit.UI;
using System;
using System.Threading.Tasks;

namespace BIM.Util.Base
{
    public class RevitTask
    {
        #region 属性

        private EventHandler Handler { get; set; }
        private TaskCompletionSource<object> TaskResult { get; set; }
        private ExternalEvent ExternalEvent { get; set; }

        #endregion

        #region IExternalEventHandler

        private void OnEventCompleted(object sender, object result)
        {
            _ = Handler.Exception == null ? TaskResult.TrySetResult(result) : TaskResult.TrySetException(Handler.Exception);
        }

        /// <summary>
        /// IExternalEventHandler
        /// </summary>
        private class EventHandler : IExternalEventHandler
        {
            private Func<UIApplication, object> _func;
            public event EventHandler<object> EventCompleted;
            public Exception Exception { get; private set; }
            public Func<UIApplication, object> Func
            {
                get => _func;
                set => _func = value ?? throw new ArgumentNullException("Func is null");
            }

            public void Execute(UIApplication app)
            {
                object result = null;
                Exception = null;
                try
                {
                    result = Func(app);
                }
                catch (Exception ex)
                {
                    Exception = ex;
                }
                EventCompleted?.Invoke(this, result);
            }

            public string GetName()
            {
                return "==TaskRevit==";
            }
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public RevitTask()
        {
            Handler = new EventHandler();
            Handler.EventCompleted += OnEventCompleted;
            ExternalEvent = ExternalEvent.Create(Handler);
        }

        /// <summary>
        /// func
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public Task<TResult> RunAsync<TResult>(Func<UIApplication, TResult> func)
        {
            TaskResult = new TaskCompletionSource<object>();
            var task = Task.Run(async () => (TResult)await TaskResult.Task.ConfigureAwait(false));
            Handler.Func = (app) => func(app);
            ExternalEvent.Raise();
            return task;
        }

        /// <summary>
        /// action
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public Task RunAsync(Action<UIApplication> act)
        {
            TaskResult = new TaskCompletionSource<object>();
            Handler.Func = (app) => { act(app); return new object(); };
            ExternalEvent.Raise();
            return TaskResult.Task;
        }
    }
}
