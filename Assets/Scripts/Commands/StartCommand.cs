namespace LightingTheDungeon
{
    public struct StartCommand : ICommand
    {
        public void Execute()
        {
            // 触发开始游戏事件
            StartEvent.Invoke();
        }
    }
}