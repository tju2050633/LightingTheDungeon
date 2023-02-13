namespace LightingTheDungeon
{
    public struct BackCommand : ICommand
    {
        public void Execute()
        {
            // 触发继续事件
            BackEvent.Invoke();
        }
    }
}