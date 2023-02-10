namespace GloryOfDead
{
    public struct ContinueCommand : ICommand
    {
        public void Execute()
        {
            // 触发继续事件
            ContinueEvent.Invoke();
        }
    }
}