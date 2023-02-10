namespace GloryOfDead
{
    public struct PauseCommand : ICommand
    {
        public void Execute()
        {
            // 触发暂停事件
            PauseEvent.Invoke();
        }
    }
}